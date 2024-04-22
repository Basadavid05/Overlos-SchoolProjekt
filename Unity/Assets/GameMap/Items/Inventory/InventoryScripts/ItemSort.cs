using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/*
public class ItemSort : MonoBehaviour
{
    public static ItemSort instance;
    public List<Item> items = new List<Item>();

    //public InventorySlot[] inventorySlots;
    public GameObject Toolbar;

    public Transform ItemContent;
    public GameObject InventoryItem;


    private void Awake()
    {
        instance = this;
    }

    public void Add(Item item)
    {
        items.Add(item);
        InventorySlot[] inventorySlots = Toolbar.GetComponentsInChildren<InventorySlot>();
        if (item.InventoryPlacement == Item.ItemType.CanBeInToolbar)
        {
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                //InventorySlot slot = inventorySlots[i];
                InventoryItem inventoryItem = slot.GetComponentInChildren<InventoryItem>();
                if (inventoryItem == null)
                {
                    SpawnNewItem(item, slot);
                    return;
                }
            }
        }
        else
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var ItemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            var ItemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            ItemName.text = item.ItemName;
            ItemIcon.sprite = item.Icon;
        }
    }

    public void Remove(Item item)
    {
        items.Remove(item);
    }

    public void listItems()
    {
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }

        /*foreach (Item item in items)
        {
            if (item.InventoryPlacement == Item.ItemType.CanBeInToolbar)
            {
                for (int i = 0; i < inventorySlots.Length; i++)
                {
                    InventorySlot slot = inventorySlots[i];
                    InventoryItem ItemInSlot = slot.GetComponentInChildren<InventoryItem>();
                    if (ItemInSlot == null)
                    {
                        SpawnNewItem(item, slot);
                    }
                }
            }
            else
            {
                GameObject obj = Instantiate(InventoryItem, ItemContent);
                var ItemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
                var ItemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();

                ItemName.text = item.ItemName;
                ItemIcon.sprite = item.Icon;
            }
            
        }

        
    }

    }
    /*void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(InventoryItem, slot.transform);
        InventoryItem inventoryitem = newItemGo.GetComponentInChildren<InventoryItem>();
        var ItemIcon = inventoryitem.transform.Find("ItemIcon").GetComponent<Image>();
        ItemIcon.sprite = item.Icon;
    }
}
*/