using UnityEngine;

public class Grapple : MonoBehaviour
{
    [SerializeField]
    float jointSpring;

    [SerializeField] 
    float jointDamping;

    [SerializeField]
    GameObject player;

    [SerializeField]
    GameObject cameraObj;

    [SerializeField]
    GameObject grapplePoint;

    [SerializeField]
    public LayerMask whatIsGrappleable;

    [SerializeField]
    Vector3 grappledPoint;

    [SerializeField]
    private LineRenderer lr;

    [SerializeField]
    Movement movement;

    private SpringJoint joint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartGrappling();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopGrapple();
        }


        SpringJoint[] destroyJoints = GetComponents<SpringJoint>();
        if (!Input.GetMouseButton(0) && destroyJoints.Length > 0)
        {
            
            for (int i = 0; i < destroyJoints.Length; i++)
            {
                Destroy(destroyJoints[i]);
            }
        }
    }


    private void StartGrappling()
    {
        Debug.Log("click");

        RaycastHit hit;
        if (Physics.Raycast(cameraObj.transform.position, cameraObj.transform.forward, out hit, 999999f, whatIsGrappleable))
        {
            Debug.Log("hit " + hit.transform.gameObject);

            movement.isGrappled = true;
            grappledPoint = hit.point;
            joint = player.AddComponent<SpringJoint>();

            joint.connectedBody = hit.rigidbody;
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = hit.transform.InverseTransformPoint(hit.point);
            joint.spring = jointSpring;
            joint.damper = jointDamping;
            joint.enableCollision = true;

            lr.positionCount = 2;


        }
    }

    private void StopGrapple()
    {
        lr.positionCount = 0;
        Destroy(joint);
        movement.isGrappled = false;
    }

    private void LateUpdate()
    {
        DrawRope();
    }

    void DrawRope()
    {
        //If not grappling, don't draw rope
        if (!joint) return;


        lr.SetPosition(0, grapplePoint.transform.position);
        lr.SetPosition(1, grappledPoint);
    }
}
