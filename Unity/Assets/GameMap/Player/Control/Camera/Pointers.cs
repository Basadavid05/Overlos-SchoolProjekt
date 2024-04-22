using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pointers : MonoBehaviour
{
    [Header("Pointers")]
    [HideInInspector]public Image pointer;
    public bool points;

    private void Start()
    {
        pointer = transform.Find("Center").gameObject.GetComponent<Image>();
    }

    public void ChangeOn()
    {
        pointer.color = Color.cyan;points = true;
    }

    public void ChangeBack()
    {
        if (points) { pointer.color = Color.white; points = false; }        
    }
}
