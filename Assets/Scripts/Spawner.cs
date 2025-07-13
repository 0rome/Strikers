using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject objectToSpawn; // ������ �������, ������� ����� ����������
    public float spawnInterval = 10f; // �������� ����� ��������

    [Header("Spawn Range")]
    public float minX = -10f; // ����������� �������� X
    public float maxX = 10f;  // ������������ �������� X
    public float minY = -2f;  // ����������� �������� Y
    public float maxY = 2f;   // ������������ �������� Y

    [Header("Optional Parent")]
    public Transform parentTransform; // �������� ��� ��������� �������� (�����������)

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
        // ���������� ��������� ���������� � ��������� ���������
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        // ������������� ������� ��� ������ �������
        Vector3 spawnPosition = new Vector3(randomX, randomY, 0f);

        // ������� ������
        GameObject spawnedObject = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);

        // ������������� �������� (���� ������)
        if (parentTransform != null)
        {
            spawnedObject.transform.SetParent(parentTransform);
        }
    }

    private void OnDrawGizmosSelected()
    {
        // ������ ������� ������ ��� �����������
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
            StopCoroutine(currentCoroutine); // ������������� ������� ��������
        }
        currentCoroutine = StartCoroutine(SpawnObjects());
    }
}
