using Unity.VisualScripting;
using UnityEngine;

public class Crossbow : MonoBehaviour
{
    [SerializeField]
    GameObject boltProjectile;

    [SerializeField]
    GameObject bolt;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if (Input.GetMouseButtonDown(0))
        {
            bolt.gameObject.SetActive(false);
            Instantiate(boltProjectile, new Vector3 (bolt.transform.position.x, bolt.transform.position.y, bolt.transform.position.z), 
            Quaternion.Euler(bolt.transform.eulerAngles.x, bolt.transform.eulerAngles.y, bolt.transform.eulerAngles.z));
        }

    }
}
