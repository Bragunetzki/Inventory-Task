using System;
using UnityEngine;
using UnityEngine.UI;


public class InventoryItemView : MonoBehaviour
{
    public InventoryItem Item { get; private set; }
    public event Action<InventoryItemView> ItemClicked;

    private RectTransform rectTransform;
    private Image itemIcon;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        itemIcon = GetComponent<Image>();
    }

    public void Initialize(InventoryItem item, int slotSize, int spacing)
    {
        Item = item;
        itemIcon.sprite = item.data.Image;
        float sizeX = slotSize * item.data.Size.x + spacing * (item.data.Size.x - 1);
        float sizeY = slotSize * item.data.Size.y + spacing * (item.data.Size.y - 1);
        rectTransform.sizeDelta = new Vector2(sizeX, sizeY);
    }

    internal void NotifyItemClicked()
    {
        ItemClicked?.Invoke(this);
    }
}
