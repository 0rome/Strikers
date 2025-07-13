using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 100; // Максимальное здоровье
    private int currentHealth;

    [Header("UI Settings")]
    public Slider healthSlider; // Ползунок для отображения здоровья

    [Header("Damage Settings")]
    public float invulnerabilityTime = 1.5f; // Время неуязвимости после получения урона
    private bool isInvulnerable = false;

    public GameObject DeathEffect;

    private void Start()
    {
        // Инициализация текущего здоровья
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    // Метод для получения урона
    public void TakeDamage(int damage)
    {
        // Проверка на неуязвимость
        if (isInvulnerable) return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Обновляем Health Bar
        UpdateHealthUI();

        // Запускаем таймер неуязвимости
        if (currentHealth > 0)
        {
            StartCoroutine(InvulnerabilityCoroutine());
        }
        else
        {
            Die();
        }
    }

    // Метод для восстановления здоровья
    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();
    }

    // Обновление UI (Health Bar)
    private void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }

    // Метод для обработки смерти
    private void Die()
    {
        Instantiate(DeathEffect,transform.position,Quaternion.identity);
        Debug.Log($"{gameObject.name} погиб!");
        gameObject.SetActive(false);
    }

    // Короутина для неуязвимости
    private IEnumerator InvulnerabilityCoroutine()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(invulnerabilityTime);
        isInvulnerable = false;
    }
}
