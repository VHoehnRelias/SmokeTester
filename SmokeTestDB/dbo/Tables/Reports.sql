CREATE TABLE [dbo].[Reports] (
    [Id]                            INT            IDENTITY (1, 1) NOT NULL,
    [Report Name]                   NVARCHAR (50)  NOT NULL,
    [File Name]                     NVARCHAR (50)  NOT NULL,
    [Left Navigation Menu location] NVARCHAR (250) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

