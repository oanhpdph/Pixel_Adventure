using UnityEngine;

public class Item
{
    public enum ItemType
    {
        Apple,
        Orange,
        Melon,
        Kiwi,
        Banana,
        Cherry,
        Strawberry,
        Pineapple
    }

    public string itemType;
    public int amount;


    public Sprite GetSprite()
    {

        return itemType switch
        {
            "Orange" => ItemAssets.Instance.Orange(),
            "Apple" => ItemAssets.Instance.Apple(),
            "Melon" => ItemAssets.Instance.Melon(),
            "Kiwi" => ItemAssets.Instance.Kiwi(),
            "Banana" => ItemAssets.Instance.Banana(),
            "Cherry" => ItemAssets.Instance.Cherry(),
            "Strawberry" => ItemAssets.Instance.Strawberry(),
            "Pineapple" => ItemAssets.Instance.Pineapple(),
            _ => null

        };
    }

}
