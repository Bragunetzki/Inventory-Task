using UnityEngine;

public class InventoryItem
{
    public readonly ItemData data;
    public float Condition { get; set; }
    public Vector2Int InventoryPosition { get; set; }

    public InventoryItem(ItemData data)
    {
        this.data = data;
        Condition = data.Condition;
    }
}
