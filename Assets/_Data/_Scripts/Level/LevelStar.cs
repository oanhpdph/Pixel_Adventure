using UnityEngine;
using UnityEngine.UI;

namespace Assets._Data._Scripts.Level
{
    public class LevelStar : MonoBehaviour
    {
        private Transform[] _listStar;

        private void Start()
        {
            _listStar = new Transform[transform.childCount];
            AddListStar();
        }
        public void Star(int numberOfStar)
        {
            for (int j = 0; j < numberOfStar; j++)
            {
                transform.GetChild(j).GetComponent<Image>().fillAmount = 0;
            }
        }

        private void AddListStar()
        {
            int i = 0;
            foreach (Transform item in _listStar)
            {
                _listStar[i] = item;
                i++;
            }
        }


    }
}