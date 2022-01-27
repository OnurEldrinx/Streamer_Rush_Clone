using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private CharacterController playerController;
    [SerializeField] private GameObject currentPlayerModel;
    private float speed = 5;

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

    // Start is called before the first frame update
    void Start()
    {

        characterLevel = -1;

        playerController = GetComponent<CharacterController>();

        currentPlayerModel = poorWomanModel;

        currentPlayerModel.GetComponent<Animator>().SetBool("isGameStarted",true);

        activeAnimationBools = new List<bool>();
        activeAnimationNames = new List<string>();

}

// Update is called once per frame
void Update()
    {

        Move();

        if(ProgressBar.instance.slider.value >= 100 && (characterLevel <= animeGirlModels.Length-2))
        {

            UpgradePlayerSkin();

        }
        

    }

    private void AutoForwardMovement()
    {

        playerController.SimpleMove(Vector3.forward * speed/2);

    }

    void Move()
    {

        AutoForwardMovement();

        float h = Input.GetAxis("Horizontal");

        playerController.SimpleMove(Vector3.right * h * speed);

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
