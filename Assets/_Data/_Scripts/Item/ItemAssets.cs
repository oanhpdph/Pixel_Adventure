using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set; }

    [SerializeField]
    private Sprite _apple;
    [SerializeField]
    private Sprite _orange;
    [SerializeField]
    private Sprite _banana;
    [SerializeField]
    private Sprite _cherry;
    [SerializeField]
    private Sprite _melon;
    [SerializeField]
    private Sprite _pineapple;
    [SerializeField]
    private Sprite _kiwi;
    [SerializeField]

    private Sprite _strawberry;


    private void Awake()
    {
        Instance = this;
    }

    public Sprite Apple()
    {
        return _apple;
    }
    public Sprite Orange()
    {
        return _orange;

    }

    public Sprite Banana()
    {
        return _banana;

    }
    public Sprite Cherry()
    {
        return _cherry;

    }

    public Sprite Melon()
    {
        return _melon;
    }
    public Sprite Pineapple()
    {
        return _pineapple;

    }
    public Sprite Kiwi()
    {
        return _kiwi;

    }
    public Sprite Strawberry()
    {
        return _strawberry;
    }



}
