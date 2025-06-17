using Assets._Data._Scripts.Level;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Scriptable Objects/GameData", order = 1)]
public class GameData : ScriptableObject
{
    public List<SaveData> saveData;
}
