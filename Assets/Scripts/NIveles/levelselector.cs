using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class levelselector : MonoBehaviour
    
{
    public int level;
    public TextMeshProUGUI levelText;
    // Start is called before the first frame update
    void Start()
    {
        levelText.text = level.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void openScene()
    {
        SceneManager.LoadScene("Level " + level.ToString());
    }
}
