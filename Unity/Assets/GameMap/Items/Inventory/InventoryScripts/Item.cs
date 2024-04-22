using UnityEngine;

[CreateAssetMenu(fileName ="New Item", menuName ="Item/Create New Item ")]
public class Item : ScriptableObject
{

    [Header("Only gameplay")]
    public string ItemName;
    public ItemType InventoryPlacement;

    [Header("Only UI")]
    public bool stackable=false;

    [Header("Both")]
    public Sprite Icon;
    public GameObject Prefab;
    public int MaxItem=1;

    [Header("Hide-Stuff")]
    [HideInInspector] public GameObject DropLocation;
    [HideInInspector] public bool Toolbar=false;
    [HideInInspector] public int Toolbarnumber;
    [HideInInspector] public int quantity=1;

    [Header("Soul Price")]
    public int SoulPrice;
    public string Code;

    public bool potion;

    public int heal;

    public enum ItemType
    {
        CanBeInToolbar,
        CannotBeInToolbar,
    }
}
