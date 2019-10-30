USE pubs ;
GO

SET NOCOUNT ON;

SELECT 'new TitleAuthor( "' + au_id + '", "' + title_id + '", ' + CAST(au_ord AS NVARCHAR) + ', ' + CAST(royaltyper AS NVARCHAR) + ' ),'
FROM dbo.titleauthor ;