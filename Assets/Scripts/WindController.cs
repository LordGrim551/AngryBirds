using System;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Sse4_2;

public class WindController : MonoBehaviour
{
    public float windStrength = 1f; // Fuerza del viento ajustable en el editor
    public bool estadoViento = false;    

    void Update()
    {
        ApplyWindEffect();
    }

    void ApplyWindEffect()
    {
        // Aquí aplicamos la lógica del viento a todos los objetos que tengan un Rigidbody2D en la escena
      // Encuentra todos los pájaros en la escena que han sido disparados(tienen un Rigidbody2D)
        Pajaro[] pajaros = FindObjectsOfType<Pajaro>();

        foreach (Pajaro pajaro in pajaros)
        {
            if (pajaro.GetComponent<Rigidbody2D>() != null) // Verifica si el pájaro tiene un Rigidbody2D
            {
                // Aplica la fuerza del viento solo a los pájaros que han sido disparados
                Vector2 windForce = Vector2.left * windStrength; // Dirección y fuerza del viento
                pajaro.GetComponent<Rigidbody2D>().AddForce(windForce, ForceMode2D.Force);
            }
        }
       
       
    }
    //SI MUERE EL CERDO MAGO
    public void MagiaOFF()
    {
        estadoViento = false;
    }
}
