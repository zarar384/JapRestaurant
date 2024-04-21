using Jap.Services.OrderAPI.DbContexts;
using Jap.Services.OrderAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Jap.Services.OrderAPI.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DbContextOptions<AppDbContext> _context;

        public OrderRepository(DbContextOptions<AppDbContext> context)
        {
            _context = context;
        }

        public async Task<bool> AddOrder(OrderHeader orderHeader)
        {
            await using var _db = new AppDbContext(_context);
            _db.OrderHeaders.Add(orderHeader);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task UpdateOrderPaymentStatus(int orderHeaderId, bool paid)
        {
            await using var _db = new AppDbContext(_context);
            var orderHeaderFromDb = await _db.OrderHeaders.FirstOrDefaultAsync(u=>u.OrderHeaderId == orderHeaderId);

            if (orderHeaderFromDb != null)
            {
                orderHeaderFromDb.PaymentStatus = paid;
                await _db.SaveChangesAsync();
            }
        }
    }
}
