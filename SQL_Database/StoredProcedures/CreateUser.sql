CREATE PROCEDURE CreateUser @username varchar(300), @password varchar(max), @email varchar(300)
AS
SET XACT_ABORT, NOCOUNT ON;

IF EXISTS(SELECT userName FROM UserTable WHERE userName = @username)
BEGIN 
	;THROW 99001, 'This username is already taken.', 1;
END

IF EXISTS (SELECT email FROM UserTable WHERE email = @email)
BEGIN
	;THROW 99002, 'This email is already in use.', 1;
END

INSERT INTO UserTable (userName, email, password)
VALUES (@username, @email, @password)

SELECT SCOPE_IDENTITY() 