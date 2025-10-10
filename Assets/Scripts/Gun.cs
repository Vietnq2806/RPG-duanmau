using UnityEngine;
using TMPro; // Dùng nếu có UI hiển thị số đạn

public class Gun : MonoBehaviour
{
    private float rotateOffset = 180f;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float shotDelay = 0.15f;
    [SerializeField] private int maxAmmo = 30;
    [SerializeField] private float reloadTime = 1.5f;

    [Header("UI (Optional)")]
    [SerializeField] private TextMeshProUGUI ammoText; // Kéo text UI vào đây nếu có

    [Header("Sound (Optional)")]
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private AudioClip reloadSound;
    private AudioSource audioSource;

    private float nextShot;
    private int currentAmmo;
    private bool isReloading;

    void Start()
    {
        currentAmmo = maxAmmo;
        audioSource = GetComponent<AudioSource>();
        UpdateAmmoUI();
    }

    void Update()
    {
        if (isReloading) return;

        RotateGun();

        if (Input.GetMouseButton(0))
            Shoot();

        if (Input.GetKeyDown(KeyCode.R))
            StartCoroutine(ReloadRoutine());
    }

    void RotateGun()
    {
        if (Input.mousePosition.x < 0 || Input.mousePosition.x > Screen.width || Input.mousePosition.y < 0 ||
            Input.mousePosition.y > Screen.height)
        {
            return;
        }
        
        Vector3 displacement = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(displacement.y, displacement.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + rotateOffset);
        
        // Lật súng khi xoay sang bên trái
        if (angle <-90 || angle > 90)
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
        if (Time.time < nextShot) return;
        if (currentAmmo <= 0)
        {
            // Hết đạn thì không bắn
            return;
        }

        nextShot = Time.time + shotDelay;
        Instantiate(bulletPrefab, firePoint.position, transform.rotation);
        currentAmmo--;

        if (audioSource && shootSound)
            audioSource.PlayOneShot(shootSound);

        UpdateAmmoUI();
    }

    System.Collections.IEnumerator ReloadRoutine()
    {
        isReloading = true;

        if (audioSource && reloadSound)
            audioSource.PlayOneShot(reloadSound);

        if (ammoText)
            ammoText.text = "Reloading...";

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        isReloading = false;
        UpdateAmmoUI();
    }

    void UpdateAmmoUI()
    {
        if (ammoText)
            ammoText.text = $"{currentAmmo}/{maxAmmo}";
    }
}
