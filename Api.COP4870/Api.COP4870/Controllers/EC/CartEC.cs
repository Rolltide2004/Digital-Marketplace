using Api.COP4870.Controllers.EC.Database;
using COP4870.DTO;
using COP4870.Models;

namespace Api.COP4870.Controllers.EC
{
    public class CartEC
    {
        public List<Item?> Get()
        {
            return FilebaseC.Current.Cart;
        }
        public IEnumerable<Item> Get(string? query)
        {
            return FakeDatabase.Search(query, "cart").Take(100) ?? new List<Item>();
        }
        public Item? Delete(int id)
        {
            var itemToDelete = FilebaseC.Current.Cart.FirstOrDefault(i => i.Id == id);
            if (itemToDelete != null)
            {
                FilebaseC.Current.Delete("cart", itemToDelete.Id + "");
            }

            return itemToDelete;
        }
        public Item? AddOrUpdate(Item item)
        {
            return FilebaseC.Current.AddOrUpdate(item);
        }
    }
}
