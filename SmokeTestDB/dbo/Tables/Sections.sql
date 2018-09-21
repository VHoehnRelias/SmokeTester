CREATE TABLE [dbo].[Sections] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Report ID]   INT            NOT NULL,
    [Section]     NVARCHAR (50)  NOT NULL,
    [Description] NVARCHAR (250) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Sections_ToReports] FOREIGN KEY ([Report ID]) REFERENCES [dbo].[Reports] ([Id])
);

