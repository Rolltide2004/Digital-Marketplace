using Api.COP4870.Controllers.EC.Database;
using COP4870.DTO;
using COP4870.Models;

namespace Api.COP4870.Controllers.EC
{
    public class InventoryEC
    {

        public List<Item?> Get()
        {
            return FakeDatabase.Inventory;
        }
        public Item? Delete(int id) {
            var itemToDelete = FakeDatabase.Inventory.FirstOrDefault(i => i.Id == id);
            if (itemToDelete != null) {
                FakeDatabase.Inventory.Remove(itemToDelete);
            }

            return itemToDelete;
        }
    }
}
