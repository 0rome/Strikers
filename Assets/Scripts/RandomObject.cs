using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObject : MonoBehaviour
{
    public GameObject[] ObjectPrefabs;
    public GameObject DestroyEffect;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<PlayerHealth>())
        {
            Instantiate(DestroyEffect, transform.position, Quaternion.identity);
            Instantiate(ObjectPrefabs[Random.Range(0, ObjectPrefabs.Length)], transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        
    }
}
