CREATE TABLE [dbo].[Staff]
(
	[id] INT IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[employee_name] VARCHAR(24) NOT NULL,
    [post_id] INT NOT NULL REFERENCES Post([id])
);
