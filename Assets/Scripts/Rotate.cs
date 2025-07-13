using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float rotationSpeed = 100f; // Скорость вращения в градусах в секунду

    private void FixedUpdate()
    {
        // Вращаем объект по оси Z
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
