CREATE TABLE [dbo].[Report Evaluation] (
    [Id]           INT             IDENTITY (1, 1) NOT NULL,
    [Release ID]   INT             NOT NULL,
    [Evaluator ID] INT             NOT NULL,
    [Report ID]    INT             NOT NULL,
    [Status ID]    INT             NULL,
    [Comment]      NVARCHAR (1000) NULL,
    [DateAdded]    DATE            DEFAULT (getdate()) NULL,
    [AddedBy]      NVARCHAR (50)   DEFAULT (user_name()) NULL,
    [DateModified] DATE            DEFAULT (getdate()) NULL,
    [ModifiedBy]   NVARCHAR (50)   DEFAULT (user_name()) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);








GO
CREATE TRIGGER [dbo].[Report_Evaluation_Modified] on [dbo].[Report Evaluation] 
	after update
as
begin

UPDATE [dbo].[Report Evaluation] 
   SET DateModified = GETDATE(),
   ModifiedBy = CURRENT_USER
  FROM [dbo].[Report Evaluation] X
  JOIN inserted I ON X.Id = I.Id

end