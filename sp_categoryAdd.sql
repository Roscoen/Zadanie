CREATE PROCEDURE [dbo].[sp_categoryAdd]
@shortName NCHAR (50),
@longName NCHAR (50),
@IdCategory INT
AS
INSERT INTO Category (shortName, longName) VALUES (@shortName, @longName)
SET @IdCategory = @@IDENTITY