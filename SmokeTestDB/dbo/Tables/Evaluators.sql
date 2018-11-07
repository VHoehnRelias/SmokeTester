CREATE TABLE [dbo].[Evaluators] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [Name]         NCHAR (50)    NULL,
    [DateAdded]    DATE          DEFAULT (getdate()) NULL,
    [AddedBy]      NVARCHAR (50) DEFAULT (user_name()) NULL,
    [DateModified] DATE          DEFAULT (getdate()) NULL,
    [ModifiedBy]   NVARCHAR (50) DEFAULT (user_name()) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);




GO
CREATE TRIGGER [dbo].[Evaluators_Modified] on [dbo].[Evaluators] 
	after update
as
begin

UPDATE [dbo].[Evaluators] 
   SET DateModified = GETDATE(),
   ModifiedBy = CURRENT_USER
  FROM [dbo].[Evaluators] X
  JOIN inserted I ON X.Id = I.Id

end