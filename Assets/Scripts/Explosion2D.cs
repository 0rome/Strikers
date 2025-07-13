using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion2D : MonoBehaviour
{
    [Header("Параметры взрыва")]
    public float explosionRadius = 5f;       // Радиус взрыва
    public float explosionForce = 800f;      // Максимальная сила взрыва
    public LayerMask explosionLayer;         // Слой объектов, которые будут затронуты взрывом
    public float upwardModifier = 0.2f;      // Модификатор для добавления силы вверх
    public float velocityDamping = 0.5f;     // Коэффициент демпфирования текущей скорости
    public float destroyTime = 2;

    private void Start()
    {
        Explode();
        
    }

    private void Explode()
    {
        // Найти все объекты в радиусе взрыва
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius, explosionLayer);

        foreach (Collider2D hit in colliders)
        {
            Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                // Рассчитываем направление взрыва
                Vector2 explosionDirection = rb.position - (Vector2)transform.position;

                // Рассчитать расстояние от центра взрыва до объекта
                float distance = explosionDirection.magnitude;

                if (distance > 0)
                {
                    // Учитываем текущую скорость объекта и уменьшаем её влияние
                    Vector2 currentVelocity = rb.velocity * velocityDamping;

                    // Рассчитать силу взрыва с учетом расстояния
                    float forceMagnitude = explosionForce * (1 - (distance / explosionRadius));

                    // Учитываем модификатор силы вверх, чтобы избежать смещения только вверх
                    explosionDirection.y += upwardModifier;

                    // Применить силу взрыва с учетом демпфирования скорости
                    Vector2 finalForce = explosionDirection.normalized * forceMagnitude - currentVelocity;
                    rb.AddForce(finalForce, ForceMode2D.Impulse);
                }
            }
        }

        // Уничтожить объект взрыва (если он временный)
        Destroy(gameObject,destroyTime);
    }

    // Опционально: Отображение радиуса взрыва в редакторе
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
   
}
