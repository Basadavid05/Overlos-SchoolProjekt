using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy")]
public class Enemy : ScriptableObject
{
    [Header("General")]
    public string Name;
    public int Heal;
    public int SoulPrice;

    [Header("Materials")]
    public List<Material> Materials;

    [Header("Detetion")]
    public int SeeAngle;
    public int SeeDistance;
    [Range(0, 1f)]
    public float SearchChance;

    [Header("Attack")]
    public int AttackCount;
    public List<int> Damages;
    public int AttackDistance;
    public int MinDistance;
    public bool CanThreath;
    public bool TwoLayer;
    public int knowbacky;
    public int knowbackxz;

    [Header("Speed")]
    public float WalkSpeed;
    public float SearchSpeed;
    public float RunSpeed;


}
