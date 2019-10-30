USE pubs ;
GO

SET NOCOUNT ON;

SELECT 'new Title( "' + title_id + '", "' + title + '", "' + RTRIM(LTRIM(type)) + '", "' + pub_id + '", ' + ISNULL(CAST(price AS VARCHAR) + 'm', 'null') + ', ' 
	+ ISNULL(CAST(advance AS VARCHAR) +'m', 'null') + ', ' 
	+ ISNULL(CAST(royalty AS VARCHAR), 'null') + ', '
	+ ISNULL(CAST(ytd_sales AS VARCHAR), 'null') + ', ' 
	+ ISNULL('"' + notes + '"', 'null')
	+ ', new DateTime( ' + CAST(YEAR(pubdate) AS VARCHAR) + ', ' + CAST(MONTH(pubdate) AS VARCHAR) + ', ' + CAST(DAY(pubdate) AS VARCHAR) + ') ),'

FROM dbo.titles; 