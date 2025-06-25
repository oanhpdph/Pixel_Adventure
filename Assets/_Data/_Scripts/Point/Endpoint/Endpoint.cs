using Assets._Data._Scripts.Audio;
using Assets._Data._Scripts.Level;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Endpoint : MonoBehaviour
{
    private static Endpoint instance { get; set; }
    public static Endpoint Instance => instance;

    [SerializeField] private float timeDelay = 0.5f;
    [SerializeField] private LevelStar levelStar;
    [SerializeField] private GameObject slotStatitic;
    //[SerializeField] private GameObject panelGameFinish;


    private Inventory inventory;
    private int calculatorStar;

    private LevelController levelController;
    private Item[] totalFruits;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        Init();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        CountStar();
        StartCoroutine(FinishGame());
    }
    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
    }
    public void Init()
    {
        levelController = LevelController.Instance;
        totalFruits = FruitsController.Instance.GetTotalFruitsPerType();
    }
    private IEnumerator FinishGame()
    {
        PlayerController.Instance.GetComponent<Rigidbody2D>().simulated = false;

        AudioManager.Instance.PlaySFX(AudioAssets.instance.FinishSound());

        this.GetComponent<Animator>().SetBool("Pressed", true);// play animator cup
        transform.Find("Animation").GetComponent<ParticleSystem>().Play();// play effect

        yield return new WaitForSecondsRealtime(timeDelay);
        GameManager.Instance.CurrentState = GameState.GameFinish;
        levelStar.Star(calculatorStar);
        StartCoroutine(Statitics());
        if (levelController != null)
        {
            UpdateLevel();
            UnlockNextLevel();
            levelController.SaveUpdate();
        }
    }
    private void CountStar()
    {
        for (int i = 0; i < totalFruits.Length; i++)
        {
            if (totalFruits[i].amount == inventory.GetInventory()[i].amount)
            {
                calculatorStar++;
            }
        }
    }
    public IEnumerator Statitics()
    {
        yield return null;
        GameObject statiticBar = GameObject.Find("StatiticBar");
        int i = 0;
        foreach (var item in inventory.GetInventory())
        {
            Vector3 position = new(i * 230, 0, 0);
            Debug.Log(GameObject.Find("StatiticBar"));
            GameObject CloneSlot = Instantiate(slotStatitic, position, Quaternion.identity, statiticBar.transform);
            CloneSlot.transform.localPosition = position;

            Sprite spriteFruit = item.GetSprite();
            CloneSlot.GetComponentInChildren<Image>().sprite = spriteFruit;
            CloneSlot.GetComponentInChildren<TextMeshProUGUI>().text = "x " + item.amount;
            i++;
        }
    }

    private void UnlockNextLevel()
    {
        LevelData nextLevel = levelController.GetOneLevel(levelController.currentLevel + 1);
        if (nextLevel != null)
        {
            if (nextLevel.unlock)
            {
                return;
            }
            nextLevel.unlock = true;
            levelController.UpdateSaveData(nextLevel);
        }
        else
        {
            Debug.Log("max level");
        }
    }

    private void UpdateLevel()
    {
        LevelData currentLevel = levelController.GetOneLevel(levelController.currentLevel);
        if (currentLevel.star < calculatorStar)
        {
            currentLevel.star = calculatorStar;
            levelController.UpdateSaveData(currentLevel);
        }
    }
}
