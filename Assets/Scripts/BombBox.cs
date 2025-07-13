using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BombBox : MonoBehaviour
{
    public GameObject ExplosionPrefab;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<Rigidbody2D>())
        {
            Instantiate(ExplosionPrefab,transform.position,Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
