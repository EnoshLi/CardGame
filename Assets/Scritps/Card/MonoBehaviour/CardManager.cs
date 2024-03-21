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

    private void Awake()
    {
        InitializeCardDataList();
    }

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
}
