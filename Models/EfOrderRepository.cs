
using Microsoft.EntityFrameworkCore;

namespace SportsStore.Models
{
    public class EfOrderRepository : IOrderRepository
    {
        private StoreDbContext _context;

        public EfOrderRepository(StoreDbContext dbContext)
        {
            _context = dbContext;
        }

        public IQueryable<Order> Orders => _context.Orders
                .Include(o => o.Lines)
                .ThenInclude(l => l.Product);

        public void SaveOrder(Order order)
        {
            _context.AttachRange(order.Lines.Select(l => l.Product));
            if(order.OrderID == 0)
            {
                _context.Add(order);
            }
            _context.SaveChanges();
        }
    }
}
