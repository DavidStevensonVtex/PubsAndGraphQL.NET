using GraphQL.Types;

namespace Pubs.Types
{
    public class PublishersQuery : ObjectGraphType<PublisherType>
    {
        public PublishersQuery()
        {
            Name = "PublishersQuery";
            Description = "Return array of Publishers";
            Field<ListGraphType<PublisherType>>(
                "publishers",
                description: "All publishers",
                resolve: _ => PubsData.Publishers);
        }
    }
}
