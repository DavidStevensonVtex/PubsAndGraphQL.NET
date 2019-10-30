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
    public class PublishersTest
    {
        [TestMethod]
        public void GraphTypeFirst_PublishersTest_GetAllPublishers()
        {
            var schema = new Schema { Query = new PublishersQuery() };

            var json = schema.Execute(_ =>
            {
                _.Query = "query PublishersQuery { publishers { pubId name city state country } }";
            });

            var d = JsonConvert.DeserializeObject<dynamic>(json);

            Assert.IsNull(d.errors);
            json = JsonConvert.SerializeObject(d.data.publishers);
            Publisher[] publishers = JsonConvert.DeserializeObject<Publisher[]>(json);

            foreach (Publisher expected in PubsData.Publishers)
            {
                Publisher publisher = publishers.FirstOrDefault(a => a.PubId == expected.PubId);
                Assert.IsNotNull(publisher);

                Assert.AreEqual(expected.PubId, publisher.PubId);
                Assert.AreEqual(expected.Name, publisher.Name);
                Assert.AreEqual(expected.City, publisher.City);
                Assert.AreEqual(expected.State, publisher.State);
                Assert.AreEqual(expected.Country, publisher.Country);
            }

            foreach (Publisher publisher in publishers)
            {
                Publisher expected = PubsData.Publishers.FirstOrDefault(a => a.PubId == publisher.PubId);
                Assert.IsNotNull(expected);
            }
        }
    }
}
