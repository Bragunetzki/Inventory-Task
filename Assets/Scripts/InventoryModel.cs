using System;
using UnityEngine;

public class InventoryModel
{
    private readonly int gridWidth;
    private readonly int gridHeight;
    private readonly InventoryItem[,] grid;
    
    public InventoryModel(int gridWidth = 10, int gridHeight = 10)
    {
        if (gridWidth <= 0 || gridHeight <= 0) throw new ArgumentException("Grid size must be above zero.");
        this.gridWidth = gridWidth;
        this.gridHeight = gridHeight;
        grid = new InventoryItem[gridWidth, gridHeight];
    }

    public bool AddItem(InventoryItem item, Vector2Int position)
    {
        if (IsPlacementPossible(item, position))
        {
            int leftX = position.x;
            int rightX = leftX + item.data.Size.x;
            int topY = position.y;
            int bottomY = topY + item.data.Size.y;

            for (int x = leftX; x <= rightX; x++)
            {
                for (int y = topY; y <= bottomY; y++)
                {
                    grid[x, y] = item;
                }
            }
            item.InventoryPosition = position;
            return true;
        }
        return false;
    }

    public void RemoveItem(Vector2Int position)
    {
        if (position.x < 0 || position.x >= gridWidth)
            return;
        if (position.y < 0 || position.y >= gridHeight) 
            return;
        
        InventoryItem item = grid[position.x, position.y];
        if (item == null) return;

        int leftX = item.InventoryPosition.x;
        int rightX = leftX + item.data.Size.x;
        int topY = item.InventoryPosition.y;
        int bottomY = topY + item.data.Size.y;

        for (int x = leftX; x <= rightX; x++)
        {
            for (int y = topY; y <= bottomY; y++)
            {
                grid[x, y] = null;
            }
        }
    }

    private bool IsPlacementPossible(InventoryItem item, Vector2Int position)
    {
        int leftX = position.x;
        int rightX = leftX + item.data.Size.x;
        int topY = position.y;
        int bottomY = topY + item.data.Size.y;

        if (leftX < 0 || rightX < 0 || leftX >= gridWidth || rightX >= gridWidth)
            return false;
        if (topY < 0 || bottomY < 0 || topY >= gridHeight || bottomY >= gridHeight)
            return false;

        for (int x = leftX; x <= rightX; x++)
        {
            for (int y = topY; y <= bottomY; y++)
            {
                if (grid[x, y] != null)
                    return false;
            }
        }

        return true;
    }
}
