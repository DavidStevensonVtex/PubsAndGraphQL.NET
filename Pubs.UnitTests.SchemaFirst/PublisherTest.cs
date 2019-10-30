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
    public class PublisherTest
    {
        [TestMethod]
        public void SchemaFirst_PublisherTest_GetSinglePublisher()
        {
            string publisherId = "0736";

            ISchema schema = PubsSchema.GetSchema();

            // The id parameter here has to match the id parameter in the C# GetAuthor method.
            string json = schema.Execute(_ =>
            {
                //_.Query = $" {{ publisher(id: \"{publisherId}\") {{ pubId name city state country }} }}";
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
        }
    }
}
