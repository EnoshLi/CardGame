using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scritps.Room.MonoBehaviour
{
    public class MapGenerator : UnityEngine.MonoBehaviour
    {
        [Header("地图房间配置表")]
        //地图信息(包含的房间数，行列数，怪物宝箱的布置)
        public MapConfigSO mapConfig;

        [Header("地图布局")]
        public MapLayoutSO mapLayout;
    
        [Header("预制体")]
        //房间的样式(Boss,Enemy,restRoom)
        public global::Room roomPrefab;
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
        private List<global::Room> rooms =new List<global::Room>();
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

        /*private void Start()
    {
        GreatMap();
    }*/
        private void OnEnable()
        {
            if (mapLayout.mapRoomDatasList.Count>0)
            {
                LoadMap();
            }
            else
            {
                GreatMap();
                
            }
        }

        /// <summary>
        /// 创建地图上的房间
        /// </summary>
        private void GreatMap()
        {
            //创建前一列房间列表
            List<global::Room> previousColumnRooms = new();
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
                List<global::Room> currentColumnRooms = new();
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
                    if (column==0)
                    {
                        room.roomState = RoomState.Attainable;
                    }
                    else
                    {
                        room.roomState = RoomState.Locked;
                    }

                    room.SetupRoom(column,i,GetRoomData(newRoomtype));
                    rooms.Add(room);
                    // Debug.Log(room.column+","+room.line);
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
            SaveMap();
        }
        /// <summary>
        /// 房间连线
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void GreateConnections(List<global::Room> column1,List<global::Room> column2)
        {
            HashSet<global::Room> connectedColumn2Rooms = new();
            foreach (var room in column1)
            {
                var targetRoom = ConnectToRandomRoom(room,column2,false);
                connectedColumn2Rooms.Add(targetRoom);
            }

            foreach (var room in column2)
            {
                if (!connectedColumn2Rooms.Contains(room))
                {
                    ConnectToRandomRoom(room,column1,true);
                }
            }
        }

        #region 保证连线方向的一致性
        private global::Room ConnectToRandomRoom(global::Room room, List<global::Room> column2,bool check)
        {
            global::Room targetRoom;
            targetRoom = column2[Random.Range(0,column2.Count)];
            //创建房间间的连线
            var line = Instantiate(linePrefab, transform);
            if (check)
            {
                targetRoom.linkTo.Add(new (room.column,room.line));
                line.SetPosition(0,room.transform.position);
                line.SetPosition(1,targetRoom.transform.position);
            }
            else
            {
                room.linkTo.Add(new (targetRoom.column,targetRoom.line));
                line.SetPosition(0,targetRoom.transform.position);
                line.SetPosition(1,room.transform.position);
            }
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
        /// <summary>
        /// 获取房间数据
        /// </summary>
        /// <param name="roomType"></param>
        /// <returns></returns>
        private RoomDataSO GetRoomData(RoomType roomType)
        {
            return roomDataDictionary[roomType];
        }
        /// <summary>
        /// 获取房间类型
        /// </summary>
        /// <param name="flags"></param>
        /// <returns></returns>
        private RoomType GetRandomRoomType(RoomType flags)
        {
            string[] option = flags.ToString().Split(',');
            string randomOption =option[Random.Range(0, option.Length)] ;
            RoomType roomType = (RoomType)Enum.Parse(typeof(RoomType),randomOption);
            return roomType;
        }
        /// <summary>
        /// 存储地图
        /// </summary>
        private void SaveMap()
        {
            mapLayout.mapRoomDatasList = new();
            mapLayout.linePositionsList = new();
            //保存生成地图的信息
            for (int i = 0; i < rooms.Count; i++)
            {
                var room = new MapRoomData()
                {
                    posX = rooms[i].transform.position.x,
                    posY = rooms[i].transform.position.y,
                    colum = rooms[i].column,
                    line = rooms[i].line,
                    roomData = rooms[i].roomData,
                    roomState = rooms[i].roomState,
                    linkTo =rooms[i].linkTo
                };
                mapLayout.mapRoomDatasList.Add(room);
            }
            //保存连线信息
            for (int i = 0; i < lines.Count; i++)
            {
                var line = new LinePosition()
                {
                    startPos = new SerializeVector3(lines[i].GetPosition(0)),
                    endPos = new SerializeVector3(lines[i].GetPosition(1)),
                };
                mapLayout.linePositionsList.Add(line);
            }
        }
        /// <summary>
        /// 加载地图
        /// </summary>
        private void LoadMap()
        {
        
            //加载地图信息
            for (int i = 0; i < mapLayout.mapRoomDatasList.Count; i++)
            {
                var newPos = new Vector3(mapLayout.mapRoomDatasList[i].posX, mapLayout.mapRoomDatasList[i].posY, 0);
                var newRoom = Instantiate(roomPrefab, newPos, Quaternion.identity,transform);
                newRoom.roomState=mapLayout.mapRoomDatasList[i].roomState;
                newRoom.SetupRoom(mapLayout.mapRoomDatasList[i].colum,mapLayout.mapRoomDatasList[i].line,mapLayout.mapRoomDatasList[i].roomData);
                newRoom.linkTo=mapLayout.mapRoomDatasList[i].linkTo;
                rooms.Add(newRoom);
            }
            //加载连线信息
            for (int i = 0; i < mapLayout.linePositionsList.Count; i++)
            {
                var line = Instantiate(linePrefab, transform);
                line.SetPosition(0,mapLayout.linePositionsList[i].startPos.ToVector3());
                line.SetPosition(1,mapLayout.linePositionsList[i].endPos.ToVector3());
                lines.Add(line);
            }
        }
    }
}
