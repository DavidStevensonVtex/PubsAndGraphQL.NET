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
    public class AuthorTest
    {
        [TestMethod]
        public void GraphTypeFirst_AuthorTest_GetAnAuthor()
        {
            var schema = new Schema { Query = new AuthorQuery() };

            var json = schema.Execute(_ =>
            {
                _.Query =
                    @"query AuthorQuery 
                    {
                        author(id: ""527-72-3246"") 
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

            Author expected = PubsData.Authors.FirstOrDefault(a => a.AuthorId == "527-72-3246");
            Assert.IsNotNull(expected);

            json = JsonConvert.SerializeObject(d.data.author);
            Author author = JsonConvert.DeserializeObject<Author>(json);
            Assert.IsNotNull(author);

            Assert.AreEqual(expected.AuthorId, author.AuthorId);
            Assert.AreEqual(expected.LastName, author.LastName);
            Assert.AreEqual(expected.FirstName, author.FirstName);
            Assert.AreEqual(expected.Phone, author.Phone);
            Assert.AreEqual(expected.Address, author.Address);
            Assert.AreEqual(expected.City, author.City);
            Assert.AreEqual(expected.State, author.State);
            Assert.AreEqual(expected.Zip, author.Zip);
            Assert.AreEqual(expected.Contract, author.Contract);

            Assert.IsNotNull(author.Titles);
            Assert.AreEqual(0, author.Titles.Length);
        }

        [TestMethod]
        public void GraphTypeFirst_AuthorTest_GetAnAuthor_UsingVariables()
        {
            string authorId = "899-46-2035";
            string input = (@"{ ""authorId"": """ + authorId + @""" }");
            var schema = new Schema { Query = new AuthorQuery() };

            var json = schema.Execute(_ =>
            {
                _.Query =
                @"query AuthorQuery($authorId: String!) 
                {   author(id: $authorId) 
                    {
                        authorId lastName firstName phone address city state zip contract 
                        titles
                        {
                            titleId title price pubId type pubDate notes advance royalty ytdSales
                        }
                    } 
                }";
                _.Inputs = input.ToInputs();
            });

            var d = JsonConvert.DeserializeObject<dynamic>(json);

            Assert.IsNull(d.errors);

            Author expected = PubsData.Authors.FirstOrDefault(a => a.AuthorId == authorId);
            Assert.IsNotNull(expected);

            json = JsonConvert.SerializeObject(d.data.author);
            Author author = JsonConvert.DeserializeObject<Author>(json);
            Assert.IsNotNull(author);

            Assert.AreEqual(expected.AuthorId, author.AuthorId);
            Assert.AreEqual(expected.LastName, author.LastName);
            Assert.AreEqual(expected.FirstName, author.FirstName);
            Assert.AreEqual(expected.Phone, author.Phone);
            Assert.AreEqual(expected.Address, author.Address);
            Assert.AreEqual(expected.City, author.City);
            Assert.AreEqual(expected.State, author.State);
            Assert.AreEqual(expected.Zip, author.Zip);
            Assert.AreEqual(expected.Contract, author.Contract);

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