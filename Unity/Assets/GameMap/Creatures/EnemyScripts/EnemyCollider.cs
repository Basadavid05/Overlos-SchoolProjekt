using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    [HideInInspector] public FinalOneLayerEnemyScript finalEnemyScript;
    [HideInInspector] public FinalTwoLayerEnemy finaltwo;
    private Collider coll;

    private void Start()
    {
        coll = GetComponent<Collider>();
    }

    public void Damage(int number)
    {
        if (finalEnemyScript != null)
        {
            finalEnemyScript.CurrentHeal = finalEnemyScript.CurrentHeal - number;
        }
        else
        {
            finaltwo.CurrentHeal = finaltwo.CurrentHeal - number;
        }
    }


}
