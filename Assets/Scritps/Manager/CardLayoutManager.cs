using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class CardLayoutManager : MonoBehaviour
{
    public bool isHorizontal;
    public float maxWidth = 7f;
    public float cardSpacing = 2f;
    public Vector3 centerPoint;
    [SerializeField]private List<Vector3> cardPosition = new();
    private List<Quaternion> cardRotation=new();

    public CardTransform getCardTransform(int index,int totalCards)
    {
        CalculatePosition(totalCards,isHorizontal);
        return new CardTransform(cardPosition[index], cardRotation[index]);
    }

    private void CalculatePosition(int numberOfCards, bool horizontal)
    {
        cardPosition.Clear();
        cardRotation.Clear();
        if (horizontal)
        {
            float currentWidth = cardSpacing * (numberOfCards - 1);
            float totalWidth = Math.Min(currentWidth, maxWidth);
            float currentSpacing = totalWidth > 0 ? totalWidth / (numberOfCards - 1) : 0;
            for (int i = 0; i < numberOfCards; i++)
            {
                float xPos=0-(totalWidth/2)+(i*currentSpacing);
                var pos= new Vector3(xPos, centerPoint.y, 0f);
                var rotation=Quaternion.identity;
                cardPosition.Add(pos);
                cardRotation.Add(rotation);
            }
        }
    }
}
