using UnityEngine;

public class GrapplingTest : MonoBehaviour
{

    [SerializeField]
    SpringJoint joint;

    [SerializeField]
    GameObject target;

    [SerializeField]
    Vector3 grapplePoint;

    [SerializeField]
    private LineRenderer lr;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            grapplePoint = target.transform.position;


            //joint.autoConfigureConnectedAnchor = false;
            //joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(this.transform.position, grapplePoint);

            //The distance grapple will try to keep from grapple point. 
            //joint.maxDistance = distanceFromPoint * 0.8f;
            //joint.minDistance = distanceFromPoint * 0.25f;

            //Adjust these values to fit your game.
            //joint.spring = 1f;
            //joint.damper = 7f;
            //joint.massScale = 1f;

        lr.positionCount = 2;

        lr.SetPosition(0, this.gameObject.transform.position);
        lr.SetPosition(1, target.transform.position);
    }
}
