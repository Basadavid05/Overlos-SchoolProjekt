using UnityEngine;

public class Sworld_Script : MonoBehaviour
{
    public Transform_Save position;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = SkinChange.Wrist.position;
        Quaternion invertedRotation = Quaternion.Inverse(SkinChange.Wrist.localRotation);
        gameObject.transform.localRotation = invertedRotation;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            MoveControl.ChangeAction("attack1");
        }
        else if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            MoveControl.ChangeAction("attack2");
        }
    }
}
