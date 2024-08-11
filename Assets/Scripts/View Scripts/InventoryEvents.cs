using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class InventoryEvents
{
    public event Action<InventoryItem, Vector2Int> ItemMovedInInventory;
    public event Action<InventoryItem, Vector2Int, int> ItemDropedToInventoryFromList;
    public event Action<InventoryItem> ItemClicked;

    public void NotifyItemDroppedToInventoryFromList(InventoryItem item, Vector2Int inventoryPosition, int index)
    {
        ItemDropedToInventoryFromList?.Invoke(item, inventoryPosition, index);
    }

    public void NotifyItemMovedInInventory(InventoryItem item, Vector2Int inventoryPosition)
    {
        ItemMovedInInventory?.Invoke(item, inventoryPosition);
    }

    public void NotifyItemClicked(InventoryItem item)
    {
        ItemClicked?.Invoke(item);
    }
}

