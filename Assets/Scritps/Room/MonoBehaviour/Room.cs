using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Room : MonoBehaviour
{   
    //列
    public int column;
    //行
    public int line;
    

    public List<Vector2Int> linkTo=new();
    //子物体图片
    private SpriteRenderer spriteRenderer;
    //房间数据(ScriptableObject)
    public RoomDataSO roomData;
    //房间状态枚举
    public RoomState roomState;
    [Header("广播")]
    public ObjectEventSO loadRoomEvent;
    

    private void Awake()
    {
        spriteRenderer=GetComponentInChildren<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
         Debug.Log("点击房间"+roomData.roomType);
        if (roomState == RoomState.Attainable)
        {
            loadRoomEvent.RaseEvent(this,this);
        }
        
    }
/// <summary>
/// 外部创建房间时调用房间配置
/// </summary>
/// <param name="line"></param>
/// <param name="column"></param>
/// <param name="roomData"></param>
    public void SetupRoom(int column,int line,RoomDataSO roomData)
    {
        this.line = line;
        this.column = column;
        this.roomData = roomData;
        spriteRenderer.sprite = roomData.roomIcon;
        spriteRenderer.color = roomState switch
        {
            RoomState.Attainable => Color.white,
            RoomState.visited => new Color(0.5f, 0.5f, 0.5f, 1f),
            RoomState.Locked => new Color(0.5f, 0.5f, 0.5f, 1f),
            _ => throw new ArgumentOutOfRangeException()
        };
        
    }
}
