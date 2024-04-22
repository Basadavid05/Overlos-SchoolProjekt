using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Attack : MonoBehaviour
{
    bool SeenPlayer;

    // Update is called once per frame
    void Update()
    {
        SeenPlayer = GetComponent<FieldView>().canSeePlayer;
    }

    
}
