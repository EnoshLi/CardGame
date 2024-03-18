using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    private AssetReference currentScene;
    public AssetReference map;
    public Vector2Int currenetRoomVector;
    [Header("广播")] 
    public ObjectEventSO afterRoomLoadedEvent;
    public async void OnLoadRoomEvent(object data)
    {
        if (data is Room)
        {
            var currentRoom=data as Room;
            var currentData= currentRoom.roomData;
            currenetRoomVector = new(currentRoom.column,currentRoom.line);
            //Debug.Log(currentRoom.column+" "+currentRoom.line);
            currentScene = currentData.sceneToLoad;
        }
        //卸载房间 
        await UnloadSceneTesk();
        //加载房间
        await LoadSceneTesk();
        afterRoomLoadedEvent.RaseEvent(currenetRoomVector,this);
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
