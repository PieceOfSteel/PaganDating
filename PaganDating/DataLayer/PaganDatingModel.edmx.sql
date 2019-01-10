
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 01/10/2019 10:53:36
-- Generated from EDMX file: C:\Users\ander\source\repos\PaganDating\PaganDating\DataLayer\PaganDatingModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [PaganDating];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_UserMessage]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MessageSet] DROP CONSTRAINT [FK_UserMessage];
GO
IF OBJECT_ID(N'[dbo].[FK_MessageUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MessageSet] DROP CONSTRAINT [FK_MessageUser];
GO
IF OBJECT_ID(N'[dbo].[FK_UserFriends]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FriendshipsSet] DROP CONSTRAINT [FK_UserFriends];
GO
IF OBJECT_ID(N'[dbo].[FK_FriendsUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FriendshipsSet] DROP CONSTRAINT [FK_FriendsUser];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[UserSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserSet];
GO
IF OBJECT_ID(N'[dbo].[MessageSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MessageSet];
GO
IF OBJECT_ID(N'[dbo].[FriendshipsSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FriendshipsSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'UserSet'
CREATE TABLE [dbo].[UserSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [ProfileImage] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [AccountId] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'MessageSet'
CREATE TABLE [dbo].[MessageSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Content] nvarchar(max)  NOT NULL,
    [Author_Id] int  NOT NULL,
    [Recipient_Id] int  NOT NULL
);
GO

-- Creating table 'FriendshipsSet'
CREATE TABLE [dbo].[FriendshipsSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [RequestAccepted] bit  NOT NULL,
    [User_Id] int  NOT NULL,
    [Friend_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'UserSet'
ALTER TABLE [dbo].[UserSet]
ADD CONSTRAINT [PK_UserSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MessageSet'
ALTER TABLE [dbo].[MessageSet]
ADD CONSTRAINT [PK_MessageSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'FriendshipsSet'
ALTER TABLE [dbo].[FriendshipsSet]
ADD CONSTRAINT [PK_FriendshipsSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Author_Id] in table 'MessageSet'
ALTER TABLE [dbo].[MessageSet]
ADD CONSTRAINT [FK_UserMessage]
    FOREIGN KEY ([Author_Id])
    REFERENCES [dbo].[UserSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserMessage'
CREATE INDEX [IX_FK_UserMessage]
ON [dbo].[MessageSet]
    ([Author_Id]);
GO

-- Creating foreign key on [Recipient_Id] in table 'MessageSet'
ALTER TABLE [dbo].[MessageSet]
ADD CONSTRAINT [FK_MessageUser]
    FOREIGN KEY ([Recipient_Id])
    REFERENCES [dbo].[UserSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MessageUser'
CREATE INDEX [IX_FK_MessageUser]
ON [dbo].[MessageSet]
    ([Recipient_Id]);
GO

-- Creating foreign key on [User_Id] in table 'FriendshipsSet'
ALTER TABLE [dbo].[FriendshipsSet]
ADD CONSTRAINT [FK_UserFriends]
    FOREIGN KEY ([User_Id])
    REFERENCES [dbo].[UserSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserFriends'
CREATE INDEX [IX_FK_UserFriends]
ON [dbo].[FriendshipsSet]
    ([User_Id]);
GO

-- Creating foreign key on [Friend_Id] in table 'FriendshipsSet'
ALTER TABLE [dbo].[FriendshipsSet]
ADD CONSTRAINT [FK_FriendsUser]
    FOREIGN KEY ([Friend_Id])
    REFERENCES [dbo].[UserSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FriendsUser'
CREATE INDEX [IX_FK_FriendsUser]
ON [dbo].[FriendshipsSet]
    ([Friend_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------