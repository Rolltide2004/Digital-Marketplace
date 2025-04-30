using Api.COP4870.Controllers.EC.Database;
using COP4870.DTO;
using COP4870.Models;

namespace Api.COP4870.Controllers.EC
{
    public class InventoryEC
    {

        public List<Item?> Get()
        {
            return Filebase.Current.Inventory;
        }
        public IEnumerable<Item> Get(string? query) {
            return FakeDatabase.Search(query, "inventory").Take(100) ?? new List<Item>();
        }
        public Item? Delete(int id) {
            var itemToDelete = Filebase.Current.Inventory.FirstOrDefault(i => i.Id == id);
            if (itemToDelete != null) {
                Filebase.Current.Delete("inventory", itemToDelete.Id+"");
            }

            return itemToDelete;
        }
        public Item? AddOrUpdate(Item item) {
            return Filebase.Current.AddOrUpdate(item);
        }
    }
}
