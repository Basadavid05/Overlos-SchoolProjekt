using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class w11 : MonoBehaviour
{
    public bool playeristhere = false;

    private void Start()
    {
        playeristhere = true;
        Invoke("EnableObjects", 3f);
    }

    private void EnableObjects()
    {
        playeristhere = false;
    }

    private void switchon(bool spawn)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(spawn);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switchon(true);
            playeristhere = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switchon(false);
            playeristhere = false;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switchon(true);
            playeristhere = true;
        }
    }
}
