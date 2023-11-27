CREATE PROCEDURE GetReviewsForMovie @movieGUId varchar(1000), @userId INT
AS
SET XACT_ABORT, NOCOUNT ON;

IF EXISTS (SELECT movieGUId FROM Review WHERE movieGUId = @movieGUId)
BEGIN

SELECT reviewId, movieGUId, Review.userId, reviewText, rating, userName FROM Review 
INNER JOIN Usertable ON Review.userId = Usertable.userId
WHERE movieGUId = @movieGUId
AND Review.userId != @userId

END