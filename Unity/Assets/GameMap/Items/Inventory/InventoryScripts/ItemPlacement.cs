using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Item;


public class ItemPlacement : MonoBehaviour
{
    [Header("Instance")]
    public static ItemPlacement instance;

    [Header("Main-Inventory-Holder")]
    [HideInInspector] public List<InventoryItem> Inventorys = new List<InventoryItem>();

    [Header("Toolbar Inventory Placemets")]
    [HideInInspector] public List<Transform> InventoryPlaces = new List<Transform>();
    [HideInInspector] public List<Transform> ToolbarPlaces = new List<Transform>();

    [Header("Toolbar-Slots")]
     [HideInInspector] public List<InventoryItem> InventoryItemPlaces = new List<InventoryItem>();
     [HideInInspector] public List<ToolbarColor> ToolbarUIPlaces = new List<ToolbarColor>();
     public Sprite Background;

    [Header("Main-Inventory")]
    [HideInInspector] public List<Item> items = new List<Item>();

    [Header("Replacement")]
    [HideInInspector] public Transform ReplacePlace;
    [HideInInspector] public Transform DragPlace;

    [Header("InventoryContent")]
    public Transform ItemContent;
    public Transform ToolbarContent;
    [HideInInspector] public Transform ToolbarPhysicContent;
    public GameObject InventoryItem;
    public bool FullInv = false;

    [Header("Toolbar")]
    private int selectedSlot = -1;
    public KeyCode ZeroItem = KeyCode.H;
    private bool SomethingIsOpen = false;
    private int previous;

    [Header("Item-Drop-Location")]
    public Transform ItemDropLocation;

    [Header("Gun")]
    public TextMeshProUGUI _bullet;
    [HideInInspector] public GameObject bullet;

    [Header("Center")]
    public Pointers Pointers;
    public GameObject player;
    public Camera Backup;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        DragPlace = ToolbarContent.parent;
        ToolbarPhysicContent =transform.GetChild(0);
        ChangeSelectedSlot(10);
        bullet = _bullet.gameObject.transform.parent.gameObject;
        bullet.SetActive(false);
        ChildUpdate();
        Backup.gameObject.SetActive(false);
    }


    private void ChildUpdate()
    {
        ToolbarPlaces.Clear();
        InventoryItemPlaces.Clear();
        ToolbarUIPlaces.Clear();
        for (int i = 0; i < ToolbarPhysicContent.childCount; i++)
        {
            InventoryPlaces.Add(ToolbarPhysicContent.GetChild(i).transform);
            ToolbarPlaces.Add(ToolbarContent.GetChild(i).transform);
            InventoryItemPlaces.Add(ToolbarPlaces[i].GetComponent<InventoryItem>());
            ToolbarUIPlaces.Add(ToolbarPlaces[i].Find("SelecetionBackground").GetComponent<ToolbarColor>());
        }
    }

    private void Update()
    {
        if (Input.inputString != null)
        {
            bool isNumber = int.TryParse(Input.inputString, out int number);
            if (isNumber)
            {
                ChangeSelectedSlot(number);
                AnimationCheck(number);
                previous = number;
            }
        }
        if (Input.GetKey(ZeroItem))
        {
            ChangeSelectedSlot(10);
            MoveControl.ChangeAction("");
        }
        if (PauseMenu.GameIsPause || InventoryController.InventoryOpen || MapControl.MapIsEnabled)
        {
            ScriptEnabler(false);
            SomethingIsOpen = true;
        }
        else if (!PauseMenu.GameIsPause && !InventoryController.InventoryOpen && !MapControl.MapIsEnabled)
        {
            if (SomethingIsOpen)
            {
                ScriptEnabler(true);
                SomethingIsOpen = false;
            }
        }
        if (InventoryController.InventoryOpen)
        {
            ChangeSelectedSlot(10);
        }
        else
        {
            ChangeSelectedSlot(previous);
        }
    }

    private void ScriptEnabler(bool enable)
    {
        for (int i = 0; i < InventoryPlaces.Count; i++)
        {
            if (HasChildren(InventoryPlaces[i].gameObject))
            {
                MonoBehaviour[] scripts = InventoryPlaces[i].GetComponentsInChildren<MonoBehaviour>();

                if (enable)
                {
                    ScriptsManager.Enable(scripts);
                }
                else
                {
                    ScriptsManager.Disable(scripts);
                }
            }
        }

    }

    private void ChangeSelectedSlot(int newValue)
    {
        if (selectedSlot != newValue)
        {
            ItemSelect(newValue);
            previous = newValue;
            selectedSlot = newValue;
        }
    }

    private void ItemSelect(int number)
    {
        for (int i = 0; i < InventoryPlaces.Count; i++)
        {
            if (i == number)
            {
                InventoryEnabler(InventoryPlaces[i].gameObject, true);
                //ToolbarUIPlaces[i].SelectSlot();
            }
            else
            {
                InventoryEnabler(InventoryPlaces[i].gameObject, false);
                //ToolbarUIPlaces[i].DeselectSlot();
            }

        }
    }

    public void InventoryEnabler(GameObject parentObject, bool enable)
    {
        if (HasChildren(parentObject))
        {
            foreach (Transform child in parentObject.transform)
            {
                child.gameObject.SetActive(enable);
            }
        }
    }

    private bool HasChildren(GameObject gameObject)
    {
        return gameObject.transform.childCount > 0;
    }

    public void Add(Item item)
    {
        if (Inventorys.Count > 0)
        {
            for (int i = 0; i < Inventorys.Count; i++)
            {
                if (Inventorys[i].Itemname == item.ItemName && item.stackable == true && Inventorys[i].Max(Inventorys[i].counter))
                {
                    Inventorys[i].counter++;
                    return;
                }
            }
        }


        if (item.InventoryPlacement == ItemType.CanBeInToolbar)
        {
            for (int i = 0; i < InventoryPlaces.Count; i++)
            {
                if (InventoryPlaces[i].GetChild(0).childCount == 0)
                {
                    Transform placeTransform = InventoryPlaces[i].GetChild(0);
                    if (placeTransform != null)
                    {
                        GameObject obj = placeTransform.gameObject;

                        // Check if the InventoryItem component exists
                        InventoryItem inventoryItemComponent = ToolbarPlaces[i].GetComponent<InventoryItem>();
                        if (inventoryItemComponent != null)
                        {
                            inventoryItemComponent.ObjectPlace = placeTransform;
                            inventoryItemComponent.item = item;
                            ReplacePlace = placeTransform;
                            item.Toolbar = true;
                            item.Toolbarnumber = i;
                        }
                    }
                    ChangePictures(i, item.Icon, 100);

                    return;
                }
            }
        }
        if (items.Count <= 60)
        {
            FullInv = false;

            UpdateUI(item);
            return;
        }
        else
        {
            FullInv = true;
        }

    }

    public void UpdateUI(Item item)
    {
        GameObject obj = Instantiate(InventoryItem, ItemContent); 
        Inventorys.Add(obj.GetComponent<InventoryItem>());
        items.Add(item);
        item.quantity = 1;
        obj.transform.GetComponent<InventoryItem>().PickUpGroup = ItemDropLocation;    
        obj.GetComponent<InventoryItem>().ObjectPlace = ItemContent.transform.parent;


        var ItemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
        var ItemIcon = obj.transform.Find("InventoryItem").GetComponent<UnityEngine.UI.Image>();
        ItemName.text = item.ItemName;
        ItemIcon.sprite = item.Icon;
        obj.transform.GetComponent<InventoryItem>().item = item;
        obj.transform.GetComponent<InventoryItem>().Itemname = item.ItemName;
    }

    public void InventoryUpdate()
    {
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }
    }

    public void Remove(Item item)
    {

        if (item.Toolbar == true)
        {
            ChangePictures(item.Toolbarnumber, Background, 0);
        }
        items.Remove(item);
        return;
    }

    public void ChangePictures(int index, Sprite img, int Transparency)
    {
        InventoryItemPlaces[index].image.sprite = img;
        /*
        InventoryItemPlaces[index].image.color = new Color(InventoryItemPlaces[index].image.color.r,
        InventoryItemPlaces[index].image.color.g, InventoryItemPlaces[index].image.color.b, Transparency);
        */
        InventoryItemPlaces[index].image.color = new Color(0,0,0, Transparency);
    }

    public void InventoryRemover(InventoryItem items)
    {
        Inventorys.Remove(items);
    }

    public Transform Placer(int x)
    {
        for (int i = 0; i < InventoryPlaces.Count; i++)
        {
            if (x == i)
            {
                return InventoryPlaces[i].GetChild(0);
            }
        }
        return null;
    }

    public void AddWeapon(int a, Item item)
    {
        Transform place = null;
        for (int i = 0; i < InventoryPlaces.Count; i++)
        {
            if (a == i)
            {
                place = InventoryPlaces[i].GetChild(0);
                break;
            }
        }
        if (place != null)
        {
            GameObject thrownItem = Instantiate(item.Prefab, place.position, Quaternion.identity);
            thrownItem.name = item.name;
            // You might want to set the parent of the instantiated item to place or another appropriate parent
            thrownItem.transform.parent = place;
            transform.position = Vector3.zero;
        }

    }

    public void CheckALlIndex()
    {
        for (int i = 0; i < Inventorys.Count; i++)
        {
            if (Inventorys[i] != null && !Inventorys[i].GetComponent<InventoryItem>().duplicate)
            {
                Inventorys[i].GetComponent<InventoryItem>().Placinger();
                Inventorys[i].GetComponent<InventoryItem>().CountItemIndex();
                Inventorys[i].GetComponent<InventoryItem>().Placinger();
            }
        }
    }

    public void CheckALlIndexToolbar()
    {
        for (int i = 0; i < ToolbarPlaces.Count; i++)
        {
            if(ToolbarPlaces[i] != null && !ToolbarPlaces[i].GetComponent<InventoryItem>().duplicate)
            {
                ToolbarPlaces[i].GetComponent<InventoryItem>().Placinger();
                ToolbarPlaces[i].GetComponent<InventoryItem>().CountItemIndex();
                ToolbarPlaces[i].GetComponent<InventoryItem>().Placinger();
            }
        }
    }

    public void ListRemoveer(int index)
    {
        InventoryItemPlaces.RemoveAt(index);
        ToolbarPlaces.RemoveAt(index);
    }
    public void ListAdder(int a,Transform place,InventoryItem item, ToolbarColor toolbar)
    {
        InventoryItemPlaces.Insert(a,item);
        ToolbarPlaces.Insert(a,place);
        ToolbarUIPlaces.RemoveAt(a);
        ToolbarUIPlaces.Insert(a,toolbar);
    }
   
    public void Death()
    {
        for (int i = 0; i < InventoryPlaces.Count; i++)
        {
            if (InventoryPlaces[i].GetChild(0).childCount > 0)
            {
                InventoryPlaces[i].GetChild(0).GetChild(0).GetComponent<ItemDefault>().DropItDown();
            }
        }
        Inventorys.Clear();
        items.Clear();
        for (int i = 0; i < ItemContent.childCount; i++)
        {
            Destroy(ItemContent.GetChild(i).gameObject);
        }
        ToolbarContent.gameObject.SetActive(false);
        Backup.gameObject.SetActive(true);
        player.SetActive(false);
        
    }

    public void Resurect()
    {
        ToolbarContent.gameObject.SetActive(true);
        player.SetActive(true);
        Backup.gameObject.SetActive(false);
    }

    private void AnimationCheck(int number)
    {
        bool animation = HasChildren(InventoryPlaces[number].gameObject);

        if (animation)
        {

        }
        else
        {
            MoveControl.ChangeAction("");
        }
    }
}
