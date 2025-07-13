using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShotWeapon : MonoBehaviour
{
    [Header("Sounds Settings")]
    public AudioSource[] sounds; // ������ ������ (��������, �������� � �����������)

    [Header("Bullet Settings")]
    public GameObject bulletPrefab; // ������ ����
    public Transform firePoint; // �����, ������ �������� ����
    public float bulletSpeed = 20f; // �������� ����

    [Header("Shooting Settings")]
    public float fireRate = 0.1f; // �������� ����� ����������
    private float nextFireTime = 0f; // ����� ��� ���������� ��������
    private Animator animator;

    [Header("Ammo Settings")]
    public int maxAmmo = 10; // �������� �������� � ��������
    private int currentAmmo;
    public float reloadTime = 2f; // ����� �����������
    private bool isReloading = false;

    private void Start()
    {
        currentAmmo = maxAmmo; // ������������� ��������
        animator = GetComponent<Animator>(); // �������� ��������� ��������
    }

    private void Update()
    {
        if (isReloading) return;

        // �������� �� �����������
        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        // ���������� ��� ������� ������
        if (transform.parent.name == "Player_1" && Input.GetKeyDown(KeyCode.Space) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }

        // ���������� ��� ������� ������
        if (transform.parent.name != "Player_1" && Input.GetKeyDown(KeyCode.RightControl) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    private void Shoot()
    {
        if (currentAmmo <= 0) return;

        // �������� �����
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.velocity = firePoint.right * bulletSpeed;
        }

        // ������ �������� �������� ����� �������
        animator.SetTrigger("Shoot");

        // ����������� ���� ��������
        PlaySound(0);

        // ��������� ���������� ��������
        currentAmmo--;

        transform.parent.GetComponent<PlayerWeaponPickup>().CheckAmmo();
    }

    private IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("�����������...");

        // ������ �������� �����������
        animator.SetTrigger("Reload");

        // ����������� ���� �����������
        PlaySound(1);

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        isReloading = false;
        Debug.Log("����������� ���������");
        if (transform.parent != null)
        {
            transform.parent.GetComponent<PlayerWeaponPickup>().CheckAmmo();
        }
        
    }

    private void PlaySound(int index)
    {
        if (sounds != null && index < sounds.Length && sounds[index] != null)
        {
            sounds[index].Play();
        }
        else
        {
            Debug.LogWarning($"Sound with index {index} is missing or not assigned.");
        }
    }
    public int GetCurrentAmmo()
    {
        return currentAmmo;
    }
    public int GetMaxAmmo()
    {
        return maxAmmo;
    }
}
