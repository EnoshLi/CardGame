using System;
using System.Collections.Generic;
using DG.Tweening;

using UnityEngine;
using UnityEngine.Rendering;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;


public class CardDeck : MonoBehaviour
{
    public CardManager cardManager;
    public CardLayoutManager cardLayoutManager;
    private List<CardDataSO> drawDeck = new();//抽牌堆
    private List<CardDataSO> discardDeck = new();//弃牌堆
    private List<Card> handCardObjectList = new();//每回合当前手牌
    public Vector3 DeckPosition;//抽排堆的位置

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
        ShuffleDeck();
    }
    //抽牌
    public void DrawCard(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            CardDataSO currentCardData = drawDeck[0];
            drawDeck.RemoveAt(0);
            if (drawDeck.Count == 0)
            {
                foreach (var item in discardDeck)
                {
                    drawDeck.Add(item);
                }
                ShuffleDeck();
            }

            //更新UI数字
            //drawCountEvent.RaisEvent(drawDeck.Count, this);

            var card = cardManager.GetCardObject().GetComponent<Card>();
            //初始化
            card.Init(currentCardData);
            card.transform.position = DeckPosition;

            handCardObjectList.Add(card);
            var delay = i * 0.2f;
            SetCardLayout(delay);
        }
    }
   
    //手牌布局
    private void SetCardLayout(float delay)
    {
        for (int i = 0; i < handCardObjectList.Count; i++)
        {
            Card currentCard = handCardObjectList[i];
            CardTransform cardTransform = cardLayoutManager.getCardTransform(i, handCardObjectList.Count);
            //currentCard.transform.SetPositionAndRotation(cardTransform.pos,cardTransform.rotation);
            //抽卡动画,从抽牌堆中移动到手牌堆
            currentCard.isAnimating = true;
            currentCard.transform.DOScale(Vector3.one, 0.2f).SetDelay(delay).onComplete= () =>
            {
                //卡牌的移动
                currentCard.transform.DOMove(cardTransform.pos, 0.5f).onComplete=()=>currentCard.isAnimating=false;
                //卡牌的旋转
                currentCard.transform.DORotateQuaternion(cardTransform.rotation, 0.5f);
            };
            //手牌的排序
            currentCard.GetComponent<SortingGroup>().sortingOrder = i;
            //更新卡牌位置和旋转
            currentCard.UpdatePositionRotation(cardTransform.pos,cardTransform.rotation);
        }
    }
    /// <summary>
    /// 洗牌
    /// </summary>
    private void ShuffleDeck()
    {
        discardDeck.Clear();
        //TODO:更新UI显示数量
        for (int i = 0; i < drawDeck.Count; i++)
        {
            CardDataSO temp = drawDeck[i];
            int randomIndex = Random.Range(i, drawDeck.Count);
            drawDeck[i] = drawDeck[randomIndex];
            drawDeck[randomIndex] = temp;
        }
    }
    /// <summary>
    /// 弃牌堆逻辑，事件函数,手牌打出回收
    /// </summary>
    /// <param name="card"></param>
    public void DiscardCardFanc(object obj)
    {
        Card card = obj as Card ;
        discardDeck.Add(card.cardDataSo);
        handCardObjectList.Remove(card);
        cardManager.DiscardCard(card.gameObject);
        SetCardLayout(0f);
    }
}
