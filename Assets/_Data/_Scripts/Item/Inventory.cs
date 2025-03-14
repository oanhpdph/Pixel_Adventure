using System;
using System.Collections.Generic;

public class Inventory
{
    public event EventHandler OnItemListChange;
    private List<Item> _itemList;

    public Inventory()
    {
        _itemList = new List<Item>();
    }

    public void AddItem(Item item)
    {
        bool a = false;
        foreach (var inventoryItem in _itemList)
        {
            if (inventoryItem.itemType == item.itemType)
            {
                inventoryItem.amount += item.amount;
                a = true;
            }
        }
        if (a == false)
        {
            _itemList.Add(item);
        }
        a = false;
        OnItemListChange?.Invoke(this, EventArgs.Empty);
    }

    public List<Item> GetInventory()
    {
        return _itemList;
    }


}
