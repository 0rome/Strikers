using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Keys")]
    public KeyCode Left;
    public KeyCode Right;
    public KeyCode Jump;

    [Header("Movement Settings")]
    public float moveSpeed = 10f; // Скорость передвижения персонажа
    public float jumpForce = 12f; // Сила прыжка
    public float groundFriction = 5f; // Сила трения на земле

    [Header("Ground Check Settings")]
    public Transform groundCheck; // Точка проверки на землю
    public float groundCheckRadius = 0.2f; // Радиус проверки земли
    public LayerMask groundLayer; // Слой земли

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isFacingRight = true; // Для разворота персонажа в сторону движения

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Проверяем, находится ли персонаж на земле
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Прыжок
        if (isGrounded && Input.GetKeyDown(Jump))
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f); // Обнуляем вертикальную скорость перед прыжком
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {
        // Движение влево и вправо с отдельными кнопками
        float moveInput = 0f;

        if (Input.GetKey(Left))
        {
            moveInput = -1f; // Движение влево
        }
        else if (Input.GetKey(Right))
        {
            moveInput = 1f; // Движение вправо
        }

        // Добавляем силу для движения
        rb.AddForce(new Vector2(moveInput * moveSpeed, 0f));

        // Ограничиваем максимальную скорость, чтобы персонаж не разгонялся бесконечно
        if (Mathf.Abs(rb.velocity.x) > moveSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * moveSpeed, rb.velocity.y);
        }

        // Разворот персонажа в сторону движения
        if (moveInput != 0)
        {
            FlipCharacter(moveInput);
        }

        // Обнуляем горизонтальную скорость, если нет ввода, чтобы убрать скольжение
        if (isGrounded && moveInput == 0)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    private void FlipCharacter(float direction)
    {
        // Поворачиваем персонажа в сторону движения, используя поворот по оси Y
        if (direction > 0 && !isFacingRight)
        {
            isFacingRight = true;
            transform.rotation = Quaternion.Euler(0, 0, 0); // Повернуть вправо
        }
        else if (direction < 0 && isFacingRight)
        {
            isFacingRight = false;
            transform.rotation = Quaternion.Euler(0, 180, 0); // Повернуть влево
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Визуализация радиуса проверки земли
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
