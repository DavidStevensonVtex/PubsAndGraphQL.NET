USE pubs ;
GO

SET NOCOUNT ON;

SELECT 'new Publisher( "' + pub_id + '", "' + pub_name + '", "' + city + '", "' + ISNULL(state, 'null') + '", "' + country + '" ),'
FROM dbo.publishers ;