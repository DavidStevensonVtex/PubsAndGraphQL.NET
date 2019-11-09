using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pubs.Types
{
    public class TitlesQuery : ObjectGraphType<TitleType>
    {
        public TitlesQuery()
        { 
            Name = "AuthorsQuery";
            Description = "Return array of Titles";
            Field<ListGraphType<TitleType>>(
                "titles",
                description: "All titles",
                resolve: _ => PubsData.Titles.OrderBy(t => t.title));
        }
    }
}
