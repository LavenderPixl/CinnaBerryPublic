CREATE PROCEDURE DeleteMoviewatched @guid varchar(1000), @id int
as
	DELETE FROM MoviesWatched where movieGUId = @guid AND userId = @id