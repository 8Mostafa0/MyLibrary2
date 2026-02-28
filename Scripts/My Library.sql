IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'MyLibrary')
BEGIN
    CREATE DATABASE MyLibrary;
    PRINT 'Database MyLibrary created.';
END
ELSE
BEGIN
    PRINT 'Database MyLibrary already exists.';
END
GO

USE MyLibrary;
GO

CREATE TABLE [Clients] (
  [Id] INT IDENTITY(1,1) PRIMARY KEY,
  [FirstName] nvarchar(255),
  [LastName] nvarchar(255),
  [Tier] INT,
  [CreatedAt] Datetime,
  [UpdatedAt] DateTime
)
GO

CREATE TABLE [Books] (
  [Id] INT IDENTITY(1,1) PRIMARY KEY,
  [Name] nvarchar(255),
  [Publisher] nvarchar(255),
  [Subject] nvarchar(255),
  [PublicationDate] varchar(255),
  [Tier] INT,
  [CreatedAt] Datetime,
  [UpdatedAt] DateTime
)
GO

CREATE TABLE [Loans] (
  [Id] INT IDENTITY(1,1) PRIMARY KEY,
  [ClientId] INT NOT NULL,
  [BookId] INT NOT NULL,
  [ReturnDate] DateTime,
  [ReturnedDAte] DateTime,
  [CreatedAt] Datetime,
  [UpdatedAt] DateTime
)
GO

CREATE TABLE [ReservedBooks] (
  [Id] INT IDENTITY(1,1) PRIMARY KEY,
  [BookId] INT,
  [ClientId] INT,
  [CreatedAt] Datetime,
  [UpdatedAt] DateTime
)
GO

ALTER TABLE [Loans] ADD FOREIGN KEY ([ClientId]) REFERENCES [Clients] ([Id])
GO

ALTER TABLE [Loans] ADD FOREIGN KEY ([BookId]) REFERENCES [Books] ([Id])
GO

ALTER TABLE [ReservedBooks] ADD FOREIGN KEY ([BookId]) REFERENCES [Books] ([Id])
GO

ALTER TABLE [ReservedBooks] ADD FOREIGN KEY ([ClientId]) REFERENCES [Clients] ([Id])
GO
