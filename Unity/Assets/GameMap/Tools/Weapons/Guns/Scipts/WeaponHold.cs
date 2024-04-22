using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHold : MonoBehaviour
{
    public bool IsHold = false;
    [SerializeField] Transform WeaponPlace;
    [SerializeField] Transform WeaponRotation;

    private float smoothness = 5.0f; // Adjust this value based on your preference

    private void FixedUpdate()
    {
        if(!PauseMenu.GameIsPause)
        {
            if (transform.childCount == 0)
            {
                IsHold = false;
            }
            else if (transform.childCount > 0)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    Transform child = transform.GetChild(i);

                    if (child.gameObject.activeSelf)
                    {
                        IsHold = true;
                        break;
                    }
                }
            }
        }
        else
        {
            IsHold=false;
        }

        if (IsHold)
        {
            transform.position = Vector3.Lerp(transform.position, WeaponPlace.position, smoothness * Time.deltaTime);
            transform.rotation = WeaponRotation.rotation;
        }

    }

}
