using UnityEngine;


public class FruitsController : MonoBehaviour
{
    private static FruitsController instance { get; set; }
    public static FruitsController Instance => instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public Item[] GetTotalFruitsPerType()
    {
        Item[] listFruit = new Item[transform.childCount];
        int i = 0;
        foreach (Transform item in transform)
        {
            Item fruit = new();
            fruit.amount = item.transform.childCount;
            fruit.itemType = item.name;
            listFruit[i] = fruit;
            i++;
        }
        return listFruit;
    }
}