using System.Linq;
using GraphQL;
using GraphQL.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Pubs.Types;

namespace Pubs.UnitTests
{
    [TestClass]
    public class PublisherTest
    {
        [TestMethod]
        public void GraphTypeFirst_PublisherTest_GetBostonPublisher()
        {
            string publisherId = "0736";

            var schema = new Schema { Query = new PublisherQuery() };

            var json = schema.Execute(_ =>
            {
                _.Query = "query PublisherQuery { publisher(id: \"" + publisherId + "\") { pubId name city state country } }";
            });

            var d = JsonConvert.DeserializeObject<dynamic>(json);
            Assert.IsNull(d.errors);

            json = JsonConvert.SerializeObject(d.data.publisher);
            Publisher publisher = JsonConvert.DeserializeObject<Publisher>(json);
            Assert.IsNotNull(publisher);

            Publisher expectedPublisher = PubsData.Publishers.FirstOrDefault(p => p.PubId == publisherId);
            Assert.IsNotNull(expectedPublisher);

            Assert.AreEqual(expectedPublisher.PubId, (string)d.data.publisher.pubId);
            Assert.AreEqual(expectedPublisher.Name, (string)d.data.publisher.name);
            Assert.AreEqual(expectedPublisher.City, (string)d.data.publisher.city);
            Assert.AreEqual(expectedPublisher.State, (string)d.data.publisher.state);
            Assert.AreEqual(expectedPublisher.Country, (string)d.data.publisher.country);

            Assert.AreEqual(expectedPublisher.PubId, publisher.PubId);
            Assert.AreEqual(expectedPublisher.Name, publisher.Name);
            Assert.AreEqual(expectedPublisher.City, publisher.City);
            Assert.AreEqual(expectedPublisher.State, publisher.State);
            Assert.AreEqual(expectedPublisher.Country, publisher.Country);
        }

        [TestMethod]
        public void GraphTypeFirst_PublisherTest_GetBostonPublisher_UsingVariables()
        {
            string publisherId = "0736";

            var schema = new Schema { Query = new PublisherQuery() };

            var json = schema.Execute(_ =>
            {
                _.Query = "query PublisherQuery($publisherId: String!)  { publisher(id: $publisherId) { pubId name city state country } }";
                _.Inputs = ("{ \"publisherId\": \"" + publisherId + "\" }").ToInputs() ;
            });

            var d = JsonConvert.DeserializeObject<dynamic>(json);
            Assert.IsNull(d.errors);

            json = JsonConvert.SerializeObject(d.data.publisher);
            Publisher publisher = JsonConvert.DeserializeObject<Publisher>(json);
            Assert.IsNotNull(publisher);

            Publisher expectedPublisher = PubsData.Publishers.FirstOrDefault(p => p.PubId == publisherId);
            Assert.IsNotNull(expectedPublisher);

            Assert.AreEqual(expectedPublisher.PubId, (string)d.data.publisher.pubId);
            Assert.AreEqual(expectedPublisher.Name, (string)d.data.publisher.name);
            Assert.AreEqual(expectedPublisher.City, (string)d.data.publisher.city);
            Assert.AreEqual(expectedPublisher.State, (string)d.data.publisher.state);
            Assert.AreEqual(expectedPublisher.Country, (string)d.data.publisher.country);

            Assert.AreEqual(expectedPublisher.PubId, publisher.PubId);
            Assert.AreEqual(expectedPublisher.Name, publisher.Name);
            Assert.AreEqual(expectedPublisher.City, publisher.City);
            Assert.AreEqual(expectedPublisher.State, publisher.State);
            Assert.AreEqual(expectedPublisher.Country, publisher.Country);
        }

        [TestMethod]
        public void GraphTypeFirst_PublisherTest_GetBostonPublisher_WithTitles()
        {
            string publisherId = "0736";

            var schema = new Schema { Query = new PublisherQuery() };

            var json = schema.Execute(_ =>
            {
                //_.Query = "query PublisherQuery { publisher(id: \"0736\") { pubId name city state country titles { titleId title price notes pubId } } }";

                _.Query =
                    @"  query PublisherQuery
                        {
                            publisher(id: """ + publisherId + @""")
                            {
                                pubId name city state country
                                titles
                                {
                                    titleId title type pubDate price notes pubId advance royalty ytdSales
                                }
                            }
                        }";
            });

            var d = JsonConvert.DeserializeObject<dynamic>(json);
            Assert.IsNull(d.errors);

            json = JsonConvert.SerializeObject(d.data.publisher);
            Publisher publisher = JsonConvert.DeserializeObject<Publisher>(json);
            Assert.IsNotNull(publisher);

            Publisher expectedPublisher = PubsData.Publishers.FirstOrDefault(p => p.PubId == publisherId);
            Assert.IsNotNull(expectedPublisher);

            Assert.AreEqual(expectedPublisher.PubId, (string)d.data.publisher.pubId);
            Assert.AreEqual(expectedPublisher.Name, (string)d.data.publisher.name);
            Assert.AreEqual(expectedPublisher.City, (string)d.data.publisher.city);
            Assert.AreEqual(expectedPublisher.State, (string)d.data.publisher.state);
            Assert.AreEqual(expectedPublisher.Country, (string)d.data.publisher.country);

            Assert.AreEqual(expectedPublisher.PubId, publisher.PubId);
            Assert.AreEqual(expectedPublisher.Name, publisher.Name);
            Assert.AreEqual(expectedPublisher.City, publisher.City);
            Assert.AreEqual(expectedPublisher.State, publisher.State);
            Assert.AreEqual(expectedPublisher.Country, publisher.Country);

            // ********************************************* To Be Done ******************************************
            // Test all the individual Title fields for each title in the response
            // ***************************************************************************************************
        }
    }
}
