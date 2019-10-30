using GraphQL;
using Pubs.Types;
using System.Linq;

namespace Pubs.SchemaFirst
{
    // Partial class is useful for source control purposes, to allow multiple people to work on the various query parts.
    public partial class Query
    {
        [GraphQLMetadata("title")]
        public Title GetTitle(string id)
        {
            return PubsData.Titles.FirstOrDefault(t => t.TitleId == id);
        }
    }
}
