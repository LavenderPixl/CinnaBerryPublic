CREATE PROCEDURE UpdateMovieWatched @guid varchar(1000), @userid int, @planToWatch bit
as
IF EXISTS(SELECT * FROM MoviesWatched WHERE movieGUId = @guid AND userId = @userid)
BEGIN
	UPDATE MoviesWatched
	SET planToWatch = @planToWatch
	WHERE movieGUId = @guid AND userId = @userid
END
ELSE
BEGIN
	INSERT INTO MoviesWatched(movieGUId, userId, planToWatch)
	VALUES(@guid, @userid, @planToWatch)
END