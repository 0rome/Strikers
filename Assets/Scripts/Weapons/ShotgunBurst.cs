using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBurst : MonoBehaviour
{
    [Header("Shotgun Settings")]
    public GameObject pelletPrefab; // Префаб дроби
    public int pelletCount = 5; // Количество дробин
    public float spreadAngle = 30f; // Максимальный угол разброса дроби
    public float pelletSpeed = 15f; // Скорость дроби
    public float lifeTime = 2f; // Время жизни каждой дроби

    private void Start()
    {
        // Создаем дробины при старте
        FirePellets();
        // Уничтожаем "материнский" объект пули после запуска дробин
        Destroy(gameObject);
    }

    private void FirePellets()
    {
        for (int i = 0; i < pelletCount; i++)
        {
            // Создаем дробину
            GameObject pellet = Instantiate(pelletPrefab, transform.position, Quaternion.identity);

            // Вычисляем разброс
            float randomAngle = Random.Range(-spreadAngle / 2f, spreadAngle / 2f);
            Vector3 direction = Quaternion.Euler(0, 0, randomAngle) * transform.right;

            // Применяем скорость
            Rigidbody2D rb = pellet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = direction * pelletSpeed;
            }

            // Уничтожаем дробины через некоторое время
            Destroy(pellet, lifeTime);
        }
    }
}
