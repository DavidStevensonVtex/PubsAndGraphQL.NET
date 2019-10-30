using GraphQL.Types;
using System.Linq;

namespace Pubs.Types
{
    public class TitleType : ObjectGraphType<Title>
    {
        public TitleType()
        {
            Name = "Title";
            Description = "Books, journals, music, or other works.";

            Field<NonNullGraphType<IdGraphType>>("titleId");
            Field(d => d.TitleId).Description("Title Id");
            Field(d => d.title).Name("title").Description("Title");
            Field(d => d.Notes, nullable: true).Description("Title notes");
            Field(d => d.Price, nullable: true).Description("Price");
            Field(d => d.PubId).Description("Publisher Id");
            Field(d => d.Type).Description("Genre of publication. Examples: business, mod_cook, popular_comp, pyschology, trad_cook, UNDECIDE.");
            Field(d => d.PubDate).Description("Publication Date");
            Field(d => d.Advance, nullable: true).Description("Advance");
            Field(d => d.Royalty, nullable: true).Description("Royalty");
            Field(d => d.YTDSales, nullable: true).Name("ytdSales").Description("Year To Date Sales");

            Field<PublisherType>(
                "publisher",
                resolve: context =>
                {
                    return PubsData.Publishers.FirstOrDefault(p => p.PubId == context.Source.PubId);
                });

            Field<ListGraphType<AuthorType>>(
                "authors",
                resolve: context =>
                {
                    string[] authorIds = PubsData.TitleAuthors
                        .Where(ta => ta.TitleId == context.Source.TitleId)
                        .Select(ta => ta.AuthorId)
                        .ToArray();
                    Author[] authors = PubsData.Authors.Where(a => authorIds.Contains(a.AuthorId)).ToArray();
                    return authors;
                }
            );
        }
    }
}
