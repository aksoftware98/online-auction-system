using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineAuctionSystem.Auction.API.Repositories
{
    public interface IAuctionsRepository
    {
        Task<Models.Auction> GetByIdAsync(int id);
        Task<Models.Auction> AddAsync(Models.Auction auction);
        Task<Models.Auction> UpdateAsync(Models.Auction auction);
        Task<IEnumerable<Models.Auction>> GetAllAsync();
        Task<IEnumerable<Models.Auction>> GetAllByUserIdAsync(string userId);
        Task<IEnumerable<Models.Auction>> GetAllByBidUserIdAsync(string bidUserId);

    }

}
