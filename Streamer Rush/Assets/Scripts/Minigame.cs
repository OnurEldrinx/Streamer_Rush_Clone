using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Minigame : MonoBehaviour
{

    public static Minigame Instance;

    private void Awake()
    {
        
        if(Instance == null)
        {

            Instance = this;

        }

    }

    public static bool isStarted;

    public Transform playerPos;
    public GameObject enemyPos;

    public GameObject player;
    public GameObject enemy;

    public GameObject normalProgressBar;

    public bool winner;

    public GameObject minigameWheel;
    public bool onGreen;

    // Start is called before the first frame update
    void Start()
    {

        DOTween.SetTweensCapacity(500, 50);

        isStarted = false;
    }

    // Update is called once per frame
    void Update()
    {


        if (isStarted)
        {


            if (onGreen && Input.GetMouseButtonDown(0))
            {

                Minigame.Instance.winner = true;
                player.GetComponent<PlayerController>().currentPlayerModel.GetComponent<Animator>().SetBool("punch", true);
                GameManager.Instance.viewerScore += 500;
                StartCoroutine(AfterPunch());
                


            }
            else if (onGreen == false && Input.GetMouseButtonDown(0))
            {

                Minigame.Instance.winner = false;
                Minigame.Instance.enemy.GetComponent<Animator>().SetBool("punch", true);
                GameManager.Instance.viewerScore -= 500;
                StartCoroutine(AfterPunch());



            }

        }
        

    }

    IEnumerator AfterPunch()
    {

        yield return new WaitForSeconds(3);
        player.GetComponent<PlayerController>().speed = 5;
        player.GetComponent<PlayerController>().currentPlayerModel.GetComponent<Animator>().SetBool("punch", false);
        player.GetComponent<PlayerController>().currentPlayerModel.GetComponent<Animator>().SetBool("Minigame", false);

        if (winner)
        {

            player.GetComponent<PlayerController>().currentPlayerModel.GetComponent<Animator>().Play("HappyWalk");
            player.GetComponent<PlayerController>().currentPlayerModel.GetComponent<Animator>().SetBool("isHappy", true);


        }
        else
        {

            player.GetComponent<PlayerController>().currentPlayerModel.GetComponent<Animator>().Play("SadWalk");
            player.GetComponent<PlayerController>().currentPlayerModel.GetComponent<Animator>().SetBool("isHappy",false);


        }


        enemy.SetActive(false);

        Minigame.isStarted = false;

        GameManager.Instance.minigameCam.SetActive(false);

    }






}
