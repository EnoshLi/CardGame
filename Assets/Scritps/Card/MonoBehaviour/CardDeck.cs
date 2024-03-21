using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardDeck : MonoBehaviour
{
    public CardManager cardManager;
    public CardLayoutManager cardLayoutManager;
    private List<CardDataSO> drawDeck = new();//抽牌堆
    private List<CardDataSO> discardDeck = new();//弃牌堆
    private List<Card> handCardObjectList = new();//每回合当前手牌

    private void Start()
    {
        InitializeDeck();
        DrawCard(3);
    }

    [ContextMenu("测试抽卡")]
    public void TestDrawCard()
    {
        DrawCard(1);
    }
    /// <summary>
    /// 初始化抽牌堆
    /// </summary>
    public void InitializeDeck()
    {
        drawDeck.Clear();
        foreach (var entry in cardManager.currentLibrary.cardLibraryList)
        {
            for (int i = 0; i < entry.amount; i++)
            {
                drawDeck.Add(entry.cardData);
            }
        }
        //TODO洗牌/更新抽牌堆OR弃牌堆的显示数字
    }
    //抽牌
    private void DrawCard(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            if (drawDeck.Count==0)
            {
                //TODO洗牌/更新抽牌堆和弃牌堆
            }

            CardDataSO currentCardData = drawDeck[0];
            drawDeck.RemoveAt(0);
            var card = cardManager.GetCardObject().GetComponent<Card>();
            //初始化
            card.Init(currentCardData);
            handCardObjectList.Add(card);
            SetCardLayout();
        }
    }
    //手牌布局
    private void SetCardLayout()
    {
        for (int i = 0; i < handCardObjectList.Count; i++)
        {
            Card currentCard = handCardObjectList[i];
            CardTransform cardTransform = cardLayoutManager.getCardTransform(i, handCardObjectList.Count);
            currentCard.transform.SetPositionAndRotation(cardTransform.pos,cardTransform.rotation);
        }
    }
}
