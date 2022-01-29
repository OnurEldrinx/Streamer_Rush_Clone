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

    public int viewerScore;
    public int goldScore;

    public Text viewerText;
    public Text goldScoreText;

    public GameObject minigameCam;

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

        viewerScore = Random.Range(100,400);
        goldScore = 0;
        
        
    }

    // Update is called once per frame
    void Update()
    {

        viewerText.text = "" + viewerScore;
        goldScoreText.text = "" + goldScore;
        
    }
}


