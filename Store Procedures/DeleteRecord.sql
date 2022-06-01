CREATE PROCEDURE DeleteInformation(@Id int)
AS
BEGIN
	DELETE FROM CrudOperationTable WHERE Id=@Id;
END
GO
