using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{

    private CharacterController playerController;
    public GameObject currentPlayerModel;
    public float speed = 5;

    public int characterLevel;
    public string characterType;

    public GameObject[] animeGirlModels; // index 0 is level 0 girl, index 1 is level 1 girl
    public GameObject[] attractiveGirlModels; // same
    public GameObject poorWomanModel;
    private List<string> activeAnimationNames;
    private List<bool> activeAnimationBools;

    public ParticleSystem changeEffect;

    public int viewer;

    public GameObject skate;

    public GameObject pathA;
    public GameObject pathB;
    public GameObject pathC;

    private Vector3 verticalMovementDirection;
    private Vector3 horizontalMovementDirection;

    public GameObject balloon;
    private bool isFlying;

    private int bonus;

    public GameObject landingPositionY;

    // Start is called before the first frame update
    void Start()
    {

        DOTween.SetTweensCapacity(1250, 500);

        verticalMovementDirection = Vector3.forward * speed / 2;
        horizontalMovementDirection = Vector3.right * speed;

        characterLevel = -1;

        playerController = GetComponent<CharacterController>();

        currentPlayerModel = poorWomanModel;


        activeAnimationBools = new List<bool>();
        activeAnimationNames = new List<string>();

        

}

// Update is called once per frame
void Update()
    {

        if (!GameManager.Instance.levelFinished && GameManager.Instance.isGameStarted)
        {

            


            if (!Minigame.isStarted)
                Move();

            if (ProgressBar.instance.slider.value >= 100 && (characterLevel <= animeGirlModels.Length - 2))
            {

                UpgradePlayerSkin();

            }


            if (isFlying)
            {


                if (GameManager.Instance.viewerScore > 0)
                {

                    GameManager.Instance.viewerScore -= 1;

                }

                if (GameManager.Instance.viewerScore == 0)
                {

                    transform.DOMoveY(landingPositionY.transform.position.y + 0.2f, 5).OnComplete(() => finish());

                }

            }
        }

    }

    public void finish()
    {

        //isFlying = false;
        speed = 0;
        currentPlayerModel.GetComponent<Animator>().Play("Dance");
        balloon.gameObject.SetActive(false);
        Minigame.Instance.normalProgressBar.SetActive(false);
        GameManager.Instance.restartButton.SetActive(true);
        GameManager.Instance.levelFinished = true;

    }

    private void AutoForwardMovement()
    {

        if (!isFlying)
        {
            playerController.SimpleMove(verticalMovementDirection);
        }
        else if(isFlying == true)
        {

            playerController.Move(verticalMovementDirection * Time.deltaTime);

        }
    }

    void Move()
    {

        AutoForwardMovement();

        float h = Input.GetAxis("Horizontal");

        if (!isFlying)
        {
            playerController.SimpleMove(h * horizontalMovementDirection);
        }
        else if (isFlying == true)
        {

            playerController.Move(h * horizontalMovementDirection * Time.deltaTime);

        }


    }

    private void OnTriggerEnter(Collider other)
    {

        for (int i = 0; i < currentPlayerModel.GetComponent<Animator>().parameterCount; i++)
        {

            if (currentPlayerModel.GetComponent<Animator>().GetParameter(i).type == AnimatorControllerParameterType.Bool)
            {

                activeAnimationNames.Add(currentPlayerModel.GetComponent<Animator>().GetParameter(i).name);
                activeAnimationBools.Add(currentPlayerModel.GetComponent<Animator>().GetBool(currentPlayerModel.GetComponent<Animator>().GetParameter(i).name));

            }

        }

        if(other.tag == "Attractive" || other.tag == "Anime")
        {

            changeEffect.gameObject.SetActive(true);

        }

        if (other.tag == "Attractive")
        {

            currentPlayerModel.SetActive(false);
            currentPlayerModel = attractiveGirlModels[0];
            currentPlayerModel.SetActive(true);
            characterLevel = 0;
            characterType = "Attractive";

        }else if(other.tag == "Anime")
        {

            currentPlayerModel.SetActive(false);
            currentPlayerModel = animeGirlModels[0];
            currentPlayerModel.SetActive(true);
            characterLevel = 0;
            characterType = "Anime";
            


        }

        if(other.tag == "Good" || other.tag == "Bad")
        {

            other.transform.parent.gameObject.SetActive(false);

            ProgressBar.instance.MakeProgress(other.transform.parent.gameObject.GetComponent<Collectable>().point);
            GameManager.Instance.viewerScore += other.transform.parent.gameObject.GetComponent<Collectable>().point;


        }



        //Skate
        if(other.tag == "SkateStart")
        {

            skate.SetActive(true);
            currentPlayerModel.GetComponent<Animator>().SetBool("isOnSkate", true);

        }

        //Minigame
        if(other.tag == "MinigameStart")
        {

            Minigame.isStarted = true;
            Minigame.Instance.minigameWheel.gameObject.SetActive(true);
            GameManager.Instance.minigameCam.SetActive(true);

            speed = 0;
            Minigame.Instance.normalProgressBar.SetActive(false);

            transform.DOMove(Minigame.Instance.playerPos.transform.position, 2f);

            StartCoroutine(Wait(1.5f));

            Minigame.Instance.enemy.SetActive(true);

            



        }


        //Return
        if(other.tag == "ReturnStart")
        {

            verticalMovementDirection = Vector3.left * speed / 2;
            horizontalMovementDirection = Vector3.forward * speed;


            Vector3 a = new Vector3(pathA.transform.position.x,transform.position.y, pathA.transform.position.z);
            Vector3 b = new Vector3(pathB.transform.position.x, transform.position.y, pathB.transform.position.z);
            Vector3 c = new Vector3(pathC.transform.position.x, transform.position.y, pathC.transform.position.z);


            Vector3[] p = { a, c, b};
            transform.DOPath(p, 2).OnComplete(()=>speed = 5);
            transform.DORotate(new Vector3(0, -90, 0),2);
        }


       
        // Door
        if(other.tag == "Door")
        {

            GameManager.Instance.onDoor = true;


        }

        //Coin
        if(other.tag == "Coin")
        {

            other.gameObject.SetActive(false);
            GameManager.Instance.goldScore += 10; 

        }


        

        // Bonus
        if (int.TryParse(other.tag, out bonus))
        {

            Debug.Log(bonus);
            GameManager.Instance.goldScore *= bonus;

        }



    }

    IEnumerator Wait(float t)
    {

        yield return new WaitForSeconds(t);
        currentPlayerModel.GetComponent<Animator>().SetBool("Minigame", true);

    }


    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Attractive" || other.tag == "Anime")
        {

            for(int i = 0; i < activeAnimationNames.Count; i++)
            {

                currentPlayerModel.GetComponent<Animator>().SetBool(activeAnimationNames[i], activeAnimationBools[i]);

            }


        }
        
        if(other.tag == "SkateEnd")
        {

            skate.SetActive(false);
            currentPlayerModel.GetComponent<Animator>().SetBool("isOnSkate", false);

        }

        if(other.tag == "Fly")
        {

            balloon.gameObject.SetActive(true);
            speed = 2f;
            isFlying = true;
            currentPlayerModel.GetComponent<Animator>().Play("Idle");

        }


    }

    public void UpgradePlayerSkin()
    {


        for (int i = 0; i < currentPlayerModel.GetComponent<Animator>().parameterCount; i++)
        {

            if (currentPlayerModel.GetComponent<Animator>().GetParameter(i).type == AnimatorControllerParameterType.Bool)
            {

                activeAnimationNames.Add(currentPlayerModel.GetComponent<Animator>().GetParameter(i).name);
                activeAnimationBools.Add(currentPlayerModel.GetComponent<Animator>().GetBool(currentPlayerModel.GetComponent<Animator>().GetParameter(i).name));

            }

        }

        currentPlayerModel.SetActive(false);


        if (characterType == "Attractive")
        {

            

            if (attractiveGirlModels[characterLevel + 1] != null)
            {
                currentPlayerModel = attractiveGirlModels[characterLevel + 1];
            }
                   
        }else if(characterType == "Anime")
        {

            if (animeGirlModels[characterLevel + 1] != null)
            {
                currentPlayerModel = animeGirlModels[characterLevel + 1];
            }

        }

        currentPlayerModel.SetActive(true);
        characterLevel++;
        ProgressBar.instance.slider.value = 0;


        for (int i = 0; i < activeAnimationNames.Count; i++)
        {

            currentPlayerModel.GetComponent<Animator>().SetBool(activeAnimationNames[i], activeAnimationBools[i]);

        }

        currentPlayerModel.GetComponent<Animator>().SetBool("isHappy", true);


    }

    

}
