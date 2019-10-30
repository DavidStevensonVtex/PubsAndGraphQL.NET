USE pubs ;
GO

SET NOCOUNT ON;

SELECT 'new Author( "' + au_id + '", "' + au_lname + '", "' + au_fname + '", "' + phone + '", "' + address + '", "' + city + '", "' + state + '", "' + zip + '", ' +
	CASE contract WHEN 0 THEN 'false ),' ELSE 'true ),' END 
FROM dbo.authors;