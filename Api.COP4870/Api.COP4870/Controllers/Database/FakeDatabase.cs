using COP4870.DTO;
using COP4870.Models;

namespace Api.COP4870.Controllers.EC.Database
{
    public static class FakeDatabase
    {
        private static List<Item?> inventory = new List<Item?>
        {
            new Item{ Product = new ProductDTO{Id=1, Name="Product 1 WEB", Quantity=10, Price=1.99 }, Id=1, Quantity=1},
            new Item{ Product = new ProductDTO{Id=2, Name="Product 2 WEB", Quantity=10, Price=1.99 }, Id=2, Quantity=2},
            new Item{ Product = new ProductDTO{Id=3, Name="Product 3 WEB", Quantity=10, Price=1.99 }, Id=3, Quantity=3}
        };
        public static int LastKey_Item {
            get {
                if (!inventory.Any()) {
                    return 0;
                }
                return inventory.Select(p => p?.Id ?? 0).Max();
            }
        }
        public static List<Item?> Inventory
        {
            get {
                return inventory;
            }
        }
        public static IEnumerable<Item> Search(string? query) {
            return Inventory.Where(p => p?.Product?.Name?.ToLower()
                .Contains(query?.ToLower() ?? string.Empty) ?? false);
        }
    }
}