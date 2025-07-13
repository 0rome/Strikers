using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 2f; // Время жизни пули
    public int Damage;
    public GameObject DeathEffect;

    private Rigidbody2D rb;

    private void Start()
    {
        // Получаем Rigidbody2D
        rb = GetComponent<Rigidbody2D>();

        // Уничтожаем пулю через заданное время
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        // Если у Rigidbody есть скорость, обновляем поворот
        if (rb != null && rb.velocity != Vector2.zero)
        {
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerHealth>())
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(Damage);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Default") ||
            collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Instantiate(DeathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
