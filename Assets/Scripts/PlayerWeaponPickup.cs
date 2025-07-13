using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWeaponPickup : MonoBehaviour
{
    public Text BulletsText;
    public AudioSource pickupSound;
    private GameObject currentWeapon; // Текущее оружие игрока

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Проверяем, является ли объект оружием
        if (other.CompareTag("Automatic") || other.CompareTag("Single"))
        {
            // Если у нас уже есть оружие, отключаем его и отсоединяем
            if (currentWeapon != null)
            {
                // Отключаем скрипт текущего оружия
                currentWeapon.GetComponent<Weapon>().CheckBox();

                currentWeapon.GetComponent<Animator>().SetBool("notTaken", true);

                // Отсоединяем текущее оружие от игрока и ставим его в корень иерархии
                currentWeapon.transform.SetParent(null);
            }


            // Подбираем новое оружие
            currentWeapon = other.gameObject;

            currentWeapon.GetComponent<Animator>().SetBool("notTaken",false);
            // Включаем скрипт нового оружия, если оно выключено
            currentWeapon.GetComponent<Weapon>().enabled = true;
            
            // Закрепляем новое оружие за игроком
            currentWeapon.transform.SetParent(transform);

            // Обнуляем позицию оружия относительно игрока
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
