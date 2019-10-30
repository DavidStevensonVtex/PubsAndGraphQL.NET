using GraphQL.Types;

namespace Pubs.Types
{
    public class AuthorsQuery : ObjectGraphType<AuthorType>
    {
        public AuthorsQuery()
        {
            Name = "AuthorsQuery";
            Description = "Return array of Authors";
            Field<ListGraphType<AuthorType>>(
                "authors",
                description: "All authors",
                resolve: _ => PubsData.Authors);
        }
    }
}
