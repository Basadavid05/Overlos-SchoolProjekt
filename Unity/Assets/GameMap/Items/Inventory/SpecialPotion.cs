using UnityEngine;
using System.Collections;

public class SpecialPotion : MonoBehaviour
{
    public static SpecialPotion sp;
    private Collider coll;

    private void Awake()
    {
        // Assign this instance to the static instance variable
        if (sp == null)
        {
            sp = this;
        }
        else
        {
            // If an instance already exists, destroy this one
            Destroy(gameObject);
        }

        // Ensure this object persists across scenes
    }
    private void Start()
    {
        coll= GetComponent<Collider>();
    }

    public static void BecomeImortal(int time)
    {
        sp.StartCoroutine(sp.AddRandomObject(time));
    }

    IEnumerator AddRandomObject(int time)
    {
        while (true)
        {
            coll.isTrigger = false;
            // Wait for a random time between time and time
            float waitTime = Random.Range(time, time);
            yield return new WaitForSeconds(waitTime);
            coll.isTrigger = true;
        }
    }



}
