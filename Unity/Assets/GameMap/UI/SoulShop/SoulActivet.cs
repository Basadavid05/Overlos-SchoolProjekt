using UnityEngine;

public class SoulActivet : MonoBehaviour
{

    private Collider coll;
    public SoulShop soulShop;
    private KeyCode key;

    private void Start()
    {
        coll = GetComponent<Collider>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (Main.main.ChangedHotkeys)
        {
            key = Main.main.keyMappings[Main.Hotkeys.interract];
        }

        if (other.CompareTag("MainCamera"))
        {
            if (Input.GetKeyDown(key) && !SoulShop.SoulShopActive && !InventoryController.InventoryOpen)
            {
                soulShop.Switcher(true);
            }
        }

    }



    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            if (Input.GetKeyDown(key) && !SoulShop.SoulShopActive)
            {
                soulShop.Switcher(true);
            }
        }

    }


}
