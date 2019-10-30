using GraphQL;
using GraphQL.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Pubs.Types;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Pubs.UnitTests
{
    [TestClass]
    public class TitleTest
    {
        private string ToDate ( DateTime d )
        {
            return $"{d.Year}-{d.Month:D2}-{d.Day:D2}";
        }

        [TestMethod]
        public void GraphTypeFirst_TitleTest_GetATitle()
        {
            string titleId = "PC1035";
            var schema = new Schema { Query = new TitleQuery() };

            var json = schema.Execute(_ =>
            {
                _.Query =
                    @"query TitleQuery 
                    {
                        title(id: """ + titleId + @""") 
                        { 
                            titleId title price pubId type pubDate notes advance royalty ytdSales 
                            authors { authorId lastName firstName phone address city state zip contract }
                            publisher { pubId name city state country }
                        } 
                    }";
            });

            var d = JsonConvert.DeserializeObject<dynamic>(json);
            Assert.IsNull(d.errors);

            Title expectedTitle = PubsData.Titles.FirstOrDefault(t => t.TitleId == titleId);
            Assert.IsNotNull(expectedTitle);

            Assert.AreEqual(expectedTitle.TitleId, (string)d.data.title.titleId);
            Assert.AreEqual(expectedTitle.title, (string)d.data.title.title);
            Assert.AreEqual(expectedTitle.Price, (decimal?)d.data.title.price);
            Assert.AreEqual(expectedTitle.PubId, (string)d.data.title.pubId);
            Assert.AreEqual(expectedTitle.Type, (string)d.data.title.type);
            Assert.AreEqual(ToDate(expectedTitle.PubDate), (string)d.data.title.pubDate);
            Assert.AreEqual(expectedTitle.Notes, (string)d.data.title.notes);
            Assert.AreEqual(expectedTitle.Advance, (decimal?)d.data.title.advance);
            Assert.AreEqual(expectedTitle.Royalty, (int?)d.data.title.royalty);
            Assert.AreEqual(expectedTitle.YTDSales, (int?)d.data.title.ytdSales);

            json = JsonConvert.SerializeObject(d.data.title);
            Title title = JsonConvert.DeserializeObject<Title>(json);
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

            Assert.IsNotNull(title.Authors);
            Assert.AreEqual(1, title.Authors.Length);
            Author author = title.Authors[0];
            Assert.IsNotNull(author);
            Author expected = PubsData.Authors.FirstOrDefault(a => a.AuthorId == author.AuthorId);
            Assert.IsNotNull(expected);

            Assert.AreEqual(expected.AuthorId, author.AuthorId);
            Assert.AreEqual(expected.LastName, author.LastName);
            Assert.AreEqual(expected.FirstName, author.FirstName);
            Assert.AreEqual(expected.Phone, author.Phone);
            Assert.AreEqual(expected.Address, author.Address);
            Assert.AreEqual(expected.City, author.City);
            Assert.AreEqual(expected.State, author.State);
            Assert.AreEqual(expected.Zip, author.Zip);
            Assert.AreEqual(expected.Contract, author.Contract);

            Publisher publisher = title.Publisher;
            Publisher expectedPublisher = PubsData.Publishers.FirstOrDefault(p => p.PubId == title.PubId);
            // Note: there is one title that has no publisher. So the data is bad. But that doesn't mean the unit test should fail.
            Assert.IsNotNull(title.Publisher);
            Assert.IsNotNull(expectedPublisher);
            if (!((publisher == null) && (expectedPublisher == null)))
            {
                Assert.IsNotNull(title.Publisher);
                Assert.IsNotNull(expectedPublisher);

                Assert.AreEqual(expectedPublisher.PubId, publisher.PubId);
                Assert.AreEqual(expectedPublisher.Name, publisher.Name);
                Assert.AreEqual(expectedPublisher.City, publisher.City);
                Assert.AreEqual(expectedPublisher.State, publisher.State);
                Assert.AreEqual(expectedPublisher.Country, publisher.Country);
            }
        }

        [TestMethod]
        public void GraphTypeFirst_TitleTest_GetATitle_WithNullValues()
        {
            string titleId = "TC7777";
            var schema = new Schema { Query = new TitleQuery() };

            var json = schema.Execute(_ =>
            {
                _.Query = 
                    @"query TitleQuery 
                    {
                        title(id: """ + titleId + @""") 
                        { 
                            titleId title price pubId type pubDate notes advance royalty ytdSales 
                            authors { authorId lastName firstName phone address city state zip contract }
                            publisher { pubId name city state country }
                        } 
                    }";
            });

            var d = JsonConvert.DeserializeObject<dynamic>(json);
            Assert.IsNull(d.errors);

            Title expectedTitle = PubsData.Titles.FirstOrDefault(t => t.TitleId == titleId);
            Assert.IsNotNull(expectedTitle);

            Assert.AreEqual(expectedTitle.TitleId, (string)d.data.title.titleId);
            Assert.AreEqual(expectedTitle.title, (string)d.data.title.title);
            Assert.AreEqual(expectedTitle.Price, (decimal?)d.data.title.price);
            Assert.AreEqual(expectedTitle.PubId, (string)d.data.title.pubId);
            Assert.AreEqual(expectedTitle.Type, (string)d.data.title.type);
            Assert.AreEqual(ToDate(expectedTitle.PubDate), (string)d.data.title.pubDate);
            Assert.AreEqual(expectedTitle.Notes, (string)d.data.title.notes);
            Assert.AreEqual(expectedTitle.Advance, (decimal?)d.data.title.advance);
            Assert.AreEqual(expectedTitle.Royalty, (int?)d.data.title.royalty);
            Assert.AreEqual(expectedTitle.YTDSales, (int?)d.data.title.ytdSales);

            json = JsonConvert.SerializeObject(d.data.title);
            Title title = JsonConvert.DeserializeObject<Title>(json);
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

            Assert.IsNotNull(title.Authors);
            Assert.AreEqual(3, title.Authors.Length);
            foreach (Author author in title.Authors)
            {
                Author expectedAuthor = PubsData.Authors.FirstOrDefault(a => a.AuthorId == author.AuthorId);
                Assert.IsNotNull(expectedAuthor);

                Assert.AreEqual(expectedAuthor.AuthorId, author.AuthorId);
                Assert.AreEqual(expectedAuthor.LastName, author.LastName);
                Assert.AreEqual(expectedAuthor.FirstName, author.FirstName);
                Assert.AreEqual(expectedAuthor.Phone, author.Phone);
                Assert.AreEqual(expectedAuthor.Address, author.Address);
                Assert.AreEqual(expectedAuthor.City, author.City);
                Assert.AreEqual(expectedAuthor.State, author.State);
                Assert.AreEqual(expectedAuthor.Zip, author.Zip);
                Assert.AreEqual(expectedAuthor.Contract, author.Contract);
            }

            Publisher publisher = title.Publisher;
            Publisher expectedPublisher = PubsData.Publishers.FirstOrDefault(p => p.PubId == title.PubId);
            // Note: there is one title that has no publisher. So the data is bad. But that doesn't mean the unit test should fail.
            Assert.IsNotNull(title.Publisher);
            Assert.IsNotNull(expectedPublisher);
            if (!((publisher == null) && (expectedPublisher == null)))
            {
                Assert.IsNotNull(title.Publisher);
                Assert.IsNotNull(expectedPublisher);

                Assert.AreEqual(expectedPublisher.PubId, publisher.PubId);
                Assert.AreEqual(expectedPublisher.Name, publisher.Name);
                Assert.AreEqual(expectedPublisher.City, publisher.City);
                Assert.AreEqual(expectedPublisher.State, publisher.State);
                Assert.AreEqual(expectedPublisher.Country, publisher.Country);
            }
        }

        [TestMethod]
        public void GraphTypeFirst_TitleTest_GetATitle_UsingVariables()
        {
            string titleId = "PC1035";
            var schema = new Schema { Query = new TitleQuery() };
            string inputString = @"{ ""titleId"": """ + titleId + @""" }";

            var json = schema.Execute(_ =>
            {
                //_.Query = "query TitleQuery($titleId: String!) { title(id: $titleId) { titleId title price pubId type pubDate notes advance royalty ytdSales } }";
                _.Query =
                    @"query TitleQuery 
                    {
                        title(id: """ + titleId + @""") 
                        { 
                            titleId title price pubId type pubDate notes advance royalty ytdSales 
                            publisher { pubId name city state country }
                            authors { authorId lastName firstName phone address city state zip contract }
                        } 
                    }";
                _.Inputs = inputString.ToInputs();
            });

            var d = JsonConvert.DeserializeObject<dynamic>(json);
            Assert.IsNull(d.errors);

            Assert.AreEqual("PC1035", (string)d.data.title.titleId);
            Assert.AreEqual("But Is It User Friendly?", (string)d.data.title.title);
            Assert.AreEqual(22.95m, (decimal)d.data.title.price);
            Assert.AreEqual("1389", (string)d.data.title.pubId);
            Assert.AreEqual("popular_comp", (string)d.data.title.type);
            Assert.AreEqual("1991-06-30", (string)d.data.title.pubDate);
            Assert.AreEqual("A survey of software for the naive user, focusing on the 'friendliness' of each.", (string)d.data.title.notes);
            Assert.AreEqual(7000.00m, (decimal)d.data.title.advance);
            Assert.AreEqual(16, (int)d.data.title.royalty);
            Assert.AreEqual(8780, (int)d.data.title.ytdSales);

            json = JsonConvert.SerializeObject(d.data.title);
            Title title = JsonConvert.DeserializeObject<Title>(json);
            Assert.AreEqual("PC1035", title.TitleId);
            Assert.AreEqual("But Is It User Friendly?", title.title);
            Assert.AreEqual(22.95m, title.Price);
            Assert.AreEqual("1389", title.PubId);
            Assert.AreEqual("popular_comp", title.Type);
            Assert.AreEqual(new DateTime(1991, 6, 30), title.PubDate);
            Assert.AreEqual("A survey of software for the naive user, focusing on the 'friendliness' of each.", title.Notes);
            Assert.AreEqual(7000.00m, title.Advance);
            Assert.AreEqual(16, title.Royalty);
            Assert.AreEqual(8780, title.YTDSales);
        }
    }
}