CREATE PROCEDURE [dbo].[sp_categoryDelete]
@IdCategory INT
AS
DELETE FROM Category
WHERE IdCategory = @IdCategory