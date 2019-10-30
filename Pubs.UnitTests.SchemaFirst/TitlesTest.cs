using System;
using System.Globalization;
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
    public class TitlesTest
    {
        private string ToDate(DateTime d)
        {
            return $"{d.Month:D2}/{d.Day:D2}/{d.Year:D4} {d.Hour:D2}:{d.Minute:D2}:{d.Second:D2}";
            //return $"{d.Year}-{d.Month:D2}-{d.Day:D2}";
        }

        [TestMethod]
        public void SchemaFirst_TitlesTest_GetAllTitles()
        {
            ISchema schema = PubsSchema.GetSchema();

            string json = schema.Execute(_ =>
            {
                //_.Query = $" {{ titles {{ titleId title pubId type pubDate notes price advance royalty ytdSales }} }}";
                _.Query =
                    @"query TitleQuery 
                    {
                        titles
                        { 
                            titleId title price pubId type pubDate notes advance royalty ytdSales 
                            authors { authorId lastName firstName phone address city state zip contract }
                            publisher { pubId name city state country }
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
