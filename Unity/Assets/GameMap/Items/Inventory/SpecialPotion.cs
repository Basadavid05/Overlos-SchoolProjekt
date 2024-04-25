using UnityEngine;
using System.Collections;

public class SpecialPotion : MonoBehaviour
{
    public static SpecialPotion sp;
    public static bool Invincible;
    public static int InvcCount;
    public Collider coll;
    private Coroutine currentCoroutine;

    private void Start()
    {
        coll= GetComponent<Collider>();
        sp = this;
    }

    public static void BecomeImortal(int time)
    {
        // Stop the previous coroutine if it's running
        if (sp.currentCoroutine != null)
        {
            sp.StopCoroutine(sp.currentCoroutine);
        }

        // Start the new coroutine
        sp.currentCoroutine = sp.StartCoroutine(sp.AddRandomObject(time * 60));
    }

    IEnumerator AddRandomObject(int time)
    {
        coll.enabled = false;
        Invincible = true;
        InvcCount = time;
        yield return new WaitForSecondsRealtime(time);
        InvcCount = 0;
        Invincible = false;
        coll.enabled = true;coll.isTrigger = true;
    
    }



}
