using System;
using UnityEngine;
using UnityEngine.Events;

public class BaseEventListener<T> : MonoBehaviour
{
    public BaseEventSO<T> eventSO;
    public UnityEvent<T> response;

    private void OnEnable()
    {
        if (eventSO != null)
        {
            eventSO.onEventRised += OnEventRised;
        }
    }

    private void OnDisable()
    {
        eventSO.onEventRised -= OnEventRised;
    }

    private void OnEventRised(T value)
    {
        response?.Invoke(value);
    }
}
