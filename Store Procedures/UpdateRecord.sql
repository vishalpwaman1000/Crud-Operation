CREATE PROCEDURE UpdateInformation
(
	@Id int, 
	@UserName varchar(255), 
	@Age int
)
AS
BEGIN
	
	UPDATE CrudOperationTable
	SET UserName=@UserName, 
	    Age=@Age 
	WHERE Id=@Id;

END
GO
