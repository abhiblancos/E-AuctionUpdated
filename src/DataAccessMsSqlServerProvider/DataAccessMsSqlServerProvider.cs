using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EAuction.Domain.Buyer;
using EAuction.Domain.Model;
using EAuction.Domain.Product;
using EAuction.Domain.Seller;
using EAuction.Service;
using EAuction.Service.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataAccessMsSqlServerProvider
{
    public class DataAccessMsSqlServerProvider : IDataAccessProvider
    {
        private readonly DomainModelMsSqlServerContext _context;
        private readonly ILogger _logger;

        public DataAccessMsSqlServerProvider(DomainModelMsSqlServerContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("DataAccessMsSqlServerProvider");

        }

        public async Task<DataEventRecord> AddDataEventRecord(DataEventRecord dataEventRecord)
        {
            if (dataEventRecord.SourceInfo != null && dataEventRecord.SourceInfoId == 0)
            {
                _context.SourceInfos.Add(dataEventRecord.SourceInfo);
                _context.SaveChanges();
                dataEventRecord.SourceInfoId = dataEventRecord.SourceInfo.SourceInfoId;
            }
            else
            {
                var sourceInfo = _context.SourceInfos.Find(dataEventRecord.SourceInfoId);
                dataEventRecord.SourceInfo = sourceInfo;
            }

            _context.DataEventRecords.Add(dataEventRecord);
            await _context.SaveChangesAsync();
            return dataEventRecord;
        }

        public async Task UpdateDataEventRecord(long dataEventRecordId, DataEventRecord dataEventRecord)
        {
            _context.DataEventRecords.Update(dataEventRecord);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDataEventRecord(long dataEventRecordId)
        {
            var entity = _context.DataEventRecords.First(t => t.DataEventRecordId == dataEventRecordId);
            _context.DataEventRecords.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<DataEventRecord> GetDataEventRecord(long dataEventRecordId)
        {
            return await _context.DataEventRecords
                .Include(s => s.SourceInfo)
                .FirstAsync(t => t.DataEventRecordId == dataEventRecordId);
        }

        public async Task<List<DataEventRecord>> GetDataEventRecords()
        {
            // Using the shadow property EF.Property<DateTime>(dataEventRecord)
            return await _context.DataEventRecords
                .Include(s => s.SourceInfo)
                .OrderByDescending(dataEventRecord => EF.Property<DateTime>(dataEventRecord, "UpdatedTimestamp"))
                .ToListAsync();
        }

        public async Task<List<SourceInfo>> GetSourceInfos(bool withChildren)
        {
            // Using the shadow property EF.Property<DateTime>(srcInfo)
            if (withChildren)
            {
                return await _context.SourceInfos.Include(s => s.DataEventRecords).OrderByDescending(srcInfo => EF.Property<DateTime>(srcInfo, "UpdatedTimestamp")).ToListAsync();
            }

            return await _context.SourceInfos.OrderByDescending(srcInfo => EF.Property<DateTime>(srcInfo, "UpdatedTimestamp")).ToListAsync();
        }

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
                               .FirstAsync(t => t.ProductId == productId && t.IsDeleted == false);
        }

        public async Task<List<BuyerInfo>> GetAllBidsByProductId(int productId)
        {
            return await _context.BuyerInfo.Where(a => a.ProductId == productId)
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
