using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableInventoryItem
{
    public string itemName;
    public Vector2Int position;
    public float condition;

    public SerializableInventoryItem(string itemName, Vector2Int position, float condition)
    {
        this.itemName = itemName;
        this.position = position;
        this.condition = condition;
    }
}

[System.Serializable]
public class SerializableInventory
{
    public List<SerializableInventoryItem> items = new();
}
