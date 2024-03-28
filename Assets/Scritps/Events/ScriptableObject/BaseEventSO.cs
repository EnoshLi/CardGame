using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu(fileName = "BaseEventSO", menuName = "Events/BaseEventSO")]
public class BaseEventSO<T> : ScriptableObject
{
    [TextArea]
    public string description;

    public UnityAction<T> onEventRised;
    
    //最后一个广播
    public string lastSender;

    /// <summary>
    /// 事件的启用
    /// </summary>
    /// <param name="value"></param>
    /// <param name="sender">广播者</param>
    public void RaseEvent(T value,object sender)
    {
        onEventRised?.Invoke(value);
        lastSender = sender.ToString();
    }
}
