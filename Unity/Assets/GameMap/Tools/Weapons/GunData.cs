using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Gundatas",menuName ="Weapons/Guns")]
public class GunData : ScriptableObject
{
    [Header("Info")]
    public new string name;

    [Header("Shooting")]
    public int damage;
    public float maxdistance;

    [Header("Bullet")]
    public float BulletVelocity = 30f;
    public float bulletPrefabLifeTime = 20f;

    [Header("Reloding")]
    public float maxAmmo;
    public float magSize;
    public float currentAmmo;
    public float firerate;
    public float reloadTime;

    [HideInInspector]
    public bool reloading;

}
