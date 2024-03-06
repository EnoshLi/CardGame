using System;
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

    private void Awake()
    {
        //获取相机高度的尺寸
        screenHeight = Camera.main.orthographicSize * 2;
        //固定写法(相机宽度尺寸的写法)
        screenWidth = Camera.main.aspect*screenHeight;
        columnWidth=screenWidth/(mapConfig.roomBluePrint.Count+1);
    }

    private void GreatMap()
    {
        for (int column = 0; column < mapConfig.roomBluePrint.Count; column++)
        {
            var bluePrint = mapConfig.roomBluePrint[column];
            var amount = Random.Range(bluePrint.min, bluePrint.max);
            for (int i = 0; i < amount; amount++)
            {
                var room = Instantiate(roomPrefab, transform);
            }
        }

        
    }
}
