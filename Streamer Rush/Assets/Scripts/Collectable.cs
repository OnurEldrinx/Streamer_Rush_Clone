using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] collectableTypes;
    public GameObject player;
    public int point;
    
    void Start()
    {

        if(gameObject.tag == "Good")
        {

            point = 5;

        }else if(gameObject.tag == "Bad")
        {

            point = -10;

        }


    }

    // Update is called once per frame
    void Update()
    {

        if(player.GetComponent<PlayerController>().characterType == "Anime")
        {

            collectableTypes[0].SetActive(true);

        }else if(player.GetComponent<PlayerController>().characterType == "Attractive")
        {

            collectableTypes[1].SetActive(true);

        }

    }
}
