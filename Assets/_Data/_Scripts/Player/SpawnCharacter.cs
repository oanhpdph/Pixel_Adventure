using UnityEngine;


public class SpawnCharacter : MonoBehaviour
{
    public CharacterSO characterSO;

    private void Awake()
    {
        Spawn();
    }

    private void Spawn()
    {
        GameObject character = Instantiate(characterSO.listCharacter[PlayerPrefs.GetInt("characterSelect", 0)].character, transform);
    }
}
