using UnityEngine;

public class Gun : MonoBehaviour
{
    private float rotateOffset = 180f;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefabs;
    [SerializeField] private float shotDelay = 0.15f;
    private float nextShot;
    [SerializeField] private int maxAmmo = 30;
    public int currentAmmo;
    void Start()
    {
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        RotateGun();
        Shoot();
        Reload();
    }
    void RotateGun()
    {
        if(Input.mousePosition.x < 0 || Input.mousePosition.x > Screen.width || Input.mousePosition.y < 0  || Input.mousePosition.y > Screen.height)
        {
            return;
        }
        Vector3 dislacement = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(dislacement.y, dislacement.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + rotateOffset));
        if(angle > 90 || angle < -90)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, -1, 1);
        }
    }
    void Shoot()
    {
       if(Input.GetMouseButton(0) && currentAmmo > 0 && Time.time > nextShot)
        {
            nextShot = Time.time + shotDelay;
            Instantiate(bulletPrefabs, firePoint.position, transform.rotation);
            currentAmmo--;
        }
    }
    void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            currentAmmo = maxAmmo;
        }
    }    
}
