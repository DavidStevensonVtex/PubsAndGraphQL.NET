using GraphQL;
using GraphQL.Types;
using Pubs.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// https://graphql-dotnet.github.io/docs/getting-started/introduction/
// See: Schema First Nested Types

namespace Pubs.SchemaFirst
{
    [GraphQLMetadata("Author", IsTypeOf = typeof(Author))]
    public class AuthorType
    {
        public string AuthorId(Author author) => author.AuthorId;
        public string LastName(Author author) => author.LastName;
        public string FirstName(Author author) => author.FirstName;
        public string Phone(Author author) => author.Phone;
        public string Address(Author author) => author.Address;
        public string City(Author author) => author.City;
        public string State(Author author) => author.State;
        public string Zip(Author author) => author.Zip;
        public bool Contract(Author author) => author.Contract;

        // these two parameters are optional
        // ResolveFieldContext provides contextual information about the field
        public Title[] Titles(ResolveFieldContext context, Author author)
        {
            string [] titleIds = PubsData.TitleAuthors
                .Where(ta => ta.AuthorId == author.AuthorId)
                .Select(ta => ta.TitleId)
                .ToArray();
            Title[] titles = PubsData.Titles.Where(t => titleIds.Contains(t.TitleId)).ToArray();
            return titles;
        }
    }
}
