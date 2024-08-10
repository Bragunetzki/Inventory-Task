using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private InventoryView inventoryView;
    [SerializeField] private ItemListView itemListView;
    [SerializeField] private int gridWidth = 10;
    [SerializeField] private int gridHeight = 10;
    [SerializeField] private List<ItemData> possibleItems;
    [SerializeField] private int listSelectionSize;

    private InventoryModel inventoryModel;
    private ItemListModel itemListModel;

    // Start is called before the first frame update
    void Start()
    {
        inventoryModel = new InventoryModel(gridWidth, gridHeight);
        itemListModel = new ItemListModel(possibleItems, listSelectionSize);

        inventoryView.ItemDropedFromList += OnItemDroppedFromList;
        inventoryView.ItemDroppedFromInventory += OnItemDroppedFromInventory;
        itemListView.ItemDropped += OnItemDroppedToInventory;

        inventoryView.Initialize(gridWidth, gridHeight);
        UpdateViews();
    }

    private void OnItemDroppedFromList(InventoryItem item, Vector2Int invPosition, int index)
    {
        bool result = inventoryModel.AddItem(item, invPosition);
        if (result) itemListModel.RemoveItemAt(index);
        UpdateViews();
    }

    private void OnItemDroppedFromInventory(InventoryItem item, Vector2Int invPosition)
    {
        inventoryModel.AddItem(item, invPosition);
        var grid = inventoryModel.Grid;
        UpdateViews();
    }

    private void OnItemDroppedToInventory(InventoryItem item)
    {
        inventoryModel.RemoveItemAt(item.InventoryPosition);
        itemListModel.AddItem(item);
        UpdateViews();
    }

    private void UpdateViews()
    {
        inventoryView.UpdateView(inventoryModel.Grid);
        itemListView.UpdateView(itemListModel.AvailableItems);
    }
}
