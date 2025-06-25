using UnityEngine;


public class SpawnCharacter : MonoBehaviour
{
    private void Awake()
    {
        Spawn();
    }

    private void Spawn()
    {
        Instantiate(GameManager.Instance.character, transform);
    }
}
