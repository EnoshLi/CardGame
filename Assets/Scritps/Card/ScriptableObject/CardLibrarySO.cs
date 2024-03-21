using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "CardLibraryEntrySO ",menuName = "Card/CardLibraryEntrySO ")]
public class CardLibrarySO : ScriptableObject
{
    public List<CardLibraryEntry> cardLibraryList;
}

[System.Serializable]
public struct CardLibraryEntry
{
    public CardDataSO cardData;
    public int amount;
}
