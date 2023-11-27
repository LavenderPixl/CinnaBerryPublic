CREATE PROCEDURE GetReviews @userId INT
AS
SET XACT_ABORT, NOCOUNT ON;
IF EXISTS (SELECT userId FROM UserTable WHERE userId = @userId)
BEGIN

SELECT movieGUId, reviewText, rating, UserTable.userName FROM Review 
INNER JOIN UserTable ON Review.userId = UserTable.userId
WHERE UserTable.userId = @userId
END