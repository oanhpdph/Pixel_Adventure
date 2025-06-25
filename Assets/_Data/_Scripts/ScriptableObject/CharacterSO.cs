using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/List Character", fileName = "List Character")]
public class CharacterSO : ScriptableObject
{
    public CharacterWrapper listCharacter;
}
[System.Serializable]
public class InfoCharacter
{
    public string nameAvt;
    public string namePrefab;
    public string name;
    public bool isUnlock;
    public int price;
}
[System.Serializable]
public class CharacterWrapper
{
    public InfoCharacter[] infoCharacters;
    public int index;
}