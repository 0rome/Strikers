using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapsPick : MonoBehaviour
{
    public Dropdown dropdown; // Ссылка на Dropdown

    private void Start()
    {
        // Загрузка сохранённого значения
        if (PlayerPrefs.HasKey("Rounds"))
        {
            int savedValue = PlayerPrefs.GetInt("Rounds");
            dropdown.value = savedValue;
        }

        // Добавляем слушатель на изменение значения
        dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
    }

    private void OnDropdownValueChanged(int value)
    {
        // Сохраняем выбранное значение в PlayerPrefs
        PlayerPrefs.SetInt("Rounds", value);
        PlayerPrefs.Save();

        // Действие в зависимости от выбранного значения
       
    }
}
