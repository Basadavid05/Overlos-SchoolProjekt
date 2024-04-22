using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class FloatinginWater : MonoBehaviour
{
    public Rigidbody Targetbody;
    public float DeptBeforceSub;
    public float DisplacementAmount;
    public float WaterDrag;
    public float WaterAngularDrag;
    public int floaters;
    public WaterSurface Water;
    private WaterSearchParameters search;
    private WaterSearchResult SearchResult;

    private void FixedUpdate()
    {
        Targetbody.AddForceAtPosition(force: Physics.gravity / floaters, transform.position, ForceMode.Acceleration);

        search.startPosition = transform.position;
    }
}
