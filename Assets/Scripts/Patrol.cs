using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public Transform pointA; // Первая точка патрулирования
    public Transform pointB; // Вторая точка патрулирования
    public float speed = 2f; // Скорость перемещения

    private Transform currentTarget; // Текущая цель (точка)

    private void Start()
    {
        // Устанавливаем начальную цель
        currentTarget = pointA;
    }

    private void Update()
    {
        // Перемещаем объект к текущей цели
        transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, speed * Time.deltaTime);

        // Если объект достиг цели, меняем цель на противоположную
        if (Vector2.Distance(transform.position, currentTarget.position) < 0.1f)
        {
            currentTarget = currentTarget == pointA ? pointB : pointA;
        }
    }
}
