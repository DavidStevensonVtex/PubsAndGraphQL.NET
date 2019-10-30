using System.Collections.Generic;
using System.Linq;
using GraphQL.Types;

namespace Pubs.Types
{
    public class PublisherType : ObjectGraphType<Publisher>
    {
        public PublisherType()
        {
            Name = "Publisher";
            Description = "A person or company that prepares and issues books, journals, music, or other works for sale.";

            Field<NonNullGraphType<IdGraphType>>("pubId");
            Field(d => d.PubId).Description("Publisher Id");
            Field(d => d.Name).Name("Name").Description("Publisher Name");
            Field(d => d.City).Description("Publisher City");
            Field(d => d.State).Description("Publisher State");
            Field(d => d.Country).Description("Publisher Country");

            Field<ListGraphType<TitleType>>(
                "titles",
                resolve: context =>
                {
                    List<Title> titleList = PubsData.Titles.Where(t => t.PubId == context.Source.PubId).ToList();
                    return titleList;
                }
            );
        }
    }
}
