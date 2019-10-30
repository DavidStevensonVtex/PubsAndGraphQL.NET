using Newtonsoft.Json;

namespace Pubs.Types
{
    public class Author
    {
        public Author(string authorId, string lastName, string firstName, string phone, 
            string address, string city, string state, string zip, bool contract)
        {
            this.AuthorId = authorId;
            this.LastName = lastName;
            this.FirstName = firstName;
            this.Phone = phone;
            this.Address = address;
            this.City = city;
            this.State = state;
            this.Zip = zip;
            this.Contract = contract;
        }

        [JsonProperty("authorId")]
        public string AuthorId { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("zip")]
        public string Zip { get; set; }

        [JsonProperty("contract")]
        public bool Contract { get; set; }

        [JsonProperty("titles")]
        public Title[] Titles { get; set; }
    }
}
