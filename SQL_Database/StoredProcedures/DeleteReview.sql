CREATE PROCEDURE DeleteReview @guId VARCHAR(1000), @userId INT
AS
IF EXISTS(SELECT reviewId FROM Review WHERE userId = @userId AND MovieGUId = @guId)
BEGIN
	DELETE FROM Review WHERE userId = @userId AND movieGUId = @guId
END