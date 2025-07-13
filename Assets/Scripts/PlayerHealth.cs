using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 100; // ������������ ��������
    private int currentHealth;

    [Header("UI Settings")]
    public Slider healthSlider; // �������� ��� ����������� ��������

    [Header("Damage Settings")]
    public float invulnerabilityTime = 1.5f; // ����� ������������ ����� ��������� �����
    private bool isInvulnerable = false;

    public GameObject DeathEffect;

    private void Start()
    {
        // ������������� �������� ��������
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    // ����� ��� ��������� �����
    public void TakeDamage(int damage)
    {
        // �������� �� ������������
        if (isInvulnerable) return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // ��������� Health Bar
        UpdateHealthUI();

        // ��������� ������ ������������
        if (currentHealth > 0)
        {
            StartCoroutine(InvulnerabilityCoroutine());
        }
        else
        {
            Die();
        }
    }

    // ����� ��� �������������� ��������
    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();
    }

    // ���������� UI (Health Bar)
    private void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }

    // ����� ��� ��������� ������
    private void Die()
    {
        Instantiate(DeathEffect,transform.position,Quaternion.identity);
        Debug.Log($"{gameObject.name} �����!");
        gameObject.SetActive(false);
    }

    // ��������� ��� ������������
    private IEnumerator InvulnerabilityCoroutine()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(invulnerabilityTime);
        isInvulnerable = false;
    }
}
