UPDATE dbo.[Section Evaluation] 
SET [Release ID] = 4, [Evaluator ID] = 1, [Report ID] = 18, [Status ID] = 1
SELECT * FROM 
dbo.Sections s
LEFT JOIN dbo.[Section Evaluation] se ON s.[ID] = se.[Section ID]
WHERE s.[Report ID] = 18 AND ISNULL(se.[Release ID],0) = 0
