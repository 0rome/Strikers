using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapsPick : MonoBehaviour
{
    public Dropdown dropdown; // ������ �� Dropdown

    private void Start()
    {
        // �������� ����������� ��������
        if (PlayerPrefs.HasKey("Rounds"))
        {
            int savedValue = PlayerPrefs.GetInt("Rounds");
            dropdown.value = savedValue;
        }

        // ��������� ��������� �� ��������� ��������
        dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
    }

    private void OnDropdownValueChanged(int value)
    {
        // ��������� ��������� �������� � PlayerPrefs
        PlayerPrefs.SetInt("Rounds", value);
        PlayerPrefs.Save();

        // �������� � ����������� �� ���������� ��������
       
    }
}
