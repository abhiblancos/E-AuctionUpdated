using System.Collections.Generic;
using System.Threading.Tasks;
using EAuction.Domain.Model;
using EAuction.Domain.Seller;
using EAuction.Domain.Product;
using EAuction.Domain.Buyer;

namespace EAuction.Service
{
    public interface IDataAccessProvider
    {
       
        Task<Seller> AddSeller(Seller sellerRecord);

        Task<List<Seller>> GetAllSeller();

        Task<Product> AddProduct(Product productRecord);

        Task<List<Product>> GetAllProducts();

        Task<BuyerInfo> AddBuyer(BuyerInfo buyerRecord);

        Task<List<BuyerInfo>> GetAllBuyer();

        Task UpdateBid(int productId, string buyerEmailId, double newBidAmt);

        Task<Product> GetProductById(int productId);

        Task<List<BuyerInfo>> GetAllBidsByProductId(int productId);

        Task<bool> ExistsProducts(long id);
        Task DeleteProduct(long productId);
    }
}
