using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<PlayerHealth>())
        {
            collision.collider.GetComponent<PlayerHealth>().TakeDamage(100);
        }
    }
}
