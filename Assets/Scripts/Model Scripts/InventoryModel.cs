using System;
using UnityEngine;

public class InventoryModel
{
    private readonly int gridWidth;
    private readonly int gridHeight;
    public InventoryItem[,] Grid { get; private set; }
    
    public InventoryModel(int gridWidth = 10, int gridHeight = 10)
    {
        if (gridWidth <= 0 || gridHeight <= 0) throw new ArgumentException("Grid size must be above zero.");
        this.gridWidth = gridWidth;
        this.gridHeight = gridHeight;
        Grid = new InventoryItem[gridWidth, gridHeight];
    }

    public bool AddItem(InventoryItem item, Vector2Int position)
    {
        if (IsPlacementPossible(item, position))
        {
            RemoveItemIfExists(item);
            int leftX = position.x;
            int rightX = leftX + item.data.Size.x - 1;
            int topY = position.y;
            int bottomY = topY + item.data.Size.y - 1;

            for (int x = leftX; x <= rightX; x++)
            {
                for (int y = topY; y <= bottomY; y++)
                {
                    Grid[x, y] = item;
                }
            }
            item.InventoryPosition = position;
            return true;
        }
        return false;
    }

    private void RemoveItemIfExists(InventoryItem item)
    {
        if (item.InventoryPosition != null)
        {
            RemoveItemAt(item.InventoryPosition);
        }
    }

    public void RemoveItemAt(Vector2Int position)
    {
        if (position.x < 0 || position.x >= gridWidth)
            return;
        if (position.y < 0 || position.y >= gridHeight) 
            return;
        
        InventoryItem item = Grid[position.x, position.y];
        if (item == null) return;

        int leftX = item.InventoryPosition.x;
        int rightX = leftX + item.data.Size.x - 1;
        int topY = item.InventoryPosition.y;
        int bottomY = topY + item.data.Size.y - 1;

        for (int x = leftX; x <= rightX; x++)
        {
            for (int y = topY; y <= bottomY; y++)
            {
                Grid[x, y] = null;
            }
        }
    }

    private bool IsPlacementPossible(InventoryItem item, Vector2Int position)
    {
        int leftX = position.x;
        int rightX = leftX + item.data.Size.x - 1;
        int topY = position.y;
        int bottomY = topY + item.data.Size.y -1;

        if (leftX < 0 || rightX < 0 || leftX >= gridWidth || rightX >= gridWidth)
            return false;
        if (topY < 0 || bottomY < 0 || topY >= gridHeight || bottomY >= gridHeight)
            return false;

        for (int x = leftX; x <= rightX; x++)
        {
            for (int y = topY; y <= bottomY; y++)
            {
                if (Grid[x, y] != null && Grid[x, y] != item)
                    return false;
            }
        }

        return true;
    }
}
