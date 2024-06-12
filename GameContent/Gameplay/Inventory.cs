using Engine.Data;
using Game;

namespace Gametest.GameContent.Gameplay
{
    public class Inventory
    {
        public List<ID.ItemID.InvItemData> items = new List<ID.ItemID.InvItemData>();

        public Inventory()
        {
            //lol
        }

        public void AddItem(string _id, int count, string _data)
        {
            var i = new ID.ItemID.InvItemData();
            i.id = _id;

            int iidx = FindItem(_id);

            if (iidx != -1)
            {
                i.amount += count;
                items[iidx] = i;
            }
            else
            {
                i.add_data = _data;
                i.amount = count;
                items.Add(i);
            }
            UpdateUI();
        }

        public void UpdateUI()
        {
            
        }

        public void RemoveItem(string _id, int count)
        {
            
        }


        public int FindItem(string _id)
        {
            for (int i = 0 ; i < items.Count; i++)
            {
                var item = items[i];

                if (item.id == _id)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
