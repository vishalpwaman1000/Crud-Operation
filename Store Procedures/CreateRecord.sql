CREATE PROCEDURE CreateInformation
(
	@UserName varchar(255),
	@Age int
)
AS
BEGIN
	
	INSERT INTO CrudOperationTable(UserName, Age) VALUES (@UserName, @Age);

END
GO
