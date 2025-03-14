using UnityEngine;

public class ItemWorld : MonoBehaviour
{
    private Item item;

    private void Awake()
    {
        item = new Item { itemType = transform.parent.name, amount = 1 };
    }
    public Item GetItem()
    {
        return item;
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }

}
