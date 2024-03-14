
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(BaseEventSO<>))]
public class BaseEventSOEditor<T> : Editor
{
    private void OnEnable()
    {
        if (baseEventSO==null)
        {
            baseEventSO = target as BaseEventSO<T>;
        }
    }
    private BaseEventSO<T> baseEventSO;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.LabelField("订阅数量"+GetListeners().Count);
        foreach (var listener in GetListeners())
        {
            EditorGUILayout.LabelField(listener.ToString());
        }
    }

    

    private List<MonoBehaviour> GetListeners()
    {
        List<MonoBehaviour> listeners = new();
        if (baseEventSO==null|| baseEventSO.onEventRised==null)
        {
            return listeners;
        }
        var subscribers = baseEventSO.onEventRised.GetInvocationList();
        foreach (var subscriber in subscribers)
        {
            var obj = subscriber.Target as MonoBehaviour;
            if (!listeners.Contains(obj))
            {
                listeners.Add(obj);
            }
        }

        return listeners;
    }
}