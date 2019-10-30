using System.Linq;
using GraphQL.Types;

namespace Pubs.Types
{
    public class PublisherQuery : ObjectGraphType<Publisher>
    {
        public PublisherQuery()
        {
            Name = "PublisherQuery";

            Field<PublisherType>(
                "publisher",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "id", Description = "id of the publisher" }
                ),
                resolve: context => {
                    string id = context.GetArgument<string>("id");
                    return PubsData.Publishers.FirstOrDefault(p => p.PubId == id);
                }
            );
        }
    }
}
