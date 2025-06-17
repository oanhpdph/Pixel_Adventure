using System.Collections.Generic;
using UnityEngine;


public class PanelManager : MonoBehaviour
{
    public List<GameObject> listPanel;
    private void Start()
    {
        foreach (Transform item in transform)
        {
            listPanel.Add(item.gameObject);
            item.gameObject.SetActive(false);
        }
    }

}