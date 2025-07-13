using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticWeapon : MonoBehaviour
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
        // Инициализация переменных
        currentAmmo = maxAmmo;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Проверка на перезарядку
        if (isReloading) return;
        if (gameObject.GetComponent<AutomaticWeapon>().enabled == false) { animator.SetBool("Shoot", false); }

        // Если патроны закончились, начинаем перезарядку
        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        // Управление стрельбой для первого игрока
        if (transform.parent.name == "Player_1")
        {
            if (Input.GetKey(KeyCode.Space) && Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + fireRate;
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                animator.SetBool("Shoot", false);
            }
        }
        // Управление стрельбой для второго игрока
        else
        {
            if (Input.GetKey(KeyCode.RightControl) && Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + fireRate;
            }
            else if (Input.GetKeyUp(KeyCode.RightControl))
            {
                animator.SetBool("Shoot", false);
            }
        }
    }

    private void Shoot()
    {
        // Если патронов нет, не стреляем
        if (currentAmmo <= 0) return;

        // Стреляем пулей
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        // Придаем пуле скорость в направлении firePoint.right
        if (rb != null)
        {
            rb.velocity = firePoint.right * bulletSpeed;
        }

        // Анимация стрельбы
        animator.SetBool("Shoot", true);

        // Проигрываем звук стрельбы
        PlaySound(0); // Индекс 0 для звука стрельбы (укажите в инспекторе)

        // Уменьшаем количество патронов
        currentAmmo--;

        transform.parent.GetComponent<PlayerWeaponPickup>().CheckAmmo();
    }

    private IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Перезарядка...");

        // Анимация перезарядки (если есть)
        animator.SetTrigger("Reload");
        animator.SetBool("Shoot", false);

        // Проигрываем звук перезарядки
        PlaySound(1); // Индекс 1 для звука перезарядки (укажите в инспекторе)

        yield return new WaitForSeconds(reloadTime);

        // Восстанавливаем количество патронов
        currentAmmo = maxAmmo;
        isReloading = false;
        Debug.Log("Перезарядка завершена");

        transform.parent.GetComponent<PlayerWeaponPickup>().CheckAmmo();
    }

    private void PlaySound(int index)
    {
        // Проверяем, есть ли звук в массиве
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