using OnlineAuctionSystem.Bids.API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuctionSystem.Bids.API.Repositories
{
    public interface IBidsRepository
    {
        Task<IEnumerable<Bid>> GetBidsByAuctionIdAsync(string id);
        Task<Bid> CreateBidAsync(Bid model);

    }
}
