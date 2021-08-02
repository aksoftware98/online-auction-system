using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace OnlineAuctionSystem.Bids.API.Models
{
    public class Bid
    {
        [JsonProperty("id")]
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonProperty("auctionId")]
        [JsonPropertyName("auctionid")]
        public string AuctionId { get; set; }

        [JsonProperty("amount")]
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("userId")]
        [JsonPropertyName("userId")]
        public string UserId { get; set; }

        [JsonProperty("bidDate")]
        [JsonPropertyName("bidDate")]
        public DateTime BidDate { get; set; }
    }
}
