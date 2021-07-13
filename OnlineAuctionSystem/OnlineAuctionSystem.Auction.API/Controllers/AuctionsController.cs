using Microsoft.AspNetCore.Mvc;
using OnlineAuctionSystem.Auction.API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuctionSystem.Auction.API.Controllers
{
    [Route("/api/[Controller]")]
    public class AuctionsController : ControllerBase
    {

        private readonly IAuctionsRepository _repo;

        public AuctionsController(IAuctionsRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _repo.GetAllAsync());
        }

        [HttpGet("bybidderid/{bidderId}")]
        public async Task<IActionResult> GetAllByBidder(string bidderId)
        {
            return Ok(await _repo.GetAllByBidUserIdAsync(bidderId));
        }


        [HttpGet("byuserid/{bidderId}")]
        public async Task<IActionResult> GetAllByUser(string userId)
        {
            return Ok(await _repo.GetAllByUserIdAsync(userId));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Models.Auction model)
        {
            var result = await _repo.AddAsync(model);
            return Ok(result);
        }


        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Models.Auction model)
        {
            var result = await _repo.UpdateAsync(model);
            return Ok(result);
        }

    }
}
