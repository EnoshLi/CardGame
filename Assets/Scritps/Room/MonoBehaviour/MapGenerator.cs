using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapGenerator : MonoBehaviour
{
    [Header("地图房间配置表")]
    //地图信息(包含的房间数，行列数，怪物宝箱的布置)
    public MapConfigSO mapConfig;
    
    [Header("预制体")]
    //房间的样式(Boss,Enemy,restRoom)
    public Room roomPrefab;
    public LineRenderer linePrefab;
    
    [Header("基本变量")]
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
    private List<Room> rooms =new List<Room>();
    //连接线列表
    private List<LineRenderer> lines = new();
    //房间数据
    public List<RoomDataSO> roomDataList = new();
    private Dictionary<RoomType, RoomDataSO> roomDataDictionary=new ();
    private void Awake()
    {
        //获取相机高度的尺寸
        screenHeight = Camera.main.orthographicSize * 2;
        //固定写法(相机宽度尺寸的写法)
        screenWidth = Camera.main.aspect*screenHeight;
        //获取列的间距
        columnWidth=screenWidth/(mapConfig.roomBluePrint.Count);
        foreach (var roomData in roomDataList)
        {
            roomDataDictionary.Add(roomData.roomType,roomData);
        }
    }

    private void Start()
    {
        GreatMap();
    }
    /// <summary>
    /// 创建地图上的房间
    /// </summary>
    private void GreatMap()
    {
        //创建前一列房间列表
        List<Room> previousColumnRooms = new();
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
            //生成当前房间配置表
            List<Room> currentColumnRooms = new();
            //生成所有房间
            for (int i = 0; i < amount; i++)
            {
                //判断是否为最后一列
                if (column==mapConfig.roomBluePrint.Count-1)
                {
                    newPoint.x = screenWidth / 2 - border * 2;
                }
                else if (column!=0)
                {
                    newPoint.x = generatePoint.x + Random.Range(-border/2, border/2);
                }
                //每个房间Y的坐标
                newPoint.y = startHeight - roomGapY * i;
                var room = Instantiate(roomPrefab, newPoint,quaternion.identity,transform);
                RoomType newRoomtype = GetRandomRoomType(mapConfig.roomBluePrint[column].roomType);
                room.SetupRoom(column,i,GetRoomData(newRoomtype));
                rooms.Add(room);
                currentColumnRooms.Add(room);
            }
            //判断当前列是否为第一列
            if (previousColumnRooms.Count>0)
            {
                //房间连线
                GreateConnections(previousColumnRooms,currentColumnRooms);
            }

            previousColumnRooms = currentColumnRooms;
        }
    }
    /// <summary>
    /// 房间连线
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    private void GreateConnections(List<Room> column1,List<Room> column2)
    {
        HashSet<Room> connectedColumn2Rooms = new();
        foreach (var room in column1)
        {
           var targetRoom = ConnectToRandomRoomOne(room,column2);
           connectedColumn2Rooms.Add(targetRoom);
        }

        foreach (var room in column2)
        {
            if (!connectedColumn2Rooms.Contains(room))
            {
                ConnectToRandomRoomTwo(room,column1);
            }
        }
    }

    #region 保证连线方向的一致性
    private Room ConnectToRandomRoomOne(Room room, List<Room> column2)
    {
        Room targetRoom;
        targetRoom = column2[Random.Range(0,column2.Count)];
        //创建房间间的连线
        var line = Instantiate(linePrefab, transform);
        line.SetPosition(0,targetRoom.transform.position);
        line.SetPosition(1,room.transform.position);
        lines.Add(line);
        return targetRoom;
    }
    private Room ConnectToRandomRoomTwo(Room room, List<Room> column1)
    {
        Room targetRoom;
        targetRoom = column1[Random.Range(0,column1.Count)];
        //创建房间间的连线
        var line = Instantiate(linePrefab, transform);
        line.SetPosition(0,room.transform.position);
        line.SetPosition(1,targetRoom.transform.position);
        lines.Add(line);
        return targetRoom;
    }
    #endregion
    

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

        foreach (var line in lines)
        {
            Destroy(line.gameObject);
        }
        rooms.Clear();
        lines.Clear();
        GreatMap();
    }

    private RoomDataSO GetRoomData(RoomType roomType)
    {
        return roomDataDictionary[roomType];
    }

    private RoomType GetRandomRoomType(RoomType flags)
    {
        string[] option = flags.ToString().Split(',');
        string randomOption =option[Random.Range(0, option.Length)] ;
        RoomType roomType = (RoomType)Enum.Parse(typeof(RoomType),randomOption);
        return roomType;
    }
}
