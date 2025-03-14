using TMPro;
using UnityEngine;

public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;

        inventory.OnItemListChange += Inventory_OnItemListChanged;
        RefreshInventory();
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventory();
    }

    public void RefreshInventory()
    {
        foreach (var item in inventory.GetInventory())
        {
            GameObject slotInventory = this.transform.Find(item.itemType).gameObject;
            slotInventory.GetComponentInChildren<TextMeshProUGUI>().text = "x " + item.amount;

        }
    }

}