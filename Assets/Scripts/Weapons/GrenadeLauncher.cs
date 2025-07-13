using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeLauncher : MonoBehaviour
{
    public float lifeTime = 2f; // Время жизни пули
    public int Damage;
    public GameObject DeathEffect;

    private void Start()
    {
        Invoke("Explode",lifeTime);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerHealth>())
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(Damage);
            Explode();
        }
        
    }
    private void Explode()
    {
        Instantiate(DeathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
