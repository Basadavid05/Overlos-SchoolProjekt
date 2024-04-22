using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Scipts")]
    public GameObject weaponhold;
    public GameObject WeaponHolderUI;
    bool GunHoldisActive;


    private void Start()
    {
        WeaponHolderUI.SetActive(false);    
    }

    // Update is called once per frame
    void Update()
    {
        if (GunHoldisActive)
        {
            WeaponHolderUI.SetActive(true);
        }
        else
        {
            WeaponHolderUI.SetActive(false);
        }
    }
    private void FixedUpdate()
    {
        GunHoldisActive = weaponhold.GetComponent<WeaponHold>().IsHold;
    }
}
