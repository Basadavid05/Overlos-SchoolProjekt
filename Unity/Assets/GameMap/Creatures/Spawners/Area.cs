using UnityEngine;

public class Area : MonoBehaviour
{
    private Collider Coll;
    private int layerMask;
    private int count;
    private bool PlayerIsThere;

    private void Start()
    {
        Coll = GetComponent<Collider>();
        count = transform.childCount;
        layerMask = LayerMask.NameToLayer("Detection");
        Invoke("DelayedStart", 0.1f);
    }

    private void DelayedStart()
    {
            ChildrenChange(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!PlayerIsThere)
        {
            if (other.gameObject.layer == layerMask)
            {
                ChildrenChange(true);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!PlayerIsThere)
        {
            if (other.gameObject.layer == layerMask)
            {
                ChildrenChange(true);
            }
        }
    }

    private void Update()
    {
        if (PlayerIsThere && SpawnReferences.Respawnenemy)
        {
            ChildrenChange(true);
        }
    }

    public void ChildrenChange(bool status)
    {
        PlayerIsThere = status;
        if (count==1)
        {
            transform.GetChild(0).gameObject.SetActive(status);
        }
        else if(count>1)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(status);
            }
        }
    }
}
