using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>())
        {
            

            if (gameObject.tag == "Automatic")
            {
                gameObject.GetComponent<AutomaticWeapon>().enabled = true;
            }
            else
            {
                gameObject.GetComponent<SingleShotWeapon>().enabled = true;
            }
            
        }

    }
    
    public void CheckBox()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;

        if (gameObject.tag == "Automatic")
        {
            gameObject.GetComponent<Animator>().SetBool("Shoot", false);
            gameObject.GetComponent<AutomaticWeapon>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<SingleShotWeapon>().enabled = false;
        }
        

        if (gameObject.GetComponent<BoxCollider2D>().enabled == false)
        {
            Invoke("OnBox",1f);
        }
    }
    private void OnBox()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }
}
