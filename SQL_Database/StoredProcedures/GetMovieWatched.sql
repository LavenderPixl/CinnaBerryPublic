CREATE PROCEDURE GetMovieWatched @guid varchar(max), @userid int
as
IF EXISTS(SELECT planToWatch FROM MoviesWatched
WHERE userId = @userid and movieGUId = @guid)
BEGIN
	SELECT planToWatch FROM MoviesWatched
	WHERE userId = @userid and movieGUId = @guid
END
ELSE
BEGIN
	SELECT ''
end
