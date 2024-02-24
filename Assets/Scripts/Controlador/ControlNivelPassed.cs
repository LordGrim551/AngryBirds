using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlNivelPassed : MonoBehaviour
{
    public string siguienteEscena;

    public void VerificarCerdos()
    {
        GameObject[] cerdos = GameObject.FindGameObjectsWithTag("cerdo");
        GameObject[] cerdosMago = GameObject.FindGameObjectsWithTag("mago");

        int totalCerdos = cerdos.Length + cerdosMago.Length;

        if (totalCerdos == 0)
        {
            ControlNivel.instance.AumentarNiveles();
            SceneManager.LoadScene(siguienteEscena);
        }
    }

    void Update()
    {
        VerificarCerdos();
    }
}
