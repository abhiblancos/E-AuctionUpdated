
using EAuction.Service.Bids.Model;
using EAuction.Service.BuyerModels;
using EAuction.Service.BuyerService;
using EAuction.Service.Model;
using EAuction.Service.ProductService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAuction.Service.BidsService
{
	public class BidService
	{

		private readonly IDataAccessProvider _dataAccessProvider;
        private readonly BuyerServ _buyerService;
        private readonly ProductServ _productService;

        public BidService(IDataAccessProvider dataAccessProvider, BuyerServ buyerService, ProductServ productService)
		{
			_dataAccessProvider = dataAccessProvider;
            _buyerService = buyerService;
            _productService = productService;

        }

        public async Task UpdateBid(int productId, string buyerEmailId, double newBidAmt)
        {
            var buyerExists = _buyerService.GetAllBuyer().Result.Where(a => a.ProductId == productId && a.Email == buyerEmailId).SingleOrDefault();
            var productInfo = _productService.GetAllProducts().Result.Where(a => a.ProductId == productId).SingleOrDefault();
            if (productInfo == null)
            {
                throw new Exception("Product is not exists.");
            }
            if (buyerExists == null)
            {
                throw new Exception("This buyer is not exists.");
            }
            if (productInfo != null && productInfo.StartingPrice > newBidAmt)
            {
                throw new Exception("Bid Amount is not less then the product starting price.");
            }
            if (productInfo != null && productInfo.BidEndDate < System.DateTime.Now)
            {
                throw new Exception("Bid end date is expired.");
            }


            if (buyerExists != null)
            {
                await _dataAccessProvider.UpdateBid(productId, buyerEmailId, newBidAmt);

            }

        }

        public async Task<BidsInfo> ShowAllBids(int productId)
        {
            BidsInfo result = new BidsInfo();
            ProductInfo productInfo = new ProductInfo();
            List<BuyerInfo> buyerInfo = new List<BuyerInfo>();
            var productInfoData = await _dataAccessProvider.GetProductById(productId);
            var buyerInfoData = await _dataAccessProvider.GetAllBidsByProductId(productId);

            if (productInfo != null)
            {
                productInfo.BidEndDate = productInfo.BidEndDate;
                productInfo.Category = productInfo.Category;
                productInfo.CreatedDate = productInfo.CreatedDate;
                productInfo.DetailedDescription = productInfo.DetailedDescription;
                productInfo.IsDeleted = productInfo.IsDeleted;
                productInfo.ProductId = productInfo.ProductId;
                productInfo.ProductName = productInfo.ProductName;
                productInfo.SellerId = productInfo.SellerId;
                productInfo.ShortDescription = productInfo.ShortDescription;
                productInfo.StartingPrice = productInfo.StartingPrice;

            }

            if (buyerInfo != null && buyerInfo.Count > 0)
            {
                foreach (var item in buyerInfoData)
                {
                    BuyerInfo buyer = new BuyerInfo();
                    buyer.Address = item.Address;
                    buyer.BidAmount = item.BidAmount;
                    buyer.BuyerId = item.BuyerId;
                    buyer.City = item.City;
                    buyer.CreatedDate = item.CreatedDate;
                    buyer.Email = item.Email;
                    buyer.FirstName = item.FirstName;
                    buyer.LastName = item.LastName;
                    buyer.Phone = item.Phone;
                    buyer.PinCode = item.PinCode;
                    buyer.ProductId = item.ProductId;
                    buyer.State = item.State;
                    buyerInfo.Add(buyer);
                }
            }

            result.buyerInfo = buyerInfo;
            result.productInfo = productInfo;
            return result;
        }
    }
}
