using GraphQL;
using GraphQL.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Pubs.Types;
using System.Linq;

namespace Pubs.UnitTests
{
    [TestClass]
    public class AuthorsTest
    {
        [TestMethod]
        public void GraphTypeFirst_AuthorsTest_GetAllAuthors()
        {
            var schema = new Schema { Query = new AuthorsQuery() };

            var json = schema.Execute(_ =>
            {
                _.Query = "query AuthorsQuery { authors { authorId lastName firstName phone address city state zip contract } }";
            });

            var d = JsonConvert.DeserializeObject<dynamic>(json);

            Assert.IsNull(d.errors);
            json = JsonConvert.SerializeObject(d.data.authors);
            Author[] authors = JsonConvert.DeserializeObject<Author[]>(json);

            foreach ( Author expected in PubsData.Authors)
            {
                Author author = authors.FirstOrDefault(a => a.AuthorId == expected.AuthorId);
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
            }

            foreach ( Author author in authors)
            {
                Author expected = PubsData.Authors.FirstOrDefault(a => a.AuthorId == author.AuthorId);
                Assert.IsNotNull(expected);
            }
        }
    }
}
