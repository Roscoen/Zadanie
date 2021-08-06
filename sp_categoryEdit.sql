CREATE PROCEDURE [dbo].[sp_categoryEdit]
@shortName NCHAR (50),
@longName NCHAR (50),
@IdCategory INT
AS
UPDATE Category
SET shortName = @shortName, longName = @longName
WHERE IdCategory = @IdCategory