IF OBJECT_ID('TransactionSummaryReport') IS NOT NULL
DROP PROCEDURE TransactionSummaryReport
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE TransactionSummaryReport
(
	@type varchar(50) = NULL,
	@CustomerName	varchar(150) =NULL,
	@FromDate datetime = NULL,
	@ToDate datetime = Null
)
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @FilterConditions nvarchar(max)  
	DECLARE @sql nvarchar(max)  
	Set @FilterConditions = ''  

	IF @type IS NOT NULL  AND @type <> ''
	BEGIN 
		SET @FilterConditions = CONCAT(@FilterConditions,' AND  tbl_transactions.type = @type ') 
	END  

	IF @CustomerName IS NOT NULL  AND @CustomerName <> ''
	BEGIN 
		SET @FilterConditions = CONCAT(@FilterConditions,' AND  tbl_dea_cust.name = @CustomerName ') 
	END  

	IF @FromDate IS NOT NULL  
	BEGIN 
		SET @FilterConditions = CONCAT(@FilterConditions,' AND  tbl_transactions.transaction_date >= @FromDate ') 
	END  

	IF @ToDate IS NOT NULL  
	BEGIN 
		SET @FilterConditions = CONCAT(@FilterConditions,' AND  tbl_transactions.transaction_date <= @ToDate ') 
	END
	
	SET @sql='SELECT
	tbl_transactions.type [Transaction Type],
	tbl_dea_cust.Name [Customer Name],
	partytypeconfigs.name [Customer Type],
	tbl_transactions.transaction_date [Date],
	tbl_transactions.discount [Discount],
	tbl_transactions.grandTotal [Grand Total]
	

	FROM 
		tbl_transactions 
		INNER JOIN tbl_dea_cust 
			ON tbl_transactions.dea_cust_id = tbl_dea_cust.id
		INNER JOIN partytypeconfigs
			ON tbl_dea_cust.partytypeid = partytypeconfigs.id
		WHERE tbl_transactions.dea_cust_id IS NOT NULL
		FILTERCONDITIONS  '  

	IF @FilterConditions IS NOT NULL  
		SET @sql = REPLACE(@sql, 'FILTERCONDITIONS', @FilterConditions )   
	ELSE  
		SET @sql = REPLACE(@sql, 'FILTERCONDITIONS', '' )   
	
	EXEC sp_executesql @sql, N'    
   
	 @type varchar(50),
	 @CustomerName	varchar(150),
	 @FromDate datetime,
	 @ToDate datetime'    
	 , @type 
	 , @CustomerName   
	 , @FromDate  
	 , @ToDate 
	 

END

