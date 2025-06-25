namespace Assets._Data._Scripts.Level
{
    [System.Serializable]
    public class LevelData
    {
        public int level;
        public int star;
        public bool unlock;

        public LevelData()
        {
        }
        public LevelData(int level, int star, bool unlock)
        {
            this.level = level;
            this.star = star;
            this.unlock = unlock;
        }
    }

}