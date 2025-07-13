using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicCamera : MonoBehaviour
{
    [Header("Player Settings")]
    public Transform player1; // Ссылка на первого игрока
    public Transform player2; // Ссылка на второго игрока

    [Header("Camera Settings")]
    public float smoothSpeed = 0.125f; // Скорость сглаживания движения камеры
    public Vector3 offset; // Смещение камеры
    public float minZoom = 5f; // Минимальный размер камеры
    public float maxZoom = 10f; // Максимальный размер камеры
    public float zoomLimiter = 10f; // Ограничитель для увеличения расстояния

    private Camera cam;

    private void Start()
    {
        // Получаем компонент камеры
        cam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        if (player1 == null || player2 == null)
        {
            Debug.LogWarning("Players are not assigned to the camera script!");
            return;
        }

        // Вычисляем среднюю точку между игроками
        Vector3 centerPoint = GetCenterPoint();

        // Двигаем камеру к средней точке
        Vector3 newPosition = centerPoint + offset;
        transform.position = Vector3.Lerp(transform.position, newPosition, smoothSpeed);

        // Обновляем размер камеры в зависимости от расстояния между игроками
        float distance = GetPlayersDistance();
        float newZoom = Mathf.Lerp(maxZoom, minZoom, distance / zoomLimiter);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, newZoom, smoothSpeed);
    }

    // Получение средней точки между двумя игроками
    private Vector3 GetCenterPoint()
    {
        return (player1.position + player2.position) / 2;
    }

    // Получение расстояния между игроками
    private float GetPlayersDistance()
    {
        return Vector3.Distance(player1.position, player2.position);
    }
}
