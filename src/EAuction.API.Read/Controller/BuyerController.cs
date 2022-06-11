using EAuction.Service.BidsService;
using EAuction.Service.BuyerService;
using EAuction.Service.MongoDb.Buyer;
using EAuction.Service.MongoDb.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace EAuction.API.Read.Controllers
{
    [Route("e-auction/api/v1/buyer/[controller]")]
    public class BuyerController : Controller
    {
        private readonly IMongoRepository<BuyerInfo> _peopleRepository;
        //private readonly string bootstrapServers = "localhost:9092";
        //private readonly string topic = "test";
        public BuyerController(IMongoRepository<BuyerInfo> peopleRepository)
        {
            _peopleRepository = peopleRepository;
        }

        /// <summary>
        /// Returns list of all Buyer
        /// </summary>
        /// <returns></returns>      
        [HttpGet]
        [Route("/getAllBuyer")]
        public IActionResult GetAllBuyer()
        {
            var buyer = _peopleRepository.AsQueryable();
    
            return Ok(buyer);
        }


       /// <summary>
       /// 
       /// </summary>
       /// <param name="buyerInfo"></param>
       /// <returns></returns>
        [HttpPost]
        [Route("/buyer")]
        public IActionResult AddBuyer([FromBody] BuyerInfo buyerInfo)
        {
            //var buyer = JsonSerializer.Deserialize<BuyerInfo>(buyerInfo);

            _peopleRepository.InsertOne(buyerInfo);

            return Ok();
        }

    }
}
