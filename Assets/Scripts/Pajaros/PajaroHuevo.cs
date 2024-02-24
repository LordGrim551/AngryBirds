using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PajaroHuevo : MonoBehaviour
{
    public Transform pivote;
    public float springRange;
    public float maxVel;
    public GameObject huevoPrefab;
    public KeyCode jumpKey;

    Rigidbody2D rb;
    Vector3 distancia;

    bool canDrag = true;
    bool lanzado = false;

    public Vector3 offset = new Vector3(0f, -0.5f, 0f);

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

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

        lanzado = true;
    }


    void Update()
    {

#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN

        // PC
        if (Input.GetKeyDown(jumpKey))
        {
            LanzarHuevo();
        }

#elif UNITY_ANDROID
        // Móvil
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                  LanzarHuevo();
            }
        }
#endif
    }


    void LanzarHuevo()
    {
        if (lanzado)
        {
            if (huevoPrefab != null)
            {
                Vector3 huevoPosition = transform.position + offset;
                Instantiate(huevoPrefab, huevoPosition, Quaternion.identity);
            }
            lanzado = false;
        }
    }
}
