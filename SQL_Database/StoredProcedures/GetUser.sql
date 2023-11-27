CREATE PROCEDURE GetUser @id INT
AS
SET XACT_ABORT, NOCOUNT ON;

IF EXISTS (SELECT userId, userName, password, email FROM UserTable WHERE userId = @id)
BEGIN
SELECT userId, userName, password, email FROM UserTable WHERE userId = @id
END
ELSE 
THROW 99004, 'The UserID does not exist.', 1;