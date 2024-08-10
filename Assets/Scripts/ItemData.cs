using UnityEngine;

[CreateAssetMenu(fileName ="ItemData", menuName = "ScriptableObjects/ItemData")]
public class ItemData : ScriptableObject
{
    [SerializeField] private string itemName;
    [SerializeField]
    [Range(0f, 1f)]
    private float condition;
    [SerializeField] private Sprite image;
    [SerializeField] private Vector2Int size;

    public string Name => itemName;
    public float Condition=> condition;
    public Sprite Image => image;
    public Vector2Int Size => size;
}
