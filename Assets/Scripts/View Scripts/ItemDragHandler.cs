using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private Vector2 originalPosition;
    private Transform originalParent;
    private Canvas canvas;


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = rectTransform.anchoredPosition;
        originalParent = rectTransform.parent;
        canvasGroup.blocksRaycasts = false;
        Vector2 pivotOffset = new(rectTransform.rect.width * -rectTransform.pivot.x * 0.95f, rectTransform.rect.height * rectTransform.pivot.y * 0.95f);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out Vector2 localMousePosition);
        rectTransform.anchoredPosition += localMousePosition - pivotOffset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        ReturnToOriginalPosition();
        //if (eventData.pointerEnter == null || eventData.pointerEnter.GetComponent<InventoryView>() == null)
        //{
        //    ReturnToOriginalPosition();
        //}

        canvasGroup.blocksRaycasts = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void ReturnToOriginalPosition()
    {
        rectTransform.anchoredPosition = originalPosition;
        rectTransform.SetParent(originalParent);
    }
}
