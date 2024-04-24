using UnityEngine;

public class InventoryController : MonoBehaviour
{

    [Header("Inventory")]
    public static bool InventoryOpen = false;
    private GameObject Inventory;
    private GameObject InventoryDrop;

    private void Start()
    {
        InventoryOpen = false;
        Inventory = transform.Find("InventoryBackround").gameObject;
        InventoryDrop = transform.parent.Find("Dropitems").gameObject;
        Inventory.SetActive(false);
        InventoryDrop.SetActive(false);

    }

    // Update is called once per frame
    private void Update()
    {
        if (!PauseMenu.GameIsPause && !MapControl.MapIsEnabled && !SoulShop.SoulShopActive && !PlayerDatas.Death)
        {
                if (Input.GetKeyDown(KeyCode.I))
                {
                    if (InventoryOpen)
                    {
                    InventoryControll(false);
                    }
                    else
                    {
                    InventoryControll(true);
                        //ItemSort.instance.listItems();
                    }
                }
        }

        if(PlayerDatas.Death && InventoryOpen)
        {
            InventoryOpen = false;
            Inventory.SetActive(false);
            InventoryDrop.SetActive(false);
        }
    }

    public void InventoryControll(bool status)
    {
        InventoryOpen = status;
        Inventory.SetActive(status);
        InventoryDrop.SetActive(status);
        Main.main.Lock(status);
    }
}
