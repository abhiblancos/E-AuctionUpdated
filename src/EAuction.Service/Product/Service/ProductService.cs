
using EAuction.Domain.Product;
using EAuction.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAuction.Service.ProductService
{
	public class ProductServ
	{

		private readonly IDataAccessProvider _dataAccessProvider;

		public ProductServ(IDataAccessProvider dataAccessProvider)
		{
			_dataAccessProvider = dataAccessProvider;
		}

        public async Task<ProductInfo> AddProduct(ProductInfo value)
        {
            var productRecord = new Product
            {
                BidEndDate = value.BidEndDate,
                Category = value.Category,
                DetailedDescription = value.DetailedDescription,
                CreatedDate = System.DateTime.UtcNow,
                IsDeleted = false,
                ProductName = value.ProductName,
                SellerId = value.SellerId,
                ShortDescription = value.ShortDescription,
                StartingPrice = value.StartingPrice,
            };



            var der = await _dataAccessProvider.AddProduct(productRecord);

            var result = new ProductInfo
            {
                BidEndDate = der.BidEndDate,
                Category = der.Category,
                DetailedDescription = der.DetailedDescription,
                CreatedDate = der.CreatedDate,
                IsDeleted = der.IsDeleted,
                ProductName = der.ProductName,
                SellerId = der.SellerId,
                ShortDescription = der.ShortDescription,
                StartingPrice = der.StartingPrice,
                ProductId = der.ProductId,
            };

            return result;
        }


        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            var data = await _dataAccessProvider.GetAllProducts();

            var results = data.Select(der => new Product
            {
                BidEndDate = der.BidEndDate,
                StartingPrice = der.StartingPrice,
                ShortDescription = der.ShortDescription,
                SellerId = der.SellerId,
                ProductName = der.ProductName,
                ProductId = der.ProductId,
                IsDeleted = der.IsDeleted,
                DetailedDescription = der.DetailedDescription,
                CreatedDate = der.CreatedDate,
                Category = der.Category,

            });

            return results;
        }


        public async Task<bool> ExistsProducts(long id)
		{
			return await _dataAccessProvider.ExistsProducts(id);
		}

		public async Task DeleteProduct(long productId)
		{
			await _dataAccessProvider.DeleteProduct(productId);
		}
	}
}
