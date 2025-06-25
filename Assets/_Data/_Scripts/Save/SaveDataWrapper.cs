using Assets._Data._Scripts.Level;
using System.Linq;
[System.Serializable]
public class SaveDataWrapper
{
    public LevelData[] levelData;
    public int TotalStar
    {
        get
        {
            if (levelData == null)
            {
                return 0;
            }
            else return levelData.Sum(data => data.star);
        }
    }
}
