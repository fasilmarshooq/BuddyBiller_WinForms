
IF OBJECT_ID('SaveCustomers') IS NOT NULL
DROP PROCEDURE SaveCustomers
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE SaveCustomers
(
		@type varchar(50), 
		@name varchar(150), 
		@email varchar(150), 
		@contact varchar(15), 
		@address text,  
		@added_by int
)
AS
BEGIN
SET NOCOUNT ON

IF EXISTS (SELECT * FROM tbl_dea_cust WHERE name= @name) 
	BEGIN
		UPDATE [dbo].[tbl_dea_cust]
		   SET [type] = PTC.Name,
			   [name] = @name,
		       [email] = @email,
		       [contact] = @contact,
		       [address] = @address,
		       [added_date] = SYSDATETIMEOFFSET(),
		       [added_by] = @added_by,
		       [PartyTypeId] = PTC.Id
		 FROM
			PartyTypeConfigs PTC
		 WHERE PTC.Name = @type
	END
ELSE
	BEGIN
		INSERT INTO [dbo].[tbl_dea_cust]
           ([type]
           ,[name]
           ,[email]
           ,[contact]
           ,[address]
           ,[added_date]
           ,[added_by]
           ,[PartyTypeId])
		SELECT
			PTC.Name,
			@name,
			@email,
			@contact,
			@address,
			SYSDATETIMEOFFSET(),
			@added_by,
			PTC.Id
		FROM
			PartyTypeConfigs PTC
		WHERE PTC.Name = @type	
	END

END
	
