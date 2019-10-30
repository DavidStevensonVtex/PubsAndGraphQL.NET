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
    public class PublishersTest
    {
        [TestMethod]
        public void SchemaFirst_PublishersTest_GetAllPublishers()
        {
            var schema = Schema.For(PubsSchema.Schema, _ =>
            {
                _.Types.Include<Query>();
            });

            string query = " { publishers { pubId name city state country } }";
            string json = schema.Execute(_ =>
            {
                _.Query = query;
            });

            var d = JsonConvert.DeserializeObject<dynamic>(json);
            Assert.IsNull(d.errors);

            json = JsonConvert.SerializeObject(d.data.publishers);
            Publisher[] publishers = JsonConvert.DeserializeObject<Publisher[]>(json);
            Assert.IsNotNull(publishers);

            foreach (Publisher publisher in publishers)
            {
                Publisher expectedPublisher = PubsData.Publishers.FirstOrDefault(p => p.PubId == publisher.PubId);
                Assert.IsNotNull(expectedPublisher);

                Assert.AreEqual(expectedPublisher.PubId, publisher.PubId);
                Assert.AreEqual(expectedPublisher.Name, publisher.Name);
                Assert.AreEqual(expectedPublisher.City, publisher.City);
                Assert.AreEqual(expectedPublisher.State, publisher.State);
                Assert.AreEqual(expectedPublisher.Country, publisher.Country);
            }

            foreach (Publisher publisher in PubsData.Publishers)
            {
                Publisher expectedPublisher = publishers.FirstOrDefault(p => p.PubId == publisher.PubId);
                Assert.IsNotNull(expectedPublisher);

                Assert.AreEqual(expectedPublisher.PubId, publisher.PubId);
                Assert.AreEqual(expectedPublisher.Name, publisher.Name);
                Assert.AreEqual(expectedPublisher.City, publisher.City);
                Assert.AreEqual(expectedPublisher.State, publisher.State);
                Assert.AreEqual(expectedPublisher.Country, publisher.Country);
            }
        }
    }
}
