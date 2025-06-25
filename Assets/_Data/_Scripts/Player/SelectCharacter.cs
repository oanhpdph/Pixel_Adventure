using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class SelectCharacter : MonoBehaviour
{
    [SerializeField] private GameObject buttonUnlock;
    [SerializeField] private GameObject buttonOk;

    public CharacterSO characterSO;
    private GameObject[] showCharacter = new GameObject[3];
    private List<GameObject> listAvt;
    private Vector3 position = new(500, 0, 0);
    private Vector3 scale;

    private int indexCurrent;

    private ISaveGame saveGame;
    private ILoadAsset loadAsset;

    private CharacterWrapper characterWrapper;

    async void Start()
    {
        this.gameObject.transform.parent.gameObject.SetActive(false);
        saveGame = new SaveController();
        loadAsset = new LoadAssets();
        characterWrapper = saveGame.Load<CharacterWrapper>("CharacterData.json") ?? characterSO.listCharacter;
        indexCurrent = characterWrapper.index;

        await SpawnAllCharacter();
        if (listAvt.Count > 0)
        {
            SpawnCharacter();
        }

        GameManager.Instance.character = await loadAsset.LoadAsset<GameObject>(characterWrapper.infoCharacters[indexCurrent].namePrefab);

    }

    private async Task SpawnAllCharacter()
    {
        listAvt = new List<GameObject>();

        List<Task<GameObject>> loadTask = new();
        foreach (var info in characterWrapper.infoCharacters)
        {
            loadTask.Add(loadAsset.LoadAsset<GameObject>(info.nameAvt));
        }
        GameObject[] loadAvts = await Task.WhenAll(loadTask);


        foreach (GameObject avt in loadAvts)
        {
            GameObject avtCharacter = Instantiate(avt, gameObject.transform);
            listAvt.Add(avtCharacter);
            avtCharacter.SetActive(false);
            scale = avt.transform.localScale;
        }

    }

    private void SpawnCharacter()
    {

        foreach (GameObject character in listAvt)
        {
            character.SetActive(false);
        }
        for (int i = 0; i < showCharacter.Length; i++)
        {
            GameObject avtCharacter;

            if (indexCurrent + i >= 1 && indexCurrent + i < characterWrapper.infoCharacters.Length + 1)
            {
                //Debug.Log(listAvt.Count);
                avtCharacter = listAvt[indexCurrent - 1 + i];

                avtCharacter.SetActive(true);
                avtCharacter.GetComponent<RectTransform>().anchoredPosition = position * (i - 1);
                avtCharacter.GetComponent<RectTransform>().localScale = scale;
                if (characterWrapper.infoCharacters[indexCurrent - 1 + i].price <= LevelController.Instance.totalStar)// check character can unlock
                {
                    characterWrapper.infoCharacters[indexCurrent - 1 + i].isUnlock = true;
                    saveGame.Save<CharacterWrapper>(characterWrapper, "CharacterData.json");
                }
                if (!characterWrapper.infoCharacters[indexCurrent - 1 + i].isUnlock)
                {
                    avtCharacter.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1f);
                }
                else
                {
                    avtCharacter.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
                }
                showCharacter[i] = avtCharacter;
            }
            else
            {
                showCharacter[i] = null;
            }
        }
        ShowButton();
        showCharacter[1].GetComponent<RectTransform>().localScale = showCharacter[1].GetComponent<RectTransform>().localScale * 1.5f;
    }
    public void ShowButton()
    {
        if (!characterWrapper.infoCharacters[indexCurrent].isUnlock)
        {
            buttonUnlock.SetActive(true);
            buttonUnlock.transform.Find("Price").GetComponent<TextMeshProUGUI>().text = characterWrapper.infoCharacters[indexCurrent].price.ToString();
            buttonOk.SetActive(false);
        }
        else
        {
            buttonUnlock.SetActive(false);
            buttonOk.SetActive(true);
        }
    }
    public void NextCharacter()
    {
        if (indexCurrent == characterWrapper.infoCharacters.Length - 1)
        {
            return;
        }
        indexCurrent += 1;
        SpawnCharacter();
    }
    public void PreviousCharacter()
    {
        if (indexCurrent == 0)
        {
            return;
        }
        indexCurrent -= 1;
        SpawnCharacter();
    }
    public async void Select()
    {
        characterWrapper.index = indexCurrent;
        saveGame.Save<CharacterWrapper>(characterWrapper, "CharacterData.json");
        transform.parent.gameObject.SetActive(false);
        GameManager.Instance.character = await loadAsset.LoadAsset<GameObject>(characterWrapper.infoCharacters[indexCurrent].namePrefab);
    }

    public void UnlockCharacter() { }
}
