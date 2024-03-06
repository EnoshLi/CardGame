using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "RoomDataSO",menuName = "Map/RoomDataSO")]
public class RoomDataSO : ScriptableObject
{
    //房间的图标
    public Sprite roomIcon;
    //房间的类型(普通怪，精英怪，BOSS，休息室，奖励房间等)
    public RoomType roomType;
    //点击图标所进入的场景
    public AssetReference sceneToLoad;
}
