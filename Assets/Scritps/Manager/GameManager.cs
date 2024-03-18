using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("地图布局")] public MapLayoutSO mapLayoutSO;
    /// <summary>
    /// 更新房间的事件监听函数
    /// </summary>
    /// <param name="roomVector"></param>
    public void UpdateMapLayoutData(object  value)
    {
        var roomVector = (Vector2Int)value;
        var currentRoom = mapLayoutSO.mapRoomDatasList.Find(r=>r.colum==roomVector.x && r.line==roomVector.y);
        currentRoom.roomState = RoomState.visited;
        // 更新地图数据
        var sameColumnRooms = mapLayoutSO.mapRoomDatasList.FindAll(r => r.colum == currentRoom.colum);
        foreach (var room in sameColumnRooms)
        {
            if (room.line!=roomVector.y)
            {
                room.roomState = RoomState.Locked;
            }

            foreach (var link in currentRoom.linkTo)
            {
                var linkRoom=mapLayoutSO.mapRoomDatasList.Find(r=>r.colum==link.x && r.line==link.y);
                linkRoom.roomState = RoomState.Attainable;
            }
        }
    }
}
