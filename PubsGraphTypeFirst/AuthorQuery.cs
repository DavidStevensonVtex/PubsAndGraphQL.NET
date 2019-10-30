using System.Linq;
using GraphQL.Types;

namespace Pubs.Types
{
    public class AuthorQuery : ObjectGraphType<Author>
    {
        public AuthorQuery()
        {
            Name = "AuthorQuery";

            Field<AuthorType>(
                "author",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "id", Description = "id of the author" }
                ),
                resolve: context => {
                    string id = context.GetArgument<string>("id");
                    return PubsData.Authors.FirstOrDefault(p => p.AuthorId == id);
                }
            );
        }
    }
}
