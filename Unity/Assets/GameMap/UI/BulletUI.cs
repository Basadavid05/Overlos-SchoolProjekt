using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BulletUI : MonoBehaviour
{
    [Header("references")]
    public TextMeshProUGUI _bullet;
    [SerializeField] GunData GunDatas;

    private void Start()
    {
        _bullet = ItemPlacement.instance._bullet;
    }
    // Update is called once per frame
    void Update()
    {
        _bullet.text = GunDatas.magSize + " / " + GunDatas.currentAmmo;
    }

}

