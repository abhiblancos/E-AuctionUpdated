using EAuction.Domain.Buyer;
using EAuction.Service.BidsService;
using EAuction.Service.BuyerModels;
using EAuction.Service.BuyerService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace EAuction.API.Write.Controllers
{
    [Route("e-auction/api/v1/bid/[controller]")]
    public class BidController : Controller
    {
        
        private readonly BidService _bidService;

        public BidController(BuyerServ buyerService, BidService bidService)
        {
            
            _bidService = bidService;   
        }

        [HttpGet]
        [Route("/show-bids/{productId}")]
        public async Task<IActionResult> ShowAllBids(int productId)
        {
            if (productId == 0)
            {
                return BadRequest();
            }
            return Ok(await _bidService.ShowAllBids(productId));
        }

        /// <summary>
        /// Update Existing Bid
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="buyerEmailId"></param>
        /// <param name="newBidAmt"></param>
        /// <returns></returns>
        [HttpPut("/update-bid/{productId}/{buyerEmailId}/{newBidAmt}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UpdateBid(int productId, string buyerEmailId, double newBidAmt)
        {
            try
            {
                if (productId == 0)
                {
                    return BadRequest();
                }
                await _bidService.UpdateBid(productId, buyerEmailId, newBidAmt);
                return Ok();
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }           
        }
        
    }
}
