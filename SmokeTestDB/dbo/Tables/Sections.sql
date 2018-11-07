CREATE TABLE [dbo].[Sections] (
    [Id]                INT            IDENTITY (1, 1) NOT NULL,
    [Report ID]         INT            NOT NULL,
    [OrderID]           INT            NULL,
    [SectionName]       NVARCHAR (50)  NOT NULL,
    [Description]       NVARCHAR (250) NULL,
    [AssociatedTickets] NVARCHAR (250) NULL,
    [DateAdded]         DATE           DEFAULT (getdate()) NULL,
    [AddedBy]           NVARCHAR (50)  DEFAULT (user_name()) NULL,
    [DateModified]      DATE           DEFAULT (getdate()) NULL,
    [ModifiedBy]        NVARCHAR (50)  DEFAULT (user_name()) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Sections_ToReports] FOREIGN KEY ([Report ID]) REFERENCES [dbo].[Reports] ([Id])
);




GO
CREATE TRIGGER [dbo].[Sections_Modified] on [dbo].[Sections] 
	after update
as
begin

UPDATE [dbo].[Sections] 
   SET DateModified = GETDATE(),
   ModifiedBy = CURRENT_USER
  FROM [dbo].[Sections] X
  JOIN inserted I ON X.Id = I.Id

end