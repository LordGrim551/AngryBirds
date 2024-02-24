using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PajaroBomba : MonoBehaviour
{
    public Transform pivote;
    public float springRange;
    public float maxVel;
    public float explosionRadius = 5f;
    public LayerMask destructibleLayer;
    public KeyCode jumpKey;

    Rigidbody2D rb;
    Vector3 distancia;
    bool lanzado = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    bool canDrag = true;

    void OnMouseDrag()
    {
        if (!canDrag)
        {
            return;
        }

        var posicion = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        distancia = posicion - pivote.position;
        distancia.z = 0;

        if (distancia.magnitude > springRange)
        {
            distancia = distancia.normalized * springRange;
        }

        transform.position = distancia + pivote.position;
    }

    void OnMouseUp()
    {
        if (!canDrag)
        {
            return;
        }

        canDrag = false;

        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.velocity = -distancia.normalized * maxVel * distancia.magnitude / springRange;

        // Marcar como lanzado
        lanzado = true;
    }

    void Update()
    {
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN

        // PC
        if (Input.GetKeyDown(jumpKey))
        {
           Explode();
        }

#elif UNITY_ANDROID
        // Móvil
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                 Explode();
            }
        }
#endif
    }
   

    void Explode()
    {
        if (lanzado)
        {
            // Detectar objetos cercanos para destruir
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius, destructibleLayer);

            // Destruir los objetos detectados
            foreach (Collider2D col in colliders)
            {
                Debug.Log("Explotao");
                Destroy(col.gameObject);
            }



            // Destruir el pájaro bomba
            Destroy(gameObject);
        }
    }
}
