using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlotView : MonoBehaviour, IDropHandler
{
    public Vector2Int InventoryPosition { get; private set; }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData == null) return;

        RectTransform targetRectTransform = GetComponent<RectTransform>();
        RectTransform draggedRectTransform = eventData.pointerDrag.GetComponent<RectTransform>();

        Vector3 targetCenter = targetRectTransform.position;
        draggedRectTransform.position = targetCenter;
    }

    public void Initialize(int x, int y)
    {
        InventoryPosition = new Vector2Int(x, y);
    }
}
