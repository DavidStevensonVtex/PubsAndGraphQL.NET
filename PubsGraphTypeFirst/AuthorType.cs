using System.Collections.Generic;
using System.Linq;
using GraphQL.Types;

namespace Pubs.Types
{
    public class AuthorType : ObjectGraphType<Author>
    {
        public AuthorType()
        {
            Name = "Author";
            Description = "A writer of a book, article, song or report";

            Field<NonNullGraphType<IdGraphType>>("authorId");
            Field(d => d.AuthorId).Description("Author Id");
            Field(d => d.LastName).Description("Last Name");
            Field(d => d.FirstName).Description("First Name");
            Field(d => d.Phone).Description("Phone");
            Field(d => d.Address).Description("Address");
            Field(d => d.City).Description("City");
            Field(d => d.State).Description("State");
            Field(d => d.Zip).Description("Zip");
            Field(d => d.Contract).Description("Is Contract");

            Field<ListGraphType<TitleType>>(
                "titles",
                resolve: context =>
                {
                    List<string> titleIdList = PubsData.TitleAuthors
                        .Where(ta => ta.AuthorId == context.Source.AuthorId)
                        .Select(ta => ta.TitleId)
                        .ToList();
                    List<Title> titleList = PubsData.Titles.Where(t => titleIdList.Contains(t.TitleId)).ToList();
                    return titleList;
                }
            );
        }
    }
}
