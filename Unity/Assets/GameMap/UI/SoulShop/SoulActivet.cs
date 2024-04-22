using UnityEngine;

public class SoulActivet : MonoBehaviour
{

    private Collider coll;
    public SoulShop soulShop;

    private void Start()
    {
        coll = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            if (Input.GetKeyDown(KeyCode.E) && !SoulShop.SoulShopActive)
            {
                soulShop.Switcher(true);
            }
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            if (Input.GetKeyDown(KeyCode.E) && !SoulShop.SoulShopActive)
            {
                soulShop.Switcher(true);
            }
        }

    }


}
