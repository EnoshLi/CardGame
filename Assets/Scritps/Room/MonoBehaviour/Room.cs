using System;
using UnityEngine;

public class Room : MonoBehaviour
{
    //行
    public int line;
    //纵
    public int column;
    private SpriteRenderer spriteRenderer;
    public RoomDataSO roomData;
    public RoomState roomState;

    private void Start()
    {
        SetupRoom(0,0,roomData);
    }

    private void Awake()
    {
        spriteRenderer=GetComponentInChildren<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        Debug.Log("点击房间"+roomData.roomType);
    }
/// <summary>
/// 外部创建房间时调用房间配置
/// </summary>
/// <param name="line"></param>
/// <param name="column"></param>
/// <param name="roomData"></param>
    public void SetupRoom(int line,int column,RoomDataSO roomData)
    {
        this.line = line;
        this.column = column;
        this.roomData = roomData;
        spriteRenderer.sprite = roomData.roomIcon;
    }
}
