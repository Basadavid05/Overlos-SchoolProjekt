using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    private EnemyCollider enemyCollider;
        
    private void Update()
    {
        enemyCollider = GetComponent<EnemyCollider>();
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            enemyCollider.Damage(200);
        }
    }
}
