using Newtonsoft.Json;
using System;

namespace Pubs.Types
{
    public class Title
    {
        public Title ( string titleId, string pubTitle, string type, string pubId, decimal? price, 
            decimal? advance, int? royalty, int? ytdSales, string notes, DateTime pubDate)
        {
            this.TitleId = titleId;
            this.title = pubTitle;
            this.Type = type;
            this.PubId = pubId;
            this.Price = price;
            this.Advance = advance;
            this.Royalty = royalty;
            this.YTDSales = ytdSales;
            this.Notes = notes;
            this.PubDate = pubDate;
        }

        [JsonProperty("titleId")]
        public string TitleId { get; set; }

        [JsonProperty("title")]
        // The following hack should work for both Graph Type First and Schema First approaches.
        public string title { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("pubId")]
        public string PubId { get; set; }

        [JsonProperty("price")]
        public decimal? Price { get; set; }

        [JsonProperty("advance")]
        public decimal? Advance { get; set; }

        [JsonProperty("royalty")]
        public int? Royalty { get; set; }

        [JsonProperty("ytdSales")]
        public int? YTDSales { get; set; }

        [JsonProperty("notes")]
        public string Notes { get; set; }

        [JsonProperty("pubDate")]
        public DateTime PubDate { get; set; }

        [JsonProperty("publisher")]
        public Publisher Publisher { get; set; }

        [JsonProperty("authors")]
        public Author[] Authors { get; set; }
    }
}
