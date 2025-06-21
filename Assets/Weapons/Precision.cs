using UnityEngine;

public class Precision : MonoBehaviour
{

    [SerializeField]
    GameObject camera;
    [SerializeField]
    GameObject weapon;

    [SerializeField]
    LayerMask layerMask;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.gameObject.transform.eulerAngles =  new Vector3 (0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, Mathf.Infinity, layerMask))
        {
            Vector3 direction = hit.point - this.gameObject.transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(direction);

            this.gameObject.transform.eulerAngles = new Vector3(lookRotation.eulerAngles.x, this.gameObject.transform.eulerAngles.y, this.gameObject.transform.eulerAngles.z);
            
        }
    }
}
