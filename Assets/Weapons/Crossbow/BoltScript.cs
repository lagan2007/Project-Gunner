using Unity.VisualScripting;
using UnityEngine;

public class BoltScript : MonoBehaviour
{
    [SerializeField]
    Rigidbody rb;

    [SerializeField]
    float speed;

    [SerializeField]
    LayerMask layerMask;

    [SerializeField]
    float explosionRadius;

    [SerializeField]
    float explosionForce;

    [SerializeField]
    GameObject particles;




    Vector3 currentPosition;
    Vector3 lastPosition;

    Rigidbody objectRb;

    bool hasLanded;

    float distance;

    Vector3 cachedScale;
    Vector3 cachedRotation;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb.AddRelativeForce(new Vector3(0, 0, speed));
        lastPosition = transform.position;
        cachedScale = transform.localScale;
        cachedRotation = transform.eulerAngles;

    }

    // Update is called once per frame
    void Update()
    {   
        

        
    }


    private void FixedUpdate()
    {

        currentPosition = transform.position;

        distance = Vector3.Distance(currentPosition, lastPosition);
        Vector3 direction = (currentPosition - lastPosition).normalized;

        if (!hasLanded)
        {
            RaycastHit hit;
            if (Physics.Raycast(lastPosition, this.transform.forward, out hit, distance, layerMask))
            {
                
                rb.linearVelocity = Vector3.zero;
                this.transform.position = hit.point;
                Debug.Log("Hit object: " + hit.collider.name);
                rb.AddExplosionForce(1000, this.transform.position,10);
                Explode();
                hasLanded = true;
            }
        }
            
        
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        Movement movement;

        Instantiate(particles, this.transform.transform.position, Quaternion.Euler(this.transform.eulerAngles.x, this.transform.eulerAngles.y, this.transform.eulerAngles.z));
            

        foreach (Collider collider in colliders)
        {
            Rigidbody rigidbody = collider.GetComponent<Rigidbody>();

            if (collider.gameObject.tag == ("Player"))
            {
                movement = collider.gameObject.GetComponent<Movement>();
                movement.exploded = true;
                rigidbody.AddExplosionForce(explosionForce, this.transform.position, explosionRadius);
            }

            if (rigidbody != null && collider.gameObject.tag != ("Player"))
            {
                rigidbody.AddExplosionForce(explosionForce, this.transform.position, explosionRadius);
            }
        }

        Destroy(this.gameObject);
    }

}
