using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasiccEnemy : MonoBehaviour
{
    [Header("Stats")]
    public int health;
    public PlayerDatas data;

    public void TakeDamage(int damage)
    {
        health-=damage;
        if(health<=0)
        {
            Destroy(gameObject);
            PlayerDatas.PlayerMaxHealth= PlayerDatas.PlayerMaxHealth+5;
        }
    }
}
