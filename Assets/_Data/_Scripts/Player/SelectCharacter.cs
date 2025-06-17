using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectCharacter : MonoBehaviour
{
    public CharacterSO characterSO;
    public GameObject nameCharacterPrefab;
    private GameObject[] showCharacter = new GameObject[3];
    private List<GameObject> listAvt;
    private Vector3 position = new(500, 0, 0);
    private Vector3 scale;

    private int indexCurrent;
    private void Start()
    {
        indexCurrent = PlayerPrefs.GetInt("characterSelect", 0);

        SpawnAllCharacter();
        SpawnCharacter();
    }

    private void SpawnAllCharacter()
    {
        listAvt = new List<GameObject>();
        foreach (InfoCharacter infoCharacter in characterSO.listCharacter)
        {
            GameObject avtCharacter = Instantiate(infoCharacter.avt, gameObject.transform);
            listAvt.Add(avtCharacter);
            avtCharacter.SetActive(false);
            scale = infoCharacter.avt.transform.localScale;
            GameObject name = Instantiate(nameCharacterPrefab, avtCharacter.transform);
            name.GetComponent<TextMeshProUGUI>().text = infoCharacter.name;
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

            if (indexCurrent - 1 + i >= 0 && indexCurrent - 1 + i < characterSO.listCharacter.Count)
            {
                avtCharacter = listAvt[indexCurrent - 1 + i];

                avtCharacter.SetActive(true);
                avtCharacter.GetComponent<RectTransform>().anchoredPosition = position * (i - 1);
                avtCharacter.GetComponent<RectTransform>().localScale = scale;

                showCharacter[i] = avtCharacter;
            }
            else
            {
                showCharacter[i] = null;
            }
        }
        showCharacter[1].GetComponent<RectTransform>().localScale = showCharacter[1].GetComponent<RectTransform>().localScale * 1.5f;
    }
    public void NextCharacter()
    {
        if (indexCurrent == characterSO.listCharacter.Count - 1)
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
    public void Select()
    {
        PlayerPrefs.SetInt("characterSelect", indexCurrent);
        PlayerPrefs.Save();
        Debug.Log(PlayerPrefs.GetInt("characterSelect", 0));
        transform.parent.gameObject.SetActive(false);
    }
}
