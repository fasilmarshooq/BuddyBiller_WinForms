
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/08/2019 12:20:57
-- Generated from EDMX file: C:\Users\fasil.m\Source\Repos\BuddyBiller\BB.System.Common\RepositoryModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [AnyStore];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ProductProductType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Products] DROP CONSTRAINT [FK_ProductProductType];
GO
IF OBJECT_ID(N'[dbo].[FK_TransactionDetailProduct]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TransactionDetails] DROP CONSTRAINT [FK_TransactionDetailProduct];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[ProductTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductTypes];
GO
IF OBJECT_ID(N'[dbo].[Parties]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Parties];
GO
IF OBJECT_ID(N'[dbo].[Products]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Products];
GO
IF OBJECT_ID(N'[dbo].[TransactionDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TransactionDetails];
GO
IF OBJECT_ID(N'[dbo].[Transactions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Transactions];
GO
IF OBJECT_ID(N'[dbo].[users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[users];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ProductTypes'
CREATE TABLE [dbo].[ProductTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(50)  NULL,
    [Description] varchar(max)  NULL,
    [Added_Date] datetime  NULL,
    [Added_By_Id] int  NOT NULL
);
GO

-- Creating table 'Parties'
CREATE TABLE [dbo].[Parties] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Type] varchar(50)  NULL,
    [Name] varchar(150)  NULL,
    [Email] varchar(150)  NULL,
    [PhoneNumber] varchar(15)  NULL,
    [Address] varchar(max)  NULL,
    [Added_Date] datetime  NULL,
    [IsActive] bit  NOT NULL,
    [Added_By_Id] int  NOT NULL
);
GO

-- Creating table 'Products'
CREATE TABLE [dbo].[Products] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(50)  NULL,
    [Description] varchar(max)  NULL,
    [Rate] decimal(18,2)  NULL,
    [Qty] decimal(18,2)  NULL,
    [Added_Date] datetime  NULL,
    [ProductType_Id] int  NOT NULL,
    [Added_By_Id] int  NOT NULL
);
GO

-- Creating table 'TransactionDetails'
CREATE TABLE [dbo].[TransactionDetails] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Rate] decimal(18,2)  NULL,
    [Qty] decimal(18,2)  NULL,
    [Total] decimal(18,2)  NULL,
    [Product_Id] int  NOT NULL,
    [Transaction_Id] int  NOT NULL
);
GO

-- Creating table 'Transactions'
CREATE TABLE [dbo].[Transactions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Type] varchar(50)  NULL,
    [Transaction_date] datetime  NULL,
    [Tax] decimal(18,2)  NULL,
    [Discount] decimal(18,2)  NULL,
    [GrandTotal] decimal(18,2)  NULL,
    [Added_By_Id] int  NOT NULL,
    [Party_Id] int  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [first_name] varchar(50)  NULL,
    [last_name] varchar(50)  NULL,
    [email] varchar(150)  NULL,
    [username] varchar(50)  NULL,
    [password] varchar(50)  NULL,
    [contact] varchar(15)  NULL,
    [address] varchar(max)  NULL,
    [gender] varchar(10)  NULL,
    [user_type] varchar(15)  NULL
);
GO

-- Creating table 'PartyTypeConfigs'
CREATE TABLE [dbo].[PartyTypeConfigs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'ProductTypes'
ALTER TABLE [dbo].[ProductTypes]
ADD CONSTRAINT [PK_ProductTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Parties'
ALTER TABLE [dbo].[Parties]
ADD CONSTRAINT [PK_Parties]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [PK_Products]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TransactionDetails'
ALTER TABLE [dbo].[TransactionDetails]
ADD CONSTRAINT [PK_TransactionDetails]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Transactions'
ALTER TABLE [dbo].[Transactions]
ADD CONSTRAINT [PK_Transactions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PartyTypeConfigs'
ALTER TABLE [dbo].[PartyTypeConfigs]
ADD CONSTRAINT [PK_PartyTypeConfigs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ProductType_Id] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [FK_ProductProductType]
    FOREIGN KEY ([ProductType_Id])
    REFERENCES [dbo].[ProductTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductProductType'
CREATE INDEX [IX_FK_ProductProductType]
ON [dbo].[Products]
    ([ProductType_Id]);
GO

-- Creating foreign key on [Product_Id] in table 'TransactionDetails'
ALTER TABLE [dbo].[TransactionDetails]
ADD CONSTRAINT [FK_TransactionDetailProduct]
    FOREIGN KEY ([Product_Id])
    REFERENCES [dbo].[Products]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TransactionDetailProduct'
CREATE INDEX [IX_FK_TransactionDetailProduct]
ON [dbo].[TransactionDetails]
    ([Product_Id]);
GO

-- Creating foreign key on [Transaction_Id] in table 'TransactionDetails'
ALTER TABLE [dbo].[TransactionDetails]
ADD CONSTRAINT [FK_TransactionTransactionDetail]
    FOREIGN KEY ([Transaction_Id])
    REFERENCES [dbo].[Transactions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TransactionTransactionDetail'
CREATE INDEX [IX_FK_TransactionTransactionDetail]
ON [dbo].[TransactionDetails]
    ([Transaction_Id]);
GO

-- Creating foreign key on [Added_By_Id] in table 'Transactions'
ALTER TABLE [dbo].[Transactions]
ADD CONSTRAINT [FK_Transactionuser]
    FOREIGN KEY ([Added_By_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Transactionuser'
CREATE INDEX [IX_FK_Transactionuser]
ON [dbo].[Transactions]
    ([Added_By_Id]);
GO

-- Creating foreign key on [Added_By_Id] in table 'Parties'
ALTER TABLE [dbo].[Parties]
ADD CONSTRAINT [FK_Partyuser]
    FOREIGN KEY ([Added_By_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Partyuser'
CREATE INDEX [IX_FK_Partyuser]
ON [dbo].[Parties]
    ([Added_By_Id]);
GO

-- Creating foreign key on [Added_By_Id] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [FK_Productuser]
    FOREIGN KEY ([Added_By_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Productuser'
CREATE INDEX [IX_FK_Productuser]
ON [dbo].[Products]
    ([Added_By_Id]);
GO

-- Creating foreign key on [Added_By_Id] in table 'ProductTypes'
ALTER TABLE [dbo].[ProductTypes]
ADD CONSTRAINT [FK_ProductTypeuser]
    FOREIGN KEY ([Added_By_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductTypeuser'
CREATE INDEX [IX_FK_ProductTypeuser]
ON [dbo].[ProductTypes]
    ([Added_By_Id]);
GO

-- Creating foreign key on [Party_Id] in table 'Transactions'
ALTER TABLE [dbo].[Transactions]
ADD CONSTRAINT [FK_TransactionParty]
    FOREIGN KEY ([Party_Id])
    REFERENCES [dbo].[Parties]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TransactionParty'
CREATE INDEX [IX_FK_TransactionParty]
ON [dbo].[Transactions]
    ([Party_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------