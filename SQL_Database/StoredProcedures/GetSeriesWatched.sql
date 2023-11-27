CREATE PROCEDURE GetSeriesWatched @seriesGUId varchar(1000), @seasonGUId varchar(1000), @userId int
AS
IF EXISTS(SELECT planToWatch FROM SeriesWatched
WHERE userId = @userid AND seriesGUId = @seriesGUId AND seasonGUId = @seasonGUId)
BEGIN
	SELECT planToWatch FROM SeriesWatched
	WHERE userId = @userid AND seriesGUId = @seriesGUId AND seasonGUId = @seasonGUId
END
ELSE
BEGIN
	SELECT ''
end