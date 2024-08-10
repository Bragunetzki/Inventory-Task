using UnityEngine;
using UnityEngine.UI;

public class InventoryView : MonoBehaviour
{
    [SerializeField] private RectTransform inventoryPanel;
    [SerializeField] private InventorySlotView slotPrefab;
    [SerializeField] private InventoryItemView itemPrefab;
    [SerializeField] private Vector2 cellSize = new(40, 40);
    [SerializeField] private Vector2 cellSpacing = new(5, 5);

    private GridLayoutGroup gridLayoutGroup;
    private int gridWidth;
    private int gridHeight;
    private InventorySlotView[,] slots;

    private void Awake()
    {
        gridLayoutGroup = inventoryPanel.GetComponent<GridLayoutGroup>();
        Initialize(10, 10);
    }

    public void Initialize(int width, int height)
    {
        gridWidth = width;
        gridHeight = height;
        slots = new InventorySlotView[gridWidth, gridHeight];

        gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayoutGroup.constraintCount = gridWidth;
        gridLayoutGroup.cellSize = cellSize;
        gridLayoutGroup.spacing = cellSpacing;

        for (int y = 0; y < gridHeight; y++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                InventorySlotView slot = Instantiate(slotPrefab, inventoryPanel);
                slots[x, y] = slot;
                slot.name = "Slot " + x + "; " + y;
                slot.Initialize(x, y);
            }
        }
    }
}
