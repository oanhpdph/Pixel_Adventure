using System.Threading.Tasks;


public interface ILoadAsset
{
    Task<T> LoadAsset<T>(string path);

}
