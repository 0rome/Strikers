using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBurst : MonoBehaviour
{
    [Header("Shotgun Settings")]
    public GameObject pelletPrefab; // ������ �����
    public int pelletCount = 5; // ���������� ������
    public float spreadAngle = 30f; // ������������ ���� �������� �����
    public float pelletSpeed = 15f; // �������� �����
    public float lifeTime = 2f; // ����� ����� ������ �����

    private void Start()
    {
        // ������� ������� ��� ������
        FirePellets();
        // ���������� "�����������" ������ ���� ����� ������� ������
        Destroy(gameObject);
    }

    private void FirePellets()
    {
        for (int i = 0; i < pelletCount; i++)
        {
            // ������� �������
            GameObject pellet = Instantiate(pelletPrefab, transform.position, Quaternion.identity);

            // ��������� �������
            float randomAngle = Random.Range(-spreadAngle / 2f, spreadAngle / 2f);
            Vector3 direction = Quaternion.Euler(0, 0, randomAngle) * transform.right;

            // ��������� ��������
            Rigidbody2D rb = pellet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = direction * pelletSpeed;
            }

            // ���������� ������� ����� ��������� �����
            Destroy(pellet, lifeTime);
        }
    }
}
