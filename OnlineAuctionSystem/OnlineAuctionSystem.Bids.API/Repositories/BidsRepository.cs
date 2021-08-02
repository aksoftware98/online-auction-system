using OnlineAuctionSystem.Bids.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;

namespace OnlineAuctionSystem.Bids.API.Repositories
{
    public class BidsRepository : IBidsRepository
    {

        private Database _db;
        private Container _container; 

        public BidsRepository(string connectionString)
        {
            var client = new CosmosClient(connectionString);
            _db = client.GetDatabase("oas-bids-db");
            _container = _db.GetContainer("bids");
        }

        public async Task<Bid> CreateBidAsync(Bid model)
        {
            model.Id = Guid.NewGuid().ToString();
            var result = await _container.CreateItemAsync(model);

            return result.Resource;
        }

        public async Task<IEnumerable<Bid>> GetBidsByAuctionIdAsync(string auctionId)
        {
            if (auctionId == null)
                throw new ArgumentNullException(nameof(auctionId));

            string query = $"SELECT * FROM b WHERE b.auctionId = {auctionId}";

            var iterator = _container.GetItemQueryIterator<Bid>(query);

            var result = await iterator.ReadNextAsync();
            var bids = new List<Bid>();
            bids.AddRange(result.Resource);
            while (result.ContinuationToken != null)
            {
                iterator = _container.GetItemQueryIterator<Bid>(query, result.ContinuationToken);
                result = await iterator.ReadNextAsync();
                bids.AddRange(result.Resource);
            }

            return bids;
        }
    }
}
