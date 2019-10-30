using GraphQL;
using GraphQL.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Pubs.Types;
using System.Linq;

namespace Pubs.UnitTests
{
    [TestClass]
    public class TitlesTest
    {
        [TestMethod]
        public void GraphTypeFirst_TitlesTest_GetAllTitles()
        {
            var schema = new Schema { Query = new TitlesQuery() };

            var json = schema.Execute(_ =>
            {
                _.Query =
                        @"query TitlesQuery 
                        {
                            titles 
                            {
                                titleId title price pubId type pubDate notes advance royalty ytdSales 
                                publisher { pubId name city state country }
                                authors { authorId lastName firstName phone address city state zip contract }
                            } 
                        }";
            });

            var d = JsonConvert.DeserializeObject<dynamic>(json);

            Assert.IsNull(d.errors);
            json = JsonConvert.SerializeObject(d.data.titles);
            Title[] titles = JsonConvert.DeserializeObject<Title[]>(json);

            foreach (Title expected in PubsData.Titles)
            {
                Title title = titles.FirstOrDefault(a => a.TitleId == expected.TitleId);
                Assert.IsNotNull(title);

                Assert.AreEqual(expected.TitleId, title.TitleId);
                Assert.AreEqual(expected.title, title.title);
                Assert.AreEqual(expected.Price, title.Price);
                Assert.AreEqual(expected.PubId, title.PubId);
                Assert.AreEqual(expected.Type, title.Type);
                Assert.AreEqual(expected.PubDate, title.PubDate);
                Assert.AreEqual(expected.Notes, title.Notes);
                Assert.AreEqual(expected.Advance, title.Advance);
                Assert.AreEqual(expected.Royalty, title.Royalty);
                Assert.AreEqual(expected.YTDSales, title.YTDSales);
            }

            foreach (Title title in titles)
            {
                Title expected = PubsData.Titles.FirstOrDefault(a => a.TitleId == title.TitleId);
                Assert.IsNotNull(expected);
            }
        }
    }
}
