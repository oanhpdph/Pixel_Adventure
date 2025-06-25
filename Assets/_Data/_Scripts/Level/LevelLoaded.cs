using Assets._Data._Scripts.Level;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelLoaded : MonoBehaviour
{
    [SerializeField] private GameObject _levelPrefab;

    private List<LevelData> saveData;

    // Start is called before the first frame update
    void Start()
    {
        InstantiateLevel();
    }

    // Update is called once per frame

    private void InstantiateLevel()
    {
        saveData = LevelController.Instance.saveData;

        foreach (LevelData item in saveData)
        {
            GameObject level = Instantiate(_levelPrefab
                , transform.position
                , Quaternion.identity
                , transform);
            level.GetComponentInChildren<TextMeshProUGUI>().text = item.level.ToString();

            LevelStar levelStar = level.transform
                .Find("Stars")
                .Find("TotalStar")
                .gameObject
                .GetComponent<LevelStar>();

            if (item.unlock)
            {
                levelStar.Star(item.star);

                level.GetComponent<Button>().onClick.AddListener(() => LevelController.Instance.SelectLevel(item.level));
            }
            else
            {
                level.GetComponent<Button>().interactable = false;
            }


        }
    }
}
