using Game;

namespace Gametest.GameContent.Gameplay
{
    public class Inventory
    {
        public List<ID.ItemID.ItemData> items = new List<ID.ItemID.ItemData>();

        public Inventory()
        {
            //lol
        }

        public void AddItem(string _id)
        {
            items.Add(ID.ItemID.GetItem(_id));
        }

        public void RemoveItem(string _id)
        {
            items.Remove(ID.ItemID.GetItem(_id));
        }
    }
}
