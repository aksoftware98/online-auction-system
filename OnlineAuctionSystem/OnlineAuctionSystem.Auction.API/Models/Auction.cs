using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuctionSystem.Auction.API.Models
{
    public class Auction
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal StartPrice { get; set; }
        public DateTime AuctionDate { get; set; }
        public int Status { get; set; }
        public string Image { get; set; }
        public int ActiveInHours { get; set; }
        public decimal BidPrice { get; set; }
        public int IsActive { get; set; }
        public string Username { get; set; }
        public bool IsPaymentMade { get; set; }
        public string BidUserId { get; set; }
        public string BidId { get; set; }
        public string UserId { get; set; }
    }
}
