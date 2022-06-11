
using EAuction.Domain.Seller;
using EAuction.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAuction.Service.SellerService
{
	public class SellerService
	{

		private readonly IDataAccessProvider _dataAccessProvider;

		public SellerService(IDataAccessProvider dataAccessProvider)
		{
			_dataAccessProvider = dataAccessProvider;
		}

        public async Task<SellerInfo> AddSeller(SellerInfo value)
        {
            var sellerRecord = new Seller
            {
                Address = value.Address,
                City = value.City,
                Email = value.Email,
                FirstName = value.FirstName,
                LastName = value.LastName,
                Phone = value.Phone,
                PinCode = value.PinCode,
                State = value.State
            };



            var der = await _dataAccessProvider.AddSeller(sellerRecord);

            var result = new SellerInfo
            {
                Address = der.Address,
                City = der.City,
                Email = der.Email,
                FirstName = der.FirstName,
                LastName = der.LastName,
                Phone = der.Phone,
                PinCode = der.PinCode,
                State = der.State,
                SellerId = der.SellerId
            };

            return result;
        }

        public async Task<IEnumerable<SellerInfo>> GetAllSeller()
        {
            var data = await _dataAccessProvider.GetAllSeller();

            var results = data.Select(der => new SellerInfo
            {
                Address = der.Address,
                State = der.State,
                SellerId = der.SellerId,
                City = der.City,
                Email = der.Email,
                FirstName = der.FirstName,
                LastName = der.LastName,
                Phone = der.Phone,
                PinCode = der.PinCode,
            });

            return results;
        }

    }
}
