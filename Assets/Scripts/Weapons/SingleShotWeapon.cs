using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShotWeapon : MonoBehaviour
{
    [Header("Sounds Settings")]
    public AudioSource[] sounds; // Массив звуков (например, стрельба и перезарядка)

    [Header("Bullet Settings")]
    public GameObject bulletPrefab; // Префаб пули
    public Transform firePoint; // Точка, откуда вылетают пули
    public float bulletSpeed = 20f; // Скорость пули

    [Header("Shooting Settings")]
    public float fireRate = 0.1f; // Задержка между выстрелами
    private float nextFireTime = 0f; // Время для следующего выстрела
    private Animator animator;

    [Header("Ammo Settings")]
    public int maxAmmo = 10; // Максимум патронов в магазине
    private int currentAmmo;
    public float reloadTime = 2f; // Время перезарядки
    private bool isReloading = false;

    private void Start()
    {
        currentAmmo = maxAmmo; // Инициализация патронов
        animator = GetComponent<Animator>(); // Получаем компонент анимации
    }

    private void Update()
    {
        if (isReloading) return;

        // Проверка на перезарядку
        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        // Управление для первого игрока
        if (transform.parent.name == "Player_1" && Input.GetKeyDown(KeyCode.Space) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }

        // Управление для второго игрока
        if (transform.parent.name != "Player_1" && Input.GetKeyDown(KeyCode.RightControl) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    private void Shoot()
    {
        if (currentAmmo <= 0) return;

        // Стреляем пулей
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.velocity = firePoint.right * bulletSpeed;
        }

        // Запуск анимации выстрела через триггер
        animator.SetTrigger("Shoot");

        // Проигрываем звук стрельбы
        PlaySound(0);

        // Уменьшаем количество патронов
        currentAmmo--;

        transform.parent.GetComponent<PlayerWeaponPickup>().CheckAmmo();
    }

    private IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Перезарядка...");

        // Запуск анимации перезарядки
        animator.SetTrigger("Reload");

        // Проигрываем звук перезарядки
        PlaySound(1);

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        isReloading = false;
        Debug.Log("Перезарядка завершена");
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
