using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotView : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private float brightnessMultiplier = 0.7f;
    public Vector2Int InventoryPosition { get; private set; }
    private InventoryView inventoryView;
    private Image image;
    private UnityEngine.Color initialColor;

    public void Initialize(InventoryView view, int x, int y)
    {
        InventoryPosition = new Vector2Int(x, y);
        inventoryView = view;
        image = GetComponent<Image>();
        initialColor = image.color;
    }

    public void OnDrop(PointerEventData eventData)
    {
        var itemView = eventData.pointerDrag.GetComponent<InventoryItemView>();
        if (itemView != null)
        {
            if (itemView is ListedItemView listedItemView)
            {
                inventoryView.NotifyItemDroppedFromList(itemView.Item, InventoryPosition, listedItemView.Index);
            }
            else
            {
                inventoryView.NotifyItemDroppedFromInventory(itemView.Item, InventoryPosition);
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = initialColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Color.RGBToHSV(image.color, out float h, out float s, out float v);
        v *= brightnessMultiplier;
        v = Mathf.Clamp01(v);
        image.color = Color.HSVToRGB(h, s, v);
    }
}
