using System;
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
    //每一列的间隔
    private float columnWidth;
    //生成点
    private Vector3 generatePoint;
    //边界
    public float border;

    private void Awake()
    {
        //获取相机高度的尺寸
        screenHeight = Camera.main.orthographicSize * 2;
        //固定写法(相机宽度尺寸的写法)
        screenWidth = Camera.main.aspect*screenHeight;
        columnWidth=screenWidth/(mapConfig.roomBluePrint.Count+1);
    }

    private void Start()
    {
        GreatMap();
    }

    private void GreatMap()
    {
        for (int column = 0; column < mapConfig.roomBluePrint.Count; column++)
        {
            var bluePrint = mapConfig.roomBluePrint[column];
            var amount = Random.Range(bluePrint.min, bluePrint.max);
            var startHeight = screenHeight / 2 - screenHeight / (amount + 1);
            generatePoint = new Vector3(-screenWidth / 2 + border+columnWidth*column, startHeight, 0);
            var newPoint = generatePoint;
            for (int i = 0; i < amount; i++)
            {
                var room = Instantiate(roomPrefab, newPoint,quaternion.identity,transform);
            }
        }

        
    }
}
