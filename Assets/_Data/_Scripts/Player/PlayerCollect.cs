using Assets._Data._Scripts.Audio;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class PlayerCollect : MonoBehaviour
{
    private Inventory _inventory;
    private UI_Inventory _UIInventory;
    private Animator _animator;
    public GameObject _slotInventory;
    private Item item;
    private void Start()
    {
        _UIInventory = GameObject.Find("UIInventory").GetComponent<UI_Inventory>();
        InventoryDefault();

        _UIInventory.SetInventory(_inventory);
        Endpoint.Instance.SetInventory(_inventory);

    }
    public void InventoryDefault()
    {
        _inventory = new Inventory();
        int y = 0;
        foreach (Transform fruit in FruitsController.Instance.transform)
        {
            item = new() { itemType = fruit.name, amount = 0 };
            _inventory.AddItem(item);

            GameObject _slotInventoryClone = Instantiate(_slotInventory, new(0, 0, 0), Quaternion.identity, _UIInventory.transform);
            _slotInventoryClone.transform.localPosition = new Vector3(0, y * -70, 0);
            _slotInventoryClone.name = fruit.name;

            Sprite sprite = item.GetSprite();
            _slotInventoryClone.GetComponentInChildren<Image>().sprite = sprite;

            y++;
        }
    }
    public void CollectFruit(Collider2D collision)
    {
        ItemWorld itemWorld = collision.GetComponent<ItemWorld>();
        if (itemWorld != null)
        {
            AudioClip audioClip = AudioAssets.instance.CollectSound();
            AudioManager.Instance.PlaySFX(audioClip);

            _animator = collision.GetComponent<Animator>();
            _animator.SetTrigger("Collect");

            _inventory.AddItem(itemWorld.GetItem());
            itemWorld.GetComponent<Collider2D>().enabled = false;
            StartCoroutine(TimeDelay(itemWorld));
        }
    }
    private IEnumerator TimeDelay(ItemWorld itemWorld)
    {
        yield return new WaitForSeconds(1f);
        itemWorld.DestroySelf();
    }
}
