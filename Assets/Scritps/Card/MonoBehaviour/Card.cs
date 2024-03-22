using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class Card : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public CardDataSO cardDataSo;
    [Header("组件")] 
    public SpriteRenderer cardImage;
    public TextMeshPro cardNameText, cost, description,cardType;
    [Header("原始数据")]
    public Vector3 originalPosition;
    public Quaternion origionalRotation;
    public int originalLayerOrder;
    public bool isAnimating;

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

    public void UpdatePositionRotation(Vector3 position, Quaternion quaternion)
    {
        originalPosition = position;
        originalLayerOrder = GetComponent<SortingGroup>().sortingOrder;
        origionalRotation = quaternion;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isAnimating)
        {
            return;
        }

        transform.position = originalPosition + Vector3.up;
        transform.rotation = Quaternion.identity;
        GetComponent<SortingGroup>().sortingOrder = 100;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //throw new NotImplementedException();
        if (isAnimating)
        {
            return;
        }
        RestCardTransform();
    }

    public void RestCardTransform()
    {
        transform.SetPositionAndRotation(originalPosition,origionalRotation);
        GetComponent<SortingGroup>().sortingOrder = originalLayerOrder;
    }
}
