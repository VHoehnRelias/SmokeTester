CREATE TABLE [dbo].[Release] (
    [Id]   INT           IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (50) NULL,
    [Date] DATE          NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

