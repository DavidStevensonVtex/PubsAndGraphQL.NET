using GraphQL;
using Pubs.Types;
using System.Linq;

namespace Pubs.SchemaFirst
{
    // Partial class is useful for source control purposes, to allow multiple people to work on the various query parts.
    public partial class Query
    {
        [GraphQLMetadata("author")]
        public Author GetAuthor(string id)
        {
            return PubsData.Authors.FirstOrDefault(a => a.AuthorId == id);
        }
    }
}
