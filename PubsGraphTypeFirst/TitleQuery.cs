using System.Linq;
using GraphQL.Types;

namespace Pubs.Types
{
    public class TitleQuery : ObjectGraphType<Title>
    {
        public TitleQuery()
        {
            Name = "TitleQuery";

            Field<TitleType>(
                "title",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "id", Description = "id of the title" }
                ),
                resolve: context => {
                    string id = context.GetArgument<string>("id");
                    return PubsData.Titles.FirstOrDefault(t => t.TitleId == id);
                }
            );
        }
    }
}
