CREATE TABLE [dbo].[Reports] (
    [Id]                            INT            IDENTITY (1, 1) NOT NULL,
    [Report Name]                   NVARCHAR (50)  NOT NULL,
    [File Name]                     NVARCHAR (50)  NOT NULL,
    [Left Navigation Menu location] NVARCHAR (250) NULL,
    [AssociatedTickets]             NVARCHAR (250) NULL,
    [DateAdded]                     DATE           DEFAULT (getdate()) NULL,
    [AddedBy]                       NVARCHAR (50)  DEFAULT (user_name()) NULL,
    [DateModified]                  DATE           DEFAULT (getdate()) NULL,
    [ModifiedBy]                    NVARCHAR (50)  DEFAULT (user_name()) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);




GO

CREATE TRIGGER [dbo].[Trigger_Reports]
    ON [dbo].[Reports]
    FOR UPDATE
    AS
    BEGIN
		UPDATE [dbo].[Release] 
		   SET DateModified = GETDATE(),
		   ModifiedBy = CURRENT_USER
		  FROM [dbo].[Release] X
		  JOIN inserted I ON X.Id = I.Id
    END