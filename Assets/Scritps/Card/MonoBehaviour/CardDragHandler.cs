using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardDragHandler : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    public bool canMove;
    [HideInInspector]public Card currentCard;
    public bool canExecute;
    public GameObject arrowPrefab;
    [HideInInspector]public GameObject currentArrow;
    private CharacterBase targetCharacter;
    //private Player player;

    private void Awake()
    {
        currentCard = GetComponent<Card>();
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        switch (currentCard.cardDataSo.cardType)
        {
            case CardType.Attack:
                currentArrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
                break;
            case CardType.Defense:
            case CardType.Abilities:
                canMove = true;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (canMove)
        {
            currentCard.isAnimating = true;
            Vector3 screenPos = new(Input.mousePosition.x,Input.mousePosition.y,10);
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
            currentCard.transform.position = worldPos;
            //执行条件
            canExecute = worldPos.y > 0.1f;
        }
        else
        {
            if (eventData.pointerEnter==null)return;
            if (eventData.pointerEnter.CompareTag("Enemy"))
            {
                canExecute = true;
                targetCharacter=eventData.pointerEnter.GetComponent<CharacterBase>();
                return;
            }
            canExecute = false;
            targetCharacter = null;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (currentArrow!=null)
        {
            Destroy(currentArrow);
        }
        if (canExecute)
        {
            currentCard.ExecuteCardEffects(currentCard.player,targetCharacter);
        }
        //throw new System.NotImplementedException();
        currentCard.RestCardTransform();
        currentCard.isAnimating = false;
    }
}
