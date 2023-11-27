CREATE PROCEDURE CreateReview 
@userId INT, @reviewText VARCHAR(1000), @rating INT,  @guId VARCHAR(1000)
AS
DECLARE @updated bit
IF EXISTS(SELECT userId FROM Review WHERE userId = @userId AND movieGUId = @guId)
BEGIN 
	UPDATE Review
	SET reviewText = @reviewText, rating = @rating
	WHERE userId = @userId AND movieGUId = @guId
	SET @updated = 1
END
ELSE
BEGIN
	INSERT INTO Review (movieGUId, userId, reviewText, rating)
	VALUES (@guId, @userId, @reviewText, @rating)
	SET @updated = 0
END
SELECT @updated AS Updated