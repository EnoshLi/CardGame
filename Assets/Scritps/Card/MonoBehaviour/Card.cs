using System;
using TMPro;
using UnityEngine;

public class Card : MonoBehaviour
{
    public CardDataSO cardDataSo;
    [Header("组件")] public SpriteRenderer cardImage;
    public TextMeshPro cardNameText, cost, description,cardType;

    private void Start()
    {
        Init(cardDataSo);
    }

    public void Init(CardDataSO data)
    {
        cardDataSo = data;
        cardNameText.text = data.cardName;
        cardImage.sprite = data.cardImage;
        cost.text = data.cost.ToString();
        description.text = data.description;
        cardType.text = data.cardType switch {
            CardType.Attack => "攻击",
            CardType.Defense => "防御",
            CardType.Abilities => "技能",
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}
