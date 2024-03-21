using System;
using System.Collections.Generic;
using Scritps.Utilities;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class CardManager : MonoBehaviour
{
    public ToolPool toolPool;
    public List<CardDataSO> cardDataList;//卡牌列表
    [Header("卡牌库")] 
    public CardLibrarySO newGameCardLibrary;//新游戏初始化卡牌库
    public CardLibrarySO currentLibrary;//当前玩家卡牌库
    private void Awake()
    {
        InitializeCardDataList();
        foreach (var item in newGameCardLibrary.cardLibraryList)
        {
            currentLibrary.cardLibraryList.Add(item);
        }
    }

    #region 获取项目卡牌
    private void InitializeCardDataList()
    {
        Addressables.LoadAssetsAsync<CardDataSO>("CardData",null).Completed += OnCardDataLoaded;
    }

    private void OnCardDataLoaded(AsyncOperationHandle<IList<CardDataSO>> handle)
    {
        if (handle.Status==AsyncOperationStatus.Succeeded)
        {
            cardDataList = new(handle.Result);
        }
        else
        {
            Debug.LogError("No CardData Found!");
        }
    }
    

    #endregion

    public GameObject GetCardObject()
    {
        return toolPool.GetObjectFromPool();
    }

    public void DiscardCard(GameObject cardObj)
    {
        toolPool.ReturnObjectToPool(cardObj);
    }

}
