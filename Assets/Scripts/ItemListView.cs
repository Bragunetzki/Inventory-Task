using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemListView : MonoBehaviour
{
    [SerializeField] private RectTransform itemListPanel;
    [SerializeField] private InventoryItemView itemPrefab;
    [SerializeField] private float spacing = 5;
    [SerializeField] private int slotSize = 27;
    [SerializeField] private int selectionSize = 5;
    [SerializeField] private List<ItemData> possibleItems;

    private VerticalLayoutGroup verticalLayoutGroup;
    private InventoryItemView[] slots;

    private void Awake()
    {
        verticalLayoutGroup = itemListPanel.GetComponent<VerticalLayoutGroup>();
        Initialize(possibleItems);
    }

    public void Initialize(List<ItemData> items)
    {
        slots = new InventoryItemView[selectionSize];

        verticalLayoutGroup.spacing = spacing;

        for (int i = 0; i < selectionSize; i++) {
            InventoryItemView itemView = Instantiate(itemPrefab, itemListPanel);
            slots[i] = itemView;

            int randIndex = Random.Range(0, items.Count);
            itemView.Initialize(items[randIndex], slotSize);
        }
    }
}
