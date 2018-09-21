CREATE TABLE [dbo].[Reports] (
    [Id]                            INT            IDENTITY (1, 1) NOT NULL,
    [Report Name]                   NVARCHAR (50)  NOT NULL,
    [File Name]                     NVARCHAR (50)  NOT NULL,
    [Left Navigation Menu location] NVARCHAR (250) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
CREATE TABLE [dbo].[Sections] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Report ID]   INT            NOT NULL,
    [Section]     NVARCHAR (50)  NOT NULL,
    [Description] NVARCHAR (250) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Sections_ToReports] FOREIGN KEY ([Report ID]) REFERENCES [dbo].[Reports] ([Id])
);
CREATE TABLE [dbo].[Evaluators] (
    [Id]   INT        IDENTITY (1, 1) NOT NULL,
    [Name] NCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
CREATE TABLE [dbo].[Release] (
    [Id]   INT           IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (50) NULL,
    [Date] DATE          NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
CREATE TABLE [dbo].[Status] (
    [Id]   INT        IDENTITY (1, 1) NOT NULL,
    [Name] NCHAR (10) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
CREATE TABLE [dbo].[Report Evaluation] (
    [Id]           INT             IDENTITY (1, 1) NOT NULL,
    [Release ID]   INT             NOT NULL,
    [Evaluator ID] INT             NOT NULL,
    [Report ID]    INT             NOT NULL,
    [Status ID]    INT             NULL,
    [Comment]      NVARCHAR (1000) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Report Evaluation_ToEvaluators] FOREIGN KEY ([Evaluator ID]) REFERENCES [dbo].[Evaluators] ([Id]),
    CONSTRAINT [FK_Report Evaluation_ToRelease] FOREIGN KEY ([Release ID]) REFERENCES [dbo].[Release] ([Id]),
    CONSTRAINT [FK_Report Evaluation_ToReports] FOREIGN KEY ([Report ID]) REFERENCES [dbo].[Reports] ([Id]),
    CONSTRAINT [FK_Report Evaluation_ToStatus] FOREIGN KEY ([Status ID]) REFERENCES [dbo].[Status] ([Id])
);
CREATE TABLE [dbo].[Section Evaluation] (
    [Id]           INT             IDENTITY (1, 1) NOT NULL,
    [Release ID]   INT             NOT NULL,
    [Evaluator ID] INT             NOT NULL,
    [Report ID]    INT             NOT NULL,
    [Section ID]   INT             NOT NULL,
    [Status ID]    INT             NULL,
    [Comment]      NVARCHAR (1000) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Section Evaluation_ToEvaluators] FOREIGN KEY ([Evaluator ID]) REFERENCES [dbo].[Evaluators] ([Id]),
    CONSTRAINT [FK_Section Evaluation_ToRelease] FOREIGN KEY ([Release ID]) REFERENCES [dbo].[Release] ([Id]),
    CONSTRAINT [FK_Section Evaluation_ToReport] FOREIGN KEY ([Report ID]) REFERENCES [dbo].[Reports] ([Id]),
    CONSTRAINT [FK_Section Evaluation_ToSections] FOREIGN KEY ([Section ID]) REFERENCES [dbo].[Sections] ([Id]),
    CONSTRAINT [FK_Section Evaluation_ToStatus] FOREIGN KEY ([Status ID]) REFERENCES [dbo].[Status] ([Id])
);
