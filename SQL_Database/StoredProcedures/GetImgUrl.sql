CREATE PROCEDURE GetImgUrl @guid varchar(1000)
as
IF EXISTS(SELECT * FROM MediaImgUrl WHERE GUId = @guid)
BEGIN
	SELECT imgUrl FROM MediaImgUrl 
	WHERE GUId = @guid 
END
ELSE
BEGIN
	SELECT imgUrl FROM MediaImgUrl 
	WHERE GUId = 'placeholder image'
END