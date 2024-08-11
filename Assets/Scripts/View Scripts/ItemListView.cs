using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemListView : MonoBehaviour, IDropHandler
{
    [SerializeField] private ListedItemView itemPrefab;
    [SerializeField] private int spacing = 5;
    [SerializeField] private int slotSize = 27;

    public event Action<InventoryItem> ItemDroppedToListFromInventory;

    private VerticalLayoutGroup verticalLayoutGroup;
    private readonly List<ListedItemView> availableItems = new();

    private void Awake()
    {
        verticalLayoutGroup = GetComponent<VerticalLayoutGroup>();
        verticalLayoutGroup.spacing = spacing;
    }

    public void UpdateView(IList<InventoryItem> items)
    {
        foreach (ListedItemView item in availableItems)
        {
            Destroy(item.gameObject);
        }
        availableItems.Clear();

        for (int i = 0; i < items.Count; i++)
        {
            InventoryItem item = items[i];
            ListedItemView itemView = Instantiate(itemPrefab, verticalLayoutGroup.transform);
            availableItems.Add(itemView);

            itemView.Initialize(item, slotSize, spacing);
            itemView.Index = i;
        }
    }

    private void NotifyItemDropped(InventoryItem item)
    {
        ItemDroppedToListFromInventory?.Invoke(item);
    }

    public void OnDrop(PointerEventData eventData)
    {
        var itemView = eventData.pointerDrag.GetComponent<InventoryItemView>();
        if (itemView != null)
        {
            if (itemView is ListedItemView)
            {
                var dragHandler = itemView.GetComponent<ItemDragHandler>();
                if (dragHandler != null) dragHandler.ReturnToOriginalPosition();
            }
            else
            {
                NotifyItemDropped(itemView.Item);
            }
        }
    }
}
