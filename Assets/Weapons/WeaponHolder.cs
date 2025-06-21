using UnityEngine;

public class WeaponHolder : MonoBehaviour
{

    [SerializeField]
    GameObject weaponHolder;

    [SerializeField]
    GameObject cameraObj;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RotateGun();
    }


    private void RotateGun()
    {
        weaponHolder.transform.rotation = Quaternion.Euler(cameraObj.transform.eulerAngles.x, weaponHolder.transform.eulerAngles.y, weaponHolder.transform.eulerAngles.z);
    }
}
