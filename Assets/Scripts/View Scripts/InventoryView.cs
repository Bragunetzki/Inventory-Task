using System;
using UnityEngine;
using UnityEngine.UI;

public class InventoryView : MonoBehaviour
{
    [SerializeField] private InventorySlotView slotPrefab;
    [SerializeField] private InventoryItemView itemPrefab;
    [SerializeField] private int cellSize = 27;
    [SerializeField] private int cellSpacing = 0;
    [SerializeField] private GridLayoutGroup gridLayoutGroup;

    public event Action<InventoryItem, Vector2Int> ItemDroppedFromInventory;
    public event Action<InventoryItem, Vector2Int, int> ItemDropedFromList;

    private int gridWidth;
    private int gridHeight;
    private InventorySlotView[,] slots;

    public void Initialize(int width, int height)
    {
        if (slots != null)
        {
            foreach (var slot in slots)
            {
                Destroy(slot);
            }
        }

        gridWidth = width;
        gridHeight = height;
        slots = new InventorySlotView[gridWidth, gridHeight];

        gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayoutGroup.constraintCount = gridWidth;
        gridLayoutGroup.cellSize = new Vector2(cellSize, cellSize);
        gridLayoutGroup.spacing = new Vector2(cellSpacing, cellSpacing);

        for (int y = 0; y < gridHeight; y++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                InventorySlotView slot = Instantiate(slotPrefab, gridLayoutGroup.transform);
                slots[x, y] = slot;
                slot.name = "Slot " + x + "; " + y;
                slot.Initialize(this, x, y);
            }
        }
    }

    public void UpdateView(InventoryItem[,] grid)
    {
        if (grid.GetLength(0) != gridWidth || (grid.GetLength(0) != 0 && grid.GetLength(1) != gridHeight)) 
            throw new ArgumentException("Data grid size differs from display grid size.");

        foreach (Transform child in transform)
        {
            if (child != null && child.CompareTag("Item"))
            {
                Destroy(child.gameObject);
            }
        }

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                InventoryItem item = grid[x, y];
                if (item != null && item.InventoryPosition.x == x && item.InventoryPosition.y == y)
                {
                    InventoryItemView itemObject = Instantiate(itemPrefab, transform);
                    itemObject.Initialize(item, cellSize, cellSpacing);

                    RectTransform itemRectTransform = itemObject.GetComponent<RectTransform>();
                    itemRectTransform.anchoredPosition = GetSlotPosition(x, y, item.data.Size.x, item.data.Size.y);
                    itemObject.tag = "Item";
                }
            }
        }
    }

    private Vector2 GetSlotPosition(int x, int y, int width, int height)
    {
        int x1 = x - gridWidth / 2;
        int y1 = -y + gridHeight / 2;
        Vector2 slotPosition = new(
            (x1 + 0.5f * width) * gridLayoutGroup.cellSize.x + gridLayoutGroup.spacing.x * x1,
            (y1 - 0.5f * height) * gridLayoutGroup.cellSize.y + gridLayoutGroup.spacing.y * y1
        );
        return slotPosition;
    }

    public void NotifyItemDroppedFromList(InventoryItem item, Vector2Int inventoryPosition, int index)
    {
        ItemDropedFromList?.Invoke(item, inventoryPosition, index);
    }

    public void NotifyItemDroppedFromInventory(InventoryItem item, Vector2Int inventoryPosition)
    {
        ItemDroppedFromInventory?.Invoke(item, inventoryPosition);
    }
}
