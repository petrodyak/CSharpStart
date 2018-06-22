CREATE PROCEDURE [dbo].[spu_GetMovieByID]
@MovieID INT
AS
BEGIN
  SELECT *
    FROM dbo.Movies
  WHERE ID = @MovieID
END