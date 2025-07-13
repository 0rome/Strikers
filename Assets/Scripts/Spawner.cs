using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject objectToSpawn; // Префаб объекта, который будет спавниться
    public float spawnInterval = 10f; // Интервал между спавнами

    [Header("Spawn Range")]
    public float minX = -10f; // Минимальное значение X
    public float maxX = 10f;  // Максимальное значение X
    public float minY = -2f;  // Минимальное значение Y
    public float maxY = 2f;   // Максимальное значение Y

    [Header("Optional Parent")]
    public Transform parentTransform; // Родитель для спавнимых объектов (опционально)

    private Coroutine currentCoroutine;

    private void Start()
    {
        currentCoroutine = StartCoroutine(SpawnObjects());
        
    }

    private IEnumerator SpawnObjects()
    {
        while (true)
        {
            Spawn();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void Spawn()
    {
        // Генерируем случайные координаты в указанном диапазоне
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        // Устанавливаем позицию для нового объекта
        Vector3 spawnPosition = new Vector3(randomX, randomY, 0f);

        // Спавним объект
        GameObject spawnedObject = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);

        // Устанавливаем родителя (если указан)
        if (parentTransform != null)
        {
            spawnedObject.transform.SetParent(parentTransform);
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Рисуем область спавна для наглядности
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector3(minX, minY, 0f), new Vector3(minX, maxY, 0f));
        Gizmos.DrawLine(new Vector3(minX, maxY, 0f), new Vector3(maxX, maxY, 0f));
        Gizmos.DrawLine(new Vector3(maxX, maxY, 0f), new Vector3(maxX, minY, 0f));
        Gizmos.DrawLine(new Vector3(maxX, minY, 0f), new Vector3(minX, minY, 0f));
    }
    public void RestartCoroutine()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine); // Останавливаем текущую корутину
        }
        currentCoroutine = StartCoroutine(SpawnObjects());
    }
}
