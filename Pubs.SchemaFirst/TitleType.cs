using GraphQL;
using GraphQL.Types;
using Pubs.Types;
using System;
using System.Linq;


// https://graphql-dotnet.github.io/docs/getting-started/introduction/
// See: Schema First Nested Types

namespace Pubs.SchemaFirst
{
    [GraphQLMetadata("Title", IsTypeOf = typeof(Title))]
    public class TitleType
    {
        public string TitleId(Title t) => t.TitleId;

        public string title(Title t) => t.title;

        public string Type(Title t) => t.Type;

        public string PubId(Title t) => t.PubId;

        public decimal? Price(Title t) => t.Price;

        public decimal? Advance(Title t) => t.Advance;

        public int? Royalty(Title t) => t.Royalty;

        public int? YTDSales(Title t) => t.YTDSales;

        public string Notes(Title t) => t.Notes;

        public DateTime PubDate(Title t) => t.PubDate;

        // these two parameters are optional
        // ResolveFieldContext provides contextual information about the field
        public Author[] Authors(ResolveFieldContext context, Title title)
        {
            string[] authorIds = PubsData.TitleAuthors
                .Where(ta => ta.TitleId == title.TitleId)
                .Select(ta => ta.AuthorId)
                .ToArray();
            Author[] authors = PubsData.Authors.Where(a => authorIds.Contains(a.AuthorId)).ToArray();
            return authors;
        }

        public Publisher Publisher(ResolveFieldContext context, Title title)
        {
            Publisher publisher = PubsData.Publishers.FirstOrDefault(p => p.PubId == title.PubId);
            return publisher;
        }
    }
}
