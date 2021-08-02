using Microsoft.AspNetCore.Mvc;
using OnlineAuctionSystem.Bids.API.Models;
using OnlineAuctionSystem.Bids.API.Repositories;
using System.Threading.Tasks;

namespace OnlineAuctionSystem.Bids.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BidsController : ControllerBase
    {

        private readonly IBidsRepository _bidsRepo;

        public BidsController(IBidsRepository bidsRepo)
        {
            _bidsRepo = bidsRepo;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string auctionId)
        {
            if (string.IsNullOrEmpty(auctionId))
                return BadRequest("Auction id is required"); 

            var result = await _bidsRepo.GetBidsByAuctionIdAsync(auctionId);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Bid  model)
        {
            if (model == null)
                return BadRequest("Bid object is required");

            var result = await _bidsRepo.CreateBidAsync(model);

            return Ok(result);
        }
    }
}
