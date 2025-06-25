using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


public class LoadAssets : ILoadAsset
{
    public async Task<T> LoadAsset<T>(string fileName)
    {
        AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(fileName);
        await handle.Task;
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            return handle.Result;
        }
        Debug.Log($"Load asset fail {fileName}");
        return default;
    }
}