
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using EAuction.Domain.Model;
using EAuction.Domain.Seller;
using EAuction.Domain.Product;
using EAuction.Domain.Buyer;
using System;
using System.Threading.Tasks;
using EAuction.Service;

namespace EAuction.DataAccessSqlite.Provider
{
    public class DataAccessSqliteProvider : IDataAccessProvider
    {
        private readonly DomainModelSqliteContext _context;
        private readonly ILogger _logger;

        public DataAccessSqliteProvider(DomainModelSqliteContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("DataAccessSqliteProvider");
        }

      

        //E-Auction Methods Start here

        public async Task<Seller> AddSeller(Seller sellerRecord)
        {

            _context.SellerInfo.Add(sellerRecord);
            await _context.SaveChangesAsync();
            return sellerRecord;
        }

        public async Task<List<Seller>> GetAllSeller()
        {

            return await _context.SellerInfo
                              .ToListAsync();
        }

        public async Task<Product> AddProduct(Product productRecord)
        {

            _context.ProductInfo.Add(productRecord);
            await _context.SaveChangesAsync();
            return productRecord;
        }

        public async Task<List<Product>> GetAllProducts()
        {

            return await _context.ProductInfo.Where(a => a.IsDeleted == false)
                              .ToListAsync();
        }

        public async Task<BuyerInfo> AddBuyer(BuyerInfo buyerRecord)
        {

            _context.BuyerInfo.Add(buyerRecord);
            await _context.SaveChangesAsync();
            return buyerRecord;
        }

        public async Task<List<BuyerInfo>> GetAllBuyer()
        {

            return await _context.BuyerInfo
                              .ToListAsync();
        }

        public async Task UpdateBid(int productId, string buyerEmailId, double newBidAmt)
        {
            var updateRecored = _context.BuyerInfo.Where(a => a.ProductId == productId && a.Email == buyerEmailId).SingleOrDefault();
            updateRecored.BidAmount = newBidAmt;
            _context.BuyerInfo.Update(updateRecored);
            await _context.SaveChangesAsync();
        }

        public async Task<Product> GetProductById(int productId)
        {
            return await _context.ProductInfo
                               .FirstAsync(t => t.ProductId == productId && t.IsDeleted == false) ;
        }

        public async Task<List<BuyerInfo>> GetAllBidsByProductId(int productId)
        {
            return await _context.BuyerInfo.Where(a=>a.ProductId==productId)
                              .ToListAsync();
        }

        public async Task<bool> ExistsProducts(long id)
        {
            var filteredDataEventRecords = _context.ProductInfo
                .Where(item => item.ProductId == id);

            return await filteredDataEventRecords.AnyAsync();
        }

        public async Task DeleteProduct(long productId)
        {
            var entity = _context.ProductInfo.First(t => t.ProductId == productId);
            _context.ProductInfo.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
