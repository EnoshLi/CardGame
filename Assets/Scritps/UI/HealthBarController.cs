using System;
using UnityEngine;
using UnityEngine.UIElements;

public class HealthBarController : MonoBehaviour
{
    [Header("Elements")] public Transform healthBarTransForm;
    private UIDocument healthBarDocument;
    private ProgressBar healthBar;

    private void Awake()
    {
        healthBarDocument = GetComponent<UIDocument>();
        healthBar=healthBarDocument.rootVisualElement.Q<ProgressBar>("HealthBar");
        MoveToWorldPosition(healthBar,healthBarTransForm.position,Vector2.zero);
    }

    private void MoveToWorldPosition(VisualElement element, Vector3 worldPosition,Vector2 size)
    {
        Rect rect = RuntimePanelUtils.CameraTransformWorldToPanelRect(element.panel, worldPosition, size, Camera.main);
        element.transform.position = rect.position;
    }
    [ContextMenu("Get UI Position")]
    public void Test()
    {
        healthBarDocument = GetComponent<UIDocument>();
        healthBar=healthBarDocument.rootVisualElement.Q<ProgressBar>("HealthBar");
        MoveToWorldPosition(healthBar,healthBarTransForm.position,Vector2.zero);
    }
}
