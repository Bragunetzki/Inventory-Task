using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class ItemListModel
{
    private readonly List<ItemData> possibleItems;
    private readonly List<InventoryItem> availableItems;

    public ReadOnlyCollection<InventoryItem> AvailableItems
    {
        get { return availableItems.AsReadOnly(); }
    }

    public ItemListModel(List<ItemData> possibleItems, int selectionSize)
    {
        this.possibleItems = possibleItems;
        availableItems = new List<InventoryItem>();

        for (int i = 0; i < selectionSize; i++)
        {
            int randIndex = Random.Range(0, possibleItems.Count);
            InventoryItem item = new(possibleItems[randIndex]);
            availableItems.Add(item);
        }
    }

    public void RemoveItemAt(int index)
    {
        availableItems.RemoveAt(index);
    }

    public void AddItem(InventoryItem item)
    {
        availableItems.Add(item);
    }
}
