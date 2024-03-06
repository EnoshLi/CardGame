//房间的类型
public enum RoomType
{
    //普通敌人
    MinorEnemy,
    //精英敌人
    EliteEnemy,
    //商店
    Shop,
    //财宝
    Treasure,
    //休息室
    RestRoom,
    //Boss
    Boos
}
//房间的访问状态
public enum RoomState
{
    //不可访问
    Locked,
    //访问过
    visited,
    //可访问
    Attainable
}
