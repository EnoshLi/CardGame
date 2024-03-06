using System;
//位标志枚举
[Flags]
//房间的类型
public enum RoomType
{
    //普通敌人
    MinorEnemy=1,
    //精英敌人
    EliteEnemy=2,
    //商店
    Shop=4,
    //财宝
    Treasure=8,
    //休息室
    RestRoom=16,
    //Boss
    Boos=32
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
