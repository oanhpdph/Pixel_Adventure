public interface ISaveGame
{
    void Save<T>(object data, string fileName);
    T Load<T>(string fileName);
}
