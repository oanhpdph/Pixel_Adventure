using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/List Character", fileName = "List Character")]
public class CharacterSO : ScriptableObject
{
    public List<InfoCharacter> listCharacter;
}
[System.Serializable]
public class InfoCharacter
{
    public GameObject avt;
    public GameObject character;
    public string name;
}