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
        if (!TryGetComponent(out rectTransform))
        {
            throw new ArgumentNullException("Rect transform not found on drag handler.");
        }
        if (!TryGetComponent(out canvasGroup))
        {
            throw new ArgumentNullException("Canvas group not found on drag handler.");
        }
        if (!TryGetComponent(out itemView))
        {
            throw new ArgumentNullException("InventoryItemView not found on drag handler.");
        }
        canvas = GetComponentInParent<Canvas>();
        if (canvas == null)
        {
            throw new ArgumentNullException("Canvas not found in parents of drag handler.");
        }

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = rectTransform.anchoredPosition;
        originalParent = rectTransform.parent;
        canvasGroup.blocksRaycasts = false;
        transform.SetParent(canvas.transform, true);

        Vector2 pivotOffset = new(rectTransform.rect.width * -rectTransform.pivot.x * 0.95f, rectTransform.rect.height * rectTransform.pivot.y * 0.95f);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out Vector2 localMousePosition);
        rectTransform.anchoredPosition += localMousePosition - pivotOffset;
        isDragging = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(originalParent, true);
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
