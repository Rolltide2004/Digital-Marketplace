using COP4870.Models;

namespace COP4870.Services
{
    public class CartServiceProxy
    {
        private CartServiceProxy()
        {
            Products = new List<Product?>();
        }

        private int LastKey
        {
            get
            {
                if (!Products.Any())
                {
                    return 0;
                }

                return Products.Select(p => p?.Id ?? 0).Max();
            }
        }

        private static CartServiceProxy? instance;
        private static object instanceLock = new object();

        public static CartServiceProxy Current
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new CartServiceProxy();
                    }
                }
                return instance;
            }
        }

        public List<Product?> Products { get; private set; }

        public Product AddOrUpdate(Product product)
        {
            if (product.Id == 0)
            {
                product.Id = LastKey + 1;
                Products.Add(product);
            }
            return product;
        }

        public Product? Delete(int id)
        {
            if (id == 0)
            {
                return null;
            }
            Product? product = Products.FirstOrDefault(p => p.Id == id);
            Products.Remove(product);
            return product;
        }
    }
}
