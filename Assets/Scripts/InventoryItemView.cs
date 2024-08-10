using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemView : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private ItemData itemData;
    private Image itemIcon;
    private Canvas canvas;

    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private Vector2 originalPosition;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        itemIcon = GetComponent<Image>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void Initialize(ItemData itemData, int slotSize)
    {
        this.itemData = itemData;
        itemIcon.sprite = itemData.Image;
        rectTransform.sizeDelta = new Vector2(slotSize, slotSize);
        rectTransform.sizeDelta = new Vector2(slotSize * itemData.Size.x, slotSize * itemData.Size.y);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = rectTransform.anchoredPosition;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!eventData.pointerEnter || eventData.pointerEnter.GetComponent<InventorySlotView>() == null)
        {
            rectTransform.anchoredPosition = originalPosition;
        }
        canvasGroup.blocksRaycasts = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
}
