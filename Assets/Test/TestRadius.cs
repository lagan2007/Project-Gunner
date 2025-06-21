using UnityEngine;

public class TestRadius : MonoBehaviour
{

    [SerializeField]
    float radius;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, radius);
    }
}
