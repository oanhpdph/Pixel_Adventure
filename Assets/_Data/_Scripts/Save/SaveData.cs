namespace Assets._Data._Scripts.Level
{
    [System.Serializable]
    public class SaveData
    {
        public int level;
        public int star;
        public bool unlock;

        public SaveData()
        {
        }
        public SaveData(int level, int star, bool unlock)
        {
            this.level = level;
            this.star = star;
            this.unlock = unlock;
        }
    }

}