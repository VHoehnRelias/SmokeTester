CREATE TABLE [dbo].[Section Evaluation] (
    [Id]           INT             IDENTITY (1, 1) NOT NULL,
    [Evaluator ID] INT             NOT NULL,
    [Section ID]   INT             NOT NULL,
    [Status]       NCHAR (10)      NULL,
    [Comment]      NVARCHAR (1000) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Section Evaluation_ToEvaluators] FOREIGN KEY ([Evaluator ID]) REFERENCES [dbo].[Evaluators] ([Id]),
    CONSTRAINT [FK_Section Evaluation_ToSections] FOREIGN KEY ([Section ID]) REFERENCES [dbo].[Sections] ([Id])
);



