using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameStick : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log(Minigame.Instance.onGreen);

    }

    private void OnTriggerStay(Collider other)
    {
        
        if(other.tag == "Green")
        {

            Minigame.Instance.onGreen = true;

        }
        else
        {

            Minigame.Instance.onGreen = false;


        }

    }

}
