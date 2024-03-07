using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapGenerator : MonoBehaviour
{
    //地图信息(包含的房间数，行列数，怪物宝箱的布置)
    public MapConfigSO mapConfig;
    //房间的样式(Boss,Enemy,restRoom)
    public Room roomPrefab;
    //屏幕的长
    private float screenHeight;
    //屏幕的宽
    private float screenWidth;
    //每一列的间距
    private float columnWidth;
    //生成点
    private Vector3 generatePoint;
    //边界
    public float border;
    //房间列表
    public List<Room> rooms;

    private void Awake()
    {
        //获取相机高度的尺寸
        screenHeight = Camera.main.orthographicSize * 2;
        //固定写法(相机宽度尺寸的写法)
        screenWidth = Camera.main.aspect*screenHeight;
        //获取列的间距
        columnWidth=screenWidth/(mapConfig.roomBluePrint.Count);
    }

    private void Start()
    {
        rooms = new();
        GreatMap();
    }
    /// <summary>
    /// 创建地图上的房间
    /// </summary>
    private void GreatMap()
    {
        for (int column = 0; column < mapConfig.roomBluePrint.Count; column++)
        {
            var bluePrint = mapConfig.roomBluePrint[column];
            var amount = Random.Range(bluePrint.min, bluePrint.max);
            var startHeight = screenHeight / 2 - screenHeight / (amount + 1);
            //房间在X轴的生成点
            generatePoint = new Vector3(-screenWidth / 2 + border+columnWidth*column, startHeight, 0);
            var newPoint = generatePoint;
            //房间Y轴的间距
            var roomGapY = screenHeight / (amount + 1);
            //生成所有房间
            for (int i = 0; i < amount; i++)
            {
                if (column==mapConfig.roomBluePrint.Count-1)
                {
                    newPoint.x = screenWidth / 2 - border * 2;
                }
                else if (column!=0)
                {
                    newPoint.x = generatePoint.x + Random.Range(-border/2, border/2);
                }
                newPoint.y = startHeight - roomGapY * i;
                var room = Instantiate(roomPrefab, newPoint,quaternion.identity,transform);
                rooms.Add(room);
            }
        }
    }
    /// <summary>
    /// 重新生成房间(房间每次随机，肉鸽玩法)
    /// </summary>
    [ContextMenu("ReGenerateRoom")]
    public void ReGenerateRoom()
    {
        foreach (var room in rooms)
        {
            Destroy(room.gameObject);
        }
        rooms.Clear();
        GreatMap();
    }
}
