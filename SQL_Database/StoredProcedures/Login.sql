CREATE PROCEDURE Login @username varchar(300), @password varchar(max)
AS
SET XACT_ABORT, NOCOUNT ON;

IF EXISTS (SELECT userId FROM UserTable WHERE userName = @username AND password = @password)
BEGIN
SELECT userId from UserTable WHERE userName =  @username AND password = @password
END
ELSE 
THROW 99003, 'The username or password is incorrect.', 1;