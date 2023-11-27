CREATE PROCEDURE DeleteSeriesWatched @seriesGUId varchar(1000), @seasonGUId varchar(1000), @id int
as
	DELETE FROM SeriesWatched where seriesGUId = @seriesGUId AND seasonGUId = @seasonGUId AND userId = @id