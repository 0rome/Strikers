using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion2D : MonoBehaviour
{
    [Header("��������� ������")]
    public float explosionRadius = 5f;       // ������ ������
    public float explosionForce = 800f;      // ������������ ���� ������
    public LayerMask explosionLayer;         // ���� ��������, ������� ����� ��������� �������
    public float upwardModifier = 0.2f;      // ����������� ��� ���������� ���� �����
    public float velocityDamping = 0.5f;     // ����������� ������������� ������� ��������
    public float destroyTime = 2;

    private void Start()
    {
        Explode();
        
    }

    private void Explode()
    {
        // ����� ��� ������� � ������� ������
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius, explosionLayer);

        foreach (Collider2D hit in colliders)
        {
            Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                // ������������ ����������� ������
                Vector2 explosionDirection = rb.position - (Vector2)transform.position;

                // ���������� ���������� �� ������ ������ �� �������
                float distance = explosionDirection.magnitude;

                if (distance > 0)
                {
                    // ��������� ������� �������� ������� � ��������� � �������
                    Vector2 currentVelocity = rb.velocity * velocityDamping;

                    // ���������� ���� ������ � ������ ����������
                    float forceMagnitude = explosionForce * (1 - (distance / explosionRadius));

                    // ��������� ����������� ���� �����, ����� �������� �������� ������ �����
                    explosionDirection.y += upwardModifier;

                    // ��������� ���� ������ � ������ ������������� ��������
                    Vector2 finalForce = explosionDirection.normalized * forceMagnitude - currentVelocity;
                    rb.AddForce(finalForce, ForceMode2D.Impulse);
                }
            }
        }

        // ���������� ������ ������ (���� �� ���������)
        Destroy(gameObject,destroyTime);
    }

    // �����������: ����������� ������� ������ � ���������
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
   
}
