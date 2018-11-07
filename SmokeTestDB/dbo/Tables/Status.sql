CREATE TABLE [dbo].[Status] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [Name]         NCHAR (10)    NULL,
    [DateAdded]    DATE          DEFAULT (getdate()) NULL,
    [AddedBy]      NVARCHAR (50) DEFAULT (user_name()) NULL,
    [DateModified] DATE          DEFAULT (getdate()) NULL,
    [ModifiedBy]   NVARCHAR (50) DEFAULT (user_name()) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);




GO
CREATE TRIGGER [dbo].[Status_Modified] on [dbo].[Status] 
	after update
as
begin

UPDATE [dbo].[Status] 
   SET DateModified = GETDATE(),
   ModifiedBy = CURRENT_USER
  FROM [dbo].[Status] X
  JOIN inserted I ON X.Id = I.Id

end