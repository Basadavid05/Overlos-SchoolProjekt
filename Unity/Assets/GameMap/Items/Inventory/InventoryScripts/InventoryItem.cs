using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.EventSystems;
using static Item;

public class InventoryItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Elemets from another Scripts")]
    public UnityEngine.UI.Image image;
    public UnityEngine.UI.Image InventoryTracker;
    public string Itemname;
    public Item item;
    public int counter = 1;
    public int PrivilousCounter = 1;
    public TextMeshProUGUI textcount;
    private TextMeshProUGUI ObjName;
    public Transform parentAfterDrag;
    public Transform ObjectPlace;
    public Transform PickUpGroup;
    public bool ItemisFull;
    public bool PlaceIsBusy;
    public Color CurrentColor;
    public int itemIndex;


    [Header("DraggedSrt")]
    private bool isMouseOver;
    private bool IsDragging;
    private int maximum;
    public GameObject duplicate;
    bool needcheck = false;


    private void Start()
    {
        if (item != null)
        {
            Itemname = item.name;
            item.Toolbarnumber = -1;
        }
        InventoryTracker = transform.gameObject.GetComponent<UnityEngine.UI.Image>();
        import();
        if (!duplicate)
        {
            CountItemIndex();
        }

        if (transform.tag == "Toolbar-Items")
        {
            CurrentColor.a = 0;
            if (ItemPlacement.instance.InventoryItemPlaces.Count <10 && ItemPlacement.instance.InventoryItemPlaces.Count > 0)
            {
                ItemPlacement.instance.ToolbarPlaces.Insert(itemIndex, this.gameObject.transform);
                ItemPlacement.instance.InventoryItemPlaces.Insert(itemIndex, this);
                //ItemPlacement.instance.Lister();
                ItemPlacement.instance.ChangePictures(itemIndex, ItemPlacement.instance.Background, 0);

            }
            ObjectPlace = ItemPlacement.instance.Placer(itemIndex);
        }
        if(transform.tag == "Inventory-Items")
        {
            ObjName= transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            ObjName.text = Itemname;
        }

    }
    public void CountItemIndex()
    {
        itemIndex = -1; // Az indexek 0-tól kezdődnek, ezért -1-től indulunk

        // Iterate through the parent's children and find the index of the current GameObject
        for (int i = 0; i < transform.parent.childCount; i++)
        {
            Transform child = transform.parent.GetChild(i);

            // Ha megtaláltuk a jelenlegi GameObject-et, akkor eltároljuk az indexét
            if (child == transform)
            {
                itemIndex = i;
                break; // Mivel megtaláltuk, nincs értelme tovább iterálni
            }
        }
    }

    private void import()
    {
        image = transform.Find("InventoryItem").GetComponent<UnityEngine.UI.Image>();
        textcount = image.transform.Find("Count").GetComponent<TextMeshProUGUI>();
    }

    //Drag and Drop
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            // Create a duplicate of the original object
            IsDragging = true;
            duplicate = Instantiate(gameObject, transform.parent);
            duplicate.transform.SetSiblingIndex(itemIndex);
            if (transform.tag == "Inventory-Items")
            {
                ObjName.text = " ";
            }
            parentAfterDrag = transform.parent;
            transform.SetParent(ItemPlacement.instance.DragPlace);
            transform.SetAsLastSibling();
            InventoryTracker.raycastTarget = false;
            image.raycastTarget = false;
        }
        
    }

    // Implementing IDragHandler
    public void OnDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            IsDragging=true;
            transform.position = Input.mousePosition;
        }
    }

    // Implementing IEndDragHandler
    public void OnEndDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            if (eventData.pointerEnter != null)
            {
                HandleDrop(eventData.pointerEnter.gameObject);
            }
            InventoryTracker.raycastTarget = true;
            if(transform.tag!= ("Toolbar-Items"))
            {
                image.raycastTarget = true;
            }
            IsDragging = false;
        }

    }

    private void HandleDrop(GameObject dropTarget)
    {
        if (dropTarget.name == "Dropitems" && item.name!="Knife")
        {
            Drop(true);
            Placing();
        }
        else
        {
            if (transform.tag == ("Toolbar-Items"))
            {
                if (dropTarget.tag == ("Toolbar-Items"))
                {
                    transform.SetParent(ItemPlacement.instance.ToolbarContent);
                    int TargetNumber = dropTarget.GetComponent<InventoryItem>().itemIndex;
                    ChangeLocation(ObjectPlace.gameObject, dropTarget.GetComponent<InventoryItem>().ObjectPlace.gameObject);
                    Change(dropTarget.gameObject);
                    Placing();
                    LocationSwitch(dropTarget.GetComponent<InventoryItem>().itemIndex, itemIndex,dropTarget);

                }
                else if (dropTarget.tag == ("Inventory-Items") || dropTarget.name == ("MainInventoryViewport") && !ItemPlacement.instance.FullInv)
                {
                        item.Toolbarnumber = -1;
                        item.Toolbar=false;

                    ItemPlacement.instance.UpdateUI(item);
                    DestoryChilds();
                    transform.SetParent(parentAfterDrag);
                    RecreateScript();
                }
                else
                {
                    Placing();
                }
            }
            else if (transform.tag == ("Inventory-Items"))
            {
                if (dropTarget.tag == ("Inventory-Items"))
                {
                    transform.Find("ItemName").GetComponent<TextMeshProUGUI>().text = item.name;
                    transform.SetParent(ItemPlacement.instance.ItemContent);
                    if (dropTarget.name.Contains("InventoryItems"))
                    {
                        Change(dropTarget);
                    }
                    else if (dropTarget.name == "InventoryItem")
                    {
                        GameObject obj = dropTarget.transform.parent.gameObject;
                        Change(obj);
                    }
                    else
                    {
                        Placing();
                    }
                }
                else if (dropTarget.tag == ("Toolbar-Items") && item.InventoryPlacement == ItemType.CanBeInToolbar)
                {
                    if (!dropTarget.GetComponent<InventoryItem>().PlaceIsBusy)
                    {
                        transform.SetParent(ItemPlacement.instance.ToolbarContent);
                        int dropTargetObjectIndex = dropTarget.GetComponent<InventoryItem>().itemIndex;
                        item.Toolbarnumber = dropTargetObjectIndex;
                        ItemPlacement.instance.AddWeapon(item.Toolbarnumber, item);
                        dropTarget.GetComponent<InventoryItem>().ObjectPlace = ItemPlacement.instance.Placer(item.Toolbarnumber);
                        dropTarget.GetComponent<InventoryItem>().item = item;
                        ItemPlacement.instance.ChangePictures(dropTargetObjectIndex, item.Icon, 100);
                        Remove(item);
                        Destroy(this.gameObject);
                    }
                    else
                    {
                        Placing();
                    }

                }
                else if (dropTarget.name == ("InventoryBackround"))
                {
                    Placing();
                }
                else if (dropTarget.name == ("MainInventoryViewport"))
                {
                    Placing();
                }
                else
                {
                    Placing();
                }
            }
        }

        Destroy(duplicate);
        needcheck = true;
    }

    private void Placing()
    {
        transform.SetParent(parentAfterDrag);
        transform.SetSiblingIndex(itemIndex);
        if (transform.tag == "Inventory-Items")
        {
            ObjName.text = Itemname;
        }
    }

    public void Placinger()
    {
        transform.SetParent(transform.parent);
        transform.SetSiblingIndex(itemIndex);
    }

    private void Change(GameObject dropTarget)
    {
        // Get the number of objects at the drop spot
        int dropTargetObjectIndex = dropTarget.GetComponent<InventoryItem>().itemIndex;
        int currentItemIndex = itemIndex;
        string name = dropTarget.name;
        

        // Switch the sibling index of the two objects
        if (currentItemIndex < dropTargetObjectIndex)
        {
            transform.SetSiblingIndex(dropTargetObjectIndex);
            dropTarget.transform.SetSiblingIndex(currentItemIndex);

        }
        else
        {
            // Moving backward
            dropTarget.transform.SetSiblingIndex(currentItemIndex);
            transform.SetSiblingIndex(dropTargetObjectIndex);
        }
        // Update the itemIndex property for both objects
        itemIndex = dropTargetObjectIndex;
        dropTarget.GetComponent<InventoryItem>().itemIndex = currentItemIndex;

        // Update the names of the objects
        dropTarget.name = gameObject.name;
        gameObject.name = name;
    }

    private void ChangeLocation(GameObject Thiss, GameObject Target)
    {
        Transform dropTargetParent = null;
        dropTargetParent = Target.transform.parent;
        Target.transform.SetParent(Thiss.transform.parent);
        Thiss.transform.SetParent(dropTargetParent);
    }

    private void RecreateScript()
    {
        transform.SetParent(transform.parent);
        transform.SetSiblingIndex(itemIndex);
        ItemPlacement.instance.ListRemoveer(itemIndex);
        this.gameObject.AddComponent<InventoryItem>();
        Destroy(this);
    }

    private void LocationSwitch(int a, int b,GameObject target)
    {
        if (a < b)
        {
            ItemPlacement.instance.ListRemoveer(a);
            ItemPlacement.instance.ListAdder(a, target.transform, target.GetComponent<InventoryItem>(), target.transform.Find("SelecetionBackground").GetComponent<ToolbarColor>());
            ItemPlacement.instance.ListRemoveer(b);
            ItemPlacement.instance.ListAdder(b, transform, this, this.gameObject.transform.Find("SelecetionBackground").GetComponent<ToolbarColor>());
            /*ItemPlacement.instance.ListRemoveer(b);
            ItemPlacement.instance.ListAdder(b, target.transform, target.GetComponent<InventoryItem>());*/

        }
        else
        {
            ItemPlacement.instance.ListRemoveer(b);
            ItemPlacement.instance.ListAdder(b, transform, this, this.gameObject.transform.Find("SelecetionBackground").GetComponent<ToolbarColor>());
            ItemPlacement.instance.ListRemoveer(a);
            ItemPlacement.instance.ListAdder(a, target.transform, target.GetComponent<InventoryItem>(),target.transform.Find("SelecetionBackground").GetComponent<ToolbarColor>());

        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isMouseOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isMouseOver = false;
    }

    private void Update()
        {       

        if (item != null)
        {
            PlaceIsBusy = true;
        }
        else
        {
            PlaceIsBusy = false;
        }
        if (isMouseOver)
                {
            
                    if (Input.GetKeyDown(KeyCode.O) && item!=null && item.name != "Knife" && !item.potion)
                    {
                        //Call the Drop function
                        Drop(false);
                    }

                    if (Input.GetKeyDown(KeyCode.E) && item.potion)
                    {
                        if(item.ItemName.Contains("Heal +") && item.name!="poison")
                        {
                            if (PlayerDatas.PlayerMaxHealth != PlayerDatas.PlayerHP)
                            {
                                AnnihilateItem(item);
                                PlayerDatas.RestoreHealth(item.heal);
                            }
                        }

                        if (item.ItemName.Contains("invincibility"))
                        {
                            if (SpecialPotion.Invincible)
                            {
                                if (SpecialPotion.InvcCount > item.heal)
                                {
                                    SpecialPotion.BecomeImortal(item.heal);
                                    AnnihilateItem(item);
                                }
                                else
                                {
                                    
                                }
                            }
                            else
                                {
                                    SpecialPotion.BecomeImortal(item.heal);
                                    AnnihilateItem(item);
                                }
                        }

                        if (item.name == "poison")
                        {
                            AnnihilateItem(item);
                            PlayerDatas.Takedamage(10000);
                        }

            }
        }

            if (gameObject.activeSelf)
            {
                if (PrivilousCounter != counter)
                    {
                    textcount.text = counter.ToString();
                    PrivilousCounter = counter;
                    RefreshCount();
                }
                if (transform.tag == "Inventory-Items" && needcheck == true)
                {
                    ItemPlacement.instance.CheckALlIndex();
                    needcheck= false;
                    Placinger();
            }
            else if (needcheck)
                {
                    ItemPlacement.instance.CheckALlIndexToolbar();
                    needcheck = false;
                    Placinger();
                }

        }
    }


    public void RefreshCount()
    {
        textcount.text = counter.ToString();
        bool textActive = counter > 1;
        textcount.gameObject.SetActive(textActive);
        return;
    }


    public bool Max(int counting)
    {
        if (item != null && counting < item.MaxItem)
        {
            return true;
        }
        else
            return false;
    }

    private void Drop(bool every)
    {
        if (!every)
        {
            Dropitems();
        }
        else
        {

            maximum = counter;
                for (int i = 0; i < maximum; i++)
                {
                    Dropitems();
                }

        }
    }

    private void Dropitems()
    {
        // Instantiate a new item GameObject
        GameObject thrownItem = Instantiate(item.Prefab, Camera.main.transform.position, Quaternion.identity);
            thrownItem.transform.parent = PickUpGroup;

            if (item.Toolbarnumber != -1)
            {
                item.Toolbarnumber = -1;
                item.Toolbar = false;
            }

            // Apply force to the thrown item
            Rigidbody thrownItemRb = thrownItem.GetComponent<Rigidbody>();
            if (thrownItemRb != null)
            {
                thrownItemRb.AddForce(Camera.main.transform.forward * 4f, ForceMode.Impulse);
            }

            if (counter > 1)
            {
                counter--;
            }
            else
            {
                //Remove from list
                Remove(thrownItem.GetComponent<ItemTypesChanger>().item);
                if(transform.tag != "Toolbar-Items")
                {
                    Destroy(this.gameObject);
                }
                else
                {
                RecreateScript();
                DestoryChilds();
            }

        }
    }

    private void DestoryChilds()
    {
        foreach (Transform child in ObjectPlace)
        {
            Destroy(child.gameObject);
        }
    }


    private void Remove(Item item)
                {
                    //Remove from list
                    ItemPlacement.instance.Remove(item);
                    ItemPlacement.instance.InventoryRemover(this);
                }

    private void AnnihilateItem(Item item)
    {
        if (counter > 1)
        {
            counter--;
        }
        else
        {
            Remove(item);
            Destroy(this.gameObject);
        }
    }

}

