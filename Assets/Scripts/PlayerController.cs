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
    public float moveSpeed = 10f; // �������� ������������ ���������
    public float jumpForce = 12f; // ���� ������
    public float groundFriction = 5f; // ���� ������ �� �����

    [Header("Ground Check Settings")]
    public Transform groundCheck; // ����� �������� �� �����
    public float groundCheckRadius = 0.2f; // ������ �������� �����
    public LayerMask groundLayer; // ���� �����

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isFacingRight = true; // ��� ��������� ��������� � ������� ��������

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // ���������, ��������� �� �������� �� �����
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // ������
        if (isGrounded && Input.GetKeyDown(Jump))
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f); // �������� ������������ �������� ����� �������
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {
        // �������� ����� � ������ � ���������� ��������
        float moveInput = 0f;

        if (Input.GetKey(Left))
        {
            moveInput = -1f; // �������� �����
        }
        else if (Input.GetKey(Right))
        {
            moveInput = 1f; // �������� ������
        }

        // ��������� ���� ��� ��������
        rb.AddForce(new Vector2(moveInput * moveSpeed, 0f));

        // ������������ ������������ ��������, ����� �������� �� ���������� ����������
        if (Mathf.Abs(rb.velocity.x) > moveSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * moveSpeed, rb.velocity.y);
        }

        // �������� ��������� � ������� ��������
        if (moveInput != 0)
        {
            FlipCharacter(moveInput);
        }

        // �������� �������������� ��������, ���� ��� �����, ����� ������ ����������
        if (isGrounded && moveInput == 0)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    private void FlipCharacter(float direction)
    {
        // ������������ ��������� � ������� ��������, ��������� ������� �� ��� Y
        if (direction > 0 && !isFacingRight)
        {
            isFacingRight = true;
            transform.rotation = Quaternion.Euler(0, 0, 0); // ��������� ������
        }
        else if (direction < 0 && isFacingRight)
        {
            isFacingRight = false;
            transform.rotation = Quaternion.Euler(0, 180, 0); // ��������� �����
        }
    }

    private void OnDrawGizmosSelected()
    {
        // ������������ ������� �������� �����
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
