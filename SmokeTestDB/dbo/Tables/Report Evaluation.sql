CREATE TABLE [dbo].[Report Evaluation] (
    [Id]           INT             IDENTITY (1, 1) NOT NULL,
    [Evaluator ID] INT             NOT NULL,
    [Report ID]    INT             NOT NULL,
    [Status]       NCHAR (10)      NULL,
    [Comment]      NVARCHAR (1000) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Report Evaluation_ToEvaluators] FOREIGN KEY ([Evaluator ID]) REFERENCES [dbo].[Evaluators] ([Id]),
    CONSTRAINT [FK_Report Evaluation_ToReports] FOREIGN KEY ([Report ID]) REFERENCES [dbo].[Reports] ([Id])
);



