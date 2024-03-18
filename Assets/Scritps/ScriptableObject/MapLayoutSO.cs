using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "MapLayoutSO",menuName = "Map/MapLayoutSO")]
//进入房间后出来的地图会随机，存储上一次地图信息
public class MapLayoutSO : ScriptableObject
{
    //存储地图信息
    public List<MapRoomData> mapRoomDatasList = new();
    //存储地图连线的信息
    public List<LinePosition> linePositionsList = new();
}
[System.Serializable]
public class MapRoomData
{
    public float posX, posY;
    public int colum, line;
    public RoomDataSO roomData;
    public RoomState roomState;
    public List<Vector2Int> linkTo;
}
[System.Serializable]
public class LinePosition
{
    public SerializeVector3 startPos, endPos;
}
