using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Huevo : MonoBehaviour
{
    public float explosionRadius = 5f;
    public LayerMask destructibleLayer;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (((1 << col.gameObject.layer) & destructibleLayer) != 0 || col.gameObject.CompareTag("Suelo"))
        {

            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius, destructibleLayer);


            foreach (Collider2D collider in colliders)
            {
                Destroy(collider.gameObject);
            }

            Destroy(gameObject);
        }
    }




}


