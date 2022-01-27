using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{

    public Slider slider;

    public static ProgressBar instance;



    private void Awake()
    {

        if(instance == null)
        {

            instance = this;

        }

        slider = gameObject.GetComponent<Slider>();

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        

        

    }

    public void MakeProgress(int newProgress)
    {

        slider.value += newProgress; 

    }

}
