using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    private AssetReference currentScene;
    public AssetReference map;
    public async void OnLoadRoomEvent(object data)
    {
        if (data is RoomDataSO)
        {
            var currentData =(RoomDataSO) data;
            //Debug.Log(currentData.roomType);
            currentScene = currentData.sceneToLoad;
        }
        //卸载房间
        await UnloadSceneTesk();
        //加载房间
        await LoadSceneTesk();
    }
    /// <summary>
    /// 异步操作加载场景
    /// </summary>
   private async Awaitable LoadSceneTesk()
   {
       var s=currentScene.LoadSceneAsync(LoadSceneMode.Additive);
       await s.Task;
       if (s.Status==AsyncOperationStatus.Succeeded)
       {
           SceneManager.SetActiveScene(s.Result.Scene);
       }
   }
    /// <summary>
    /// 异步卸载场景
    /// </summary>
    private async Awaitable UnloadSceneTesk()
    {
       await SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
    }
    /// <summary>
    /// 监听加载地图的点击事件
    /// </summary>
    public async void LoadMap()
    {
        await UnloadSceneTesk();
        currentScene = map;
        await LoadSceneTesk();
    }
}
