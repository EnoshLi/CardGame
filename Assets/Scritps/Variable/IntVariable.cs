using UnityEngine;

[CreateAssetMenu(fileName = "IntVariable", menuName = "Variables/IntVariable")]
public class IntVariable : ScriptableObject
{
    public int maxValue;
    public int currentValue;
    public IntEventSO valueChangeEvent;
    [TextArea] 
    [SerializeField]private string description;

    public void SetValue(int value)
    {
        currentValue = value;
        valueChangeEvent.RaseEvent(value,this);
    }
}
