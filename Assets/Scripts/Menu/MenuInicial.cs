using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject levelController;
    public void Jugar()
    {
        levelController.SetActive(true);
   
    }

    public void Salir()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Debug.Log("Salir...");
        Application.Quit();
    }
    
}
