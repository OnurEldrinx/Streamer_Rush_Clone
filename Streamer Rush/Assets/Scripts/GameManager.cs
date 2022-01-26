using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private string levelName;

    public Text levelNameText;

    public static GameManager Instance;

    private void Awake()
    {
        
        if(Instance == null)
        {

            Instance = this;
        
        }


        levelName = "Day " + SceneManager.GetActiveScene().name;
        levelNameText.text = levelName;

    }


    

    // Start is called before the first frame update
    void Start()
    {

        
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
}


