
using EAuction.Service.BuyerModels;
using EAuction.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EAuction.Service.Bids.Model
{
    public class BidsInfo
    {
		//public List<EAuction.Service.Buyer.Models.BuyerInfo> buyerInfo;

		public ProductInfo productInfo { get; set; }

        public List<BuyerInfo> buyerInfo { get; set; }
    }
}
