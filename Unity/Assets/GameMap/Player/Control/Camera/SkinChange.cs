using System.Collections.Generic;
using UnityEngine;

public class SkinChange : MonoBehaviour
{
    public List<GameObject> bodyParts = new List<GameObject>();

    public static Transform Wrist;
    public Transform Wrists;

    private void Start()
    {
        Wrist = Wrists;
        ChangeParts(false);
    }


    public void ChangeParts(bool status)
    {
            for (int i = 0; i < bodyParts.Count; i++)
            {
                SkinnedMeshRenderer renderer = bodyParts[i].GetComponent<SkinnedMeshRenderer>();

                // Set the visibility of the body part
                renderer.enabled = status;

                // Set the shadow casting mode to on regardless of visibility status
                renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
            }
            ChangePosition(status);
    }

    private void ChangePosition(bool status)
    {
        if (!status)
        {
            gameObject.transform.position = transform.position+ new Vector3(0f,1f,0f);
        }
        else
        {
            gameObject.transform.position = transform.position - new Vector3(0f, 1f, 0f);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            MoveControl.ChangeAction("attack1");
        }
        else if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            MoveControl.ChangeAction("attack2");
        }
        else if(Input.GetKeyDown(KeyCode.Mouse2))
        {
            MoveControl.ChangeAction("attack3");
        }
    }


}
