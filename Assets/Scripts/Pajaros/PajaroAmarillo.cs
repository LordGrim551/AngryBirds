using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PajaroAmarillo : MonoBehaviour
{
    public Transform pivote;
    public float springRange;
    public float maxVel;
    public KeyCode jumpKey;

    Rigidbody2D rb;
    Vector3 distancia;

    // Variable para rastrear si el p�jaro ha sido lanzado
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

    // M�todo para acelerar el p�jaro cuando se toca la pantalla despu�s de ser lanzado
    void Accelerate()
    {
        if (lanzado)
        {
            rb.velocity *= 2f; // Duplicar la velocidad
            Debug.Log("Velocidad" + rb.velocity);
            lanzado = false; // Impedir que se acelere nuevamente
        }
    }

    void Update()
    {


#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN

        // PC
        if (Input.GetKeyDown(jumpKey))
        {
          Accelerate();
        }

#elif UNITY_ANDROID
        // M�vil
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                Accelerate();
            }
        }
#endif
    }
}
