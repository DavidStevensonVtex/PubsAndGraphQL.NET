using System.Linq;  
using GraphQL;
using GraphQL.Types;
using Pubs.Types;

// https://graphql-dotnet.github.io/docs/getting-started/introduction/
// See: Schema First Nested Types

namespace Pubs.SchemaFirst
{
    [GraphQLMetadata("Publisher", IsTypeOf = typeof(Publisher))]
    public class PublisherType
    {
        public string PubId(Publisher publisher) => publisher.PubId;
        public string Name(Publisher publisher) => publisher.Name;
        public string City(Publisher publisher) => publisher.City;
        public string State(Publisher publisher) => publisher.State;
        public string Country(Publisher publisher) => publisher.Country;

        // these two parameters are optional
        // ResolveFieldContext provides contextual information about the field
        public Title[] titles(ResolveFieldContext context, Publisher publisher)
        {
            Title[] titles = PubsData.Titles.Where(t => t.PubId == publisher.PubId).ToArray();
            return titles;
        }

    }
}
