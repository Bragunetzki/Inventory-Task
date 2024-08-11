using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private Vector2 originalPosition;
    private Transform originalParent;
    private Canvas canvas;
    private bool isDragging;
    private InventoryItemView itemView;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        if (rectTransform == null)
        {
            throw new ArgumentNullException("Rect transform not found on drag handler.");
        }
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            throw new ArgumentNullException("Canvas group not found on drag handler.");
        }
        canvas = GetComponentInParent<Canvas>();
        if (canvas == null)
        {
            throw new ArgumentNullException("Canvas not found in parents of drag handler.");
        }

        itemView = GetComponent<InventoryItemView>();
        if (itemView == null)
        {
            throw new ArgumentNullException("InventoryItemView not found on drag handler.");
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = rectTransform.anchoredPosition;
        originalParent = rectTransform.parent;
        canvasGroup.blocksRaycasts = false;
        Vector2 pivotOffset = new(rectTransform.rect.width * -rectTransform.pivot.x * 0.95f, rectTransform.rect.height * rectTransform.pivot.y * 0.95f);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out Vector2 localMousePosition);
        rectTransform.anchoredPosition += localMousePosition - pivotOffset;
        isDragging = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        ReturnToOriginalPosition();

        canvasGroup.blocksRaycasts = true;
        isDragging = false;
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

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Ayo");
        if (!isDragging)
        {
            itemView.NotifyItemClicked();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        return;
    }
}
