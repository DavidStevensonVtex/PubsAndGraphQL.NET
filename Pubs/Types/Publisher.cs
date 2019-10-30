using Newtonsoft.Json;

namespace Pubs.Types
{
    public class Publisher
    {
        public Publisher(string pubId, string name, string city, string state, string country)
        {
            this.PubId = pubId;
            this.Name = name;
            this.City = city;
            this.State = state;
            this.Country = country;
        }

        [JsonProperty("pubId")]
        public string PubId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("titles")]
        public Title [] Titles { get; set; }
    }
}
