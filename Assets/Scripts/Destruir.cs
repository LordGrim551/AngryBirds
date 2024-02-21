using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destruir : MonoBehaviour
{
    public float vida;
    public GameObject explosion;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.relativeVelocity.magnitude > vida)
        {

            if (explosion != null)
            {
                var go = Instantiate(explosion, transform.position, Quaternion.identity);
                Destroy(go, 3);
            }
            Destroy(gameObject, 0.1f);
        }
        else
        {
            vida -= col.relativeVelocity.magnitude;
        }
    }
}
