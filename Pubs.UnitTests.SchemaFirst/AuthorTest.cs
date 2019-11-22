using System.Linq;  
using GraphQL;
using GraphQL.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Pubs.SchemaFirst;
using Pubs.Types;

namespace Pubs.UnitTests.SchemaFirst
{
    [TestClass]
    public class AuthorTest
    {
        [TestMethod]
        public void SchemaFirst_AuthorTest_GetSingleAuthor()
        {
            string authorId = "899-46-2035";

            ISchema schema = PubsSchema.GetSchema();

            // The id parameter here has to match the id parameter in the C# GetAuthor method.
            string json = schema.Execute(_ =>
            {
                //_.Query = query;
                _.Query =
                    @"query AuthorQuery 
                    {
                        author(id: """ + authorId + @""") 
                        { 
                            authorId lastName firstName phone address city state zip contract 
                            titles
                            {
                                titleId title price pubId type pubDate notes advance royalty ytdSales 
                            }
                        } 
                    }";
            });

            var d = JsonConvert.DeserializeObject<dynamic>(json);
            Assert.IsNull(d.errors);

            json = JsonConvert.SerializeObject(d.data.author);
            Author author = JsonConvert.DeserializeObject<Author>(json);

            Author expectedAuthor = PubsData.Authors.FirstOrDefault(a => a.AuthorId == authorId);

            Assert.IsNotNull(expectedAuthor);
            Assert.IsNotNull(author);

            Assert.AreEqual(expectedAuthor.AuthorId, author.AuthorId);
            Assert.AreEqual(expectedAuthor.LastName, author.LastName);
            Assert.AreEqual(expectedAuthor.FirstName, author.FirstName);
            Assert.AreEqual(expectedAuthor.Phone, author.Phone);
            Assert.AreEqual(expectedAuthor.Address, author.Address);
            Assert.AreEqual(expectedAuthor.City, author.City);
            Assert.AreEqual(expectedAuthor.State, author.State);
            Assert.AreEqual(expectedAuthor.Zip, author.Zip);
            Assert.AreEqual(expectedAuthor.Contract, author.Contract);

            Assert.IsNotNull(author.Titles);
            Assert.AreEqual(2, author.Titles.Length);

            string[] titleIds = PubsData.TitleAuthors.Where(ta => ta.AuthorId == authorId).Select(ta => ta.TitleId).ToArray();
            Title[] expectedTitles = PubsData.Titles.Where(t => titleIds.Contains(t.TitleId)).ToArray();

            foreach (Title title in author.Titles)
            {
                Title expectedTitle = expectedTitles.FirstOrDefault(et => et.TitleId == title.TitleId);
                Assert.IsNotNull(expectedTitle);
                Assert.AreEqual(expectedTitle.TitleId, title.TitleId);
                Assert.AreEqual(expectedTitle.title, title.title);
                Assert.AreEqual(expectedTitle.Price, title.Price);
                Assert.AreEqual(expectedTitle.PubId, title.PubId);
                Assert.AreEqual(expectedTitle.Type, title.Type);
                Assert.AreEqual(expectedTitle.PubDate, title.PubDate);
                Assert.AreEqual(expectedTitle.Notes, title.Notes);
                Assert.AreEqual(expectedTitle.Advance, title.Advance);
                Assert.AreEqual(expectedTitle.Royalty, title.Royalty);
                Assert.AreEqual(expectedTitle.YTDSales, title.YTDSales);
            }

            foreach (Title expectedTitle in expectedTitles)
            {
                Title title = author.Titles.FirstOrDefault(t => t.TitleId == expectedTitle.TitleId);
                Assert.IsNotNull(title);
            }
        }
    }
}
