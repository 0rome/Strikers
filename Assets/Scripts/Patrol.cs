using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public Transform pointA; // ������ ����� ��������������
    public Transform pointB; // ������ ����� ��������������
    public float speed = 2f; // �������� �����������

    private Transform currentTarget; // ������� ���� (�����)

    private void Start()
    {
        // ������������� ��������� ����
        currentTarget = pointA;
    }

    private void Update()
    {
        // ���������� ������ � ������� ����
        transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, speed * Time.deltaTime);

        // ���� ������ ������ ����, ������ ���� �� ���������������
        if (Vector2.Distance(transform.position, currentTarget.position) < 0.1f)
        {
            currentTarget = currentTarget == pointA ? pointB : pointA;
        }
    }
}
