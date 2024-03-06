using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapConfigSO",menuName = "Map/MapConfigSO")]
public class MapConfigSO : ScriptableObject
{
    public List<RoomBluePrint> roomBluePrint;
}

[System.Serializable]
public class RoomBluePrint
{
    //每个纵列有最多房间数量或者最小房间数量
    public int min, max;
    public RoomType roomType;
}
