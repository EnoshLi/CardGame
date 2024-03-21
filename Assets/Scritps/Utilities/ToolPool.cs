using System;
using UnityEngine;
using UnityEngine.Pool;

namespace Scritps.Utilities
{
    public class ToolPool : MonoBehaviour
    {
        [SerializeField] private GameObject objPrefab;
        
        // 创建对象池
        private ObjectPool<GameObject> pool;
        /// <summary>
        /// 初始化对象池
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void Start()
        {
            pool = new ObjectPool<GameObject>(
                createFunc:()=>Instantiate(objPrefab,transform),
                actionOnGet:(obj)=>obj.SetActive(true),
                actionOnRelease:(obj)=>obj.SetActive(false),
                actionOnDestroy:(obj)=>Destroy(obj),
                collectionCheck:false,
                defaultCapacity:10,
                maxSize:100
                );
            PreFillPool(7);
        }

        private void PreFillPool(int count)
        {
            var preFillArray = new GameObject[count];
            for (int i = 0; i < count; i++)
            {
                preFillArray[i] = pool.Get();
            }

            foreach (var item in preFillArray)
            {
                pool.Release(item);
            }
        }

        public GameObject GetObjectFromPool()
        {
            return pool.Get();
        }

        public void ReturnObjectToPool(GameObject obj)
        {
            pool.Release(obj);
        }
    }
}