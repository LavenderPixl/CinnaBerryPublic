CREATE PROCEDURE UpdateSeriesWatched 
@seriesGUId varchar(1000),
@seasonGUId varchar(1000),
@userId int,
@planToWatch bit
AS
IF EXISTS(SELECT * FROM SeriesWatched WHERE 
seriesGUId = @seriesGUId AND seasonGUId = @seasonGUId AND userId = @userid)
BEGIN
	UPDATE SeriesWatched
	SET planToWatch = @planToWatch
	WHERE seriesGUId = @seriesGUId AND seasonGUId = @seasonGUId AND userId = @userid
END
ELSE
BEGIN
	INSERT INTO SeriesWatched(seriesGUId, seasonGUId, userId, planToWatch)
	VALUES(@seriesGUId, @seasonGUId, @userid, @planToWatch)
END