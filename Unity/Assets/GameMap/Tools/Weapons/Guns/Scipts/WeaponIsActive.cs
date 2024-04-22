using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WeaponIsActive : MonoBehaviour
{
    [Header("Weapon-Comp")]
    [HideInInspector] public int counting = 0;
    FireWithWeapon weapon;
    BulletUI bullet;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        weapon =GetComponent<FireWithWeapon>();
        bullet =GetComponent<BulletUI>();
        if (counting==0)
        {
            rb.transform.rotation = Quaternion.Euler(0, 0, 90);
            weapon.enabled = false;
            bullet.enabled = false;
        }
    }

    void Update()
    {
        if (transform.parent != null && transform.parent.name.Contains("ToolbarSlotHolder") && gameObject.activeSelf)
        {
            weapon.enabled = true;
            bullet.enabled = true;
            ItemPlacement.instance.bullet.SetActive(true);
            if (counting != 2)
            {
                counting++;
            }

        }
        else
        {
            rb.transform.eulerAngles = new Vector3(rb.transform.eulerAngles.x, rb.transform.eulerAngles.y, rb.transform.eulerAngles.z); 
            weapon.enabled = false;
            bullet.enabled = false;
            ItemPlacement.instance.bullet.SetActive(false);
        }
    }

}
