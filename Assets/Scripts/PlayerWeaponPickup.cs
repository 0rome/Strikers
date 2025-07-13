using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWeaponPickup : MonoBehaviour
{
    public Text BulletsText;
    public AudioSource pickupSound;
    private GameObject currentWeapon; // ������� ������ ������

    private void OnTriggerEnter2D(Collider2D other)
    {
        // ���������, �������� �� ������ �������
        if (other.CompareTag("Automatic") || other.CompareTag("Single"))
        {
            // ���� � ��� ��� ���� ������, ��������� ��� � �����������
            if (currentWeapon != null)
            {
                // ��������� ������ �������� ������
                currentWeapon.GetComponent<Weapon>().CheckBox();

                currentWeapon.GetComponent<Animator>().SetBool("notTaken", true);

                // ����������� ������� ������ �� ������ � ������ ��� � ������ ��������
                currentWeapon.transform.SetParent(null);
            }


            // ��������� ����� ������
            currentWeapon = other.gameObject;

            currentWeapon.GetComponent<Animator>().SetBool("notTaken",false);
            // �������� ������ ������ ������, ���� ��� ���������
            currentWeapon.GetComponent<Weapon>().enabled = true;
            
            // ���������� ����� ������ �� �������
            currentWeapon.transform.SetParent(transform);

            // �������� ������� ������ ������������ ������
            //currentWeapon.transform.localPosition = Vector3.zero;
            currentWeapon.transform.position = gameObject.transform.position;

            currentWeapon.transform.rotation = gameObject.transform.rotation;

            pickupSound.Play();
            if (other.tag == "Automatic")
            {
                BulletsText.text = currentWeapon.GetComponent<AutomaticWeapon>().GetMaxAmmo().ToString();
                currentWeapon.GetComponent<AutomaticWeapon>().enabled = true;
            }
            else
            {
                BulletsText.text = currentWeapon.GetComponent<SingleShotWeapon>().GetMaxAmmo().ToString();
                currentWeapon.GetComponent<SingleShotWeapon>().enabled = true;
            }

        }
    }
    private void Update()
    {
        if (currentWeapon == null)
        {
            BulletsText.text = null;
        }
    }
    public void CheckAmmo()
    {
        if (currentWeapon.tag == "Automatic")
        {
            BulletsText.text = currentWeapon.GetComponent<AutomaticWeapon>().GetCurrentAmmo().ToString();
            
        }
        else
        {
            BulletsText.text = currentWeapon.GetComponent<SingleShotWeapon>().GetCurrentAmmo().ToString();
           
        }
    }
}
