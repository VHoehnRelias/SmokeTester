CREATE TABLE [dbo].[Release] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [Name]         NVARCHAR (50) NULL,
    [Date]         DATE          NULL,
    [DateAdded]    DATE          DEFAULT (getdate()) NULL,
    [AddedBy]      NVARCHAR (50) DEFAULT (user_name()) NULL,
    [DateModified] DATE          DEFAULT (getdate()) NULL,
    [ModifiedBy]   NVARCHAR (50) DEFAULT (user_name()) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);




GO

CREATE TRIGGER [dbo].[ModDate] on [dbo].[Release] 
	after update
as
begin

UPDATE [dbo].[Release] 
   SET DateModified = GETDATE(),
   ModifiedBy = CURRENT_USER
  FROM [dbo].[Release] X
  JOIN inserted I ON X.Id = I.Id

end