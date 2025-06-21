using UnityEngine;

public class Grounded : MonoBehaviour
{
    [SerializeField]
    Movement movement;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == (8))
        {
            movement.grounded = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            movement.grounded = true;
            Debug.Log("hit");
        }



    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            movement.grounded = false;
        }
    }
}
