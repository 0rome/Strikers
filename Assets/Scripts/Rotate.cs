using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float rotationSpeed = 100f; // �������� �������� � �������� � �������

    private void FixedUpdate()
    {
        // ������� ������ �� ��� Z
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
