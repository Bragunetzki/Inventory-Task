using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotView : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private float brightnessMultiplier = 0.7f;    
    public Vector2Int InventoryPosition { get; private set; }
    private InventoryEvents inventoryEvents;
    private Image image;
    private UnityEngine.Color initialColor;

    public void Initialize(InventoryEvents inventoryEvents, int x, int y)
    {
        this.inventoryEvents = inventoryEvents;
        InventoryPosition = new Vector2Int(x, y);
        image = GetComponent<Image>();
        initialColor = image.color;
    }

    public void OnDrop(PointerEventData eventData)
    {
        var itemView = eventData.pointerDrag.GetComponent<InventoryItemView>();
        if (itemView != null)
        {
            if (itemView is ListedItemView listedItemView)
            {
                inventoryEvents.NotifyItemDroppedToInventoryFromList(itemView.Item, InventoryPosition, listedItemView.Index);
            }
            else
            {
                inventoryEvents.NotifyItemMovedInInventory(itemView.Item, InventoryPosition);
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = initialColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Color.RGBToHSV(image.color, out float h, out float s, out float v);
        v *= brightnessMultiplier;
        v = Mathf.Clamp01(v);
        image.color = Color.HSVToRGB(h, s, v);
    }
}
