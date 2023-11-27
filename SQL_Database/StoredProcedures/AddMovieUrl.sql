CREATE PROCEDURE AddMovieUrl @guid varchar(1000), @indexNumber int, @imgUrl varchar(max)
as
IF EXISTS(SELECT * FROM MediaImgUrl where GUId = @guid)
BEGIN
	UPDATE MediaImgUrl
	SET indexNumber = @indexNumber, imgUrl = @imgUrl
	WHERE GUId = @guid
END
ELSE
BEGIN
	INSERT INTO MediaImgUrl(GUId, indexNumber, imgUrl)
	VALUES(@guid, @indexNumber, @imgUrl)
END