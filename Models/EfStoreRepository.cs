
namespace SportsStore.Models
{
    public class EfStoreRepository : IStoreRepository
    {
        private StoreDbContext _context;

        public EfStoreRepository(StoreDbContext context)
        {
            _context = context;
        }

        public IQueryable<Product> Products => _context.Products;

        public void CreateProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges(); 
        }

        public void DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
        }

        public void SaveProduct(Product product)
        {
            _context.SaveChanges();
        }
    }
}
