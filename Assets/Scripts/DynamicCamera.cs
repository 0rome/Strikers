using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicCamera : MonoBehaviour
{
    [Header("Player Settings")]
    public Transform player1; // ������ �� ������� ������
    public Transform player2; // ������ �� ������� ������

    [Header("Camera Settings")]
    public float smoothSpeed = 0.125f; // �������� ����������� �������� ������
    public Vector3 offset; // �������� ������
    public float minZoom = 5f; // ����������� ������ ������
    public float maxZoom = 10f; // ������������ ������ ������
    public float zoomLimiter = 10f; // ������������ ��� ���������� ����������

    private Camera cam;

    private void Start()
    {
        // �������� ��������� ������
        cam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        if (player1 == null || player2 == null)
        {
            Debug.LogWarning("Players are not assigned to the camera script!");
            return;
        }

        // ��������� ������� ����� ����� ��������
        Vector3 centerPoint = GetCenterPoint();

        // ������� ������ � ������� �����
        Vector3 newPosition = centerPoint + offset;
        transform.position = Vector3.Lerp(transform.position, newPosition, smoothSpeed);

        // ��������� ������ ������ � ����������� �� ���������� ����� ��������
        float distance = GetPlayersDistance();
        float newZoom = Mathf.Lerp(maxZoom, minZoom, distance / zoomLimiter);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, newZoom, smoothSpeed);
    }

    // ��������� ������� ����� ����� ����� ��������
    private Vector3 GetCenterPoint()
    {
        return (player1.position + player2.position) / 2;
    }

    // ��������� ���������� ����� ��������
    private float GetPlayersDistance()
    {
        return Vector3.Distance(player1.position, player2.position);
    }
}
