using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Finish : MonoBehaviour
{

    public GameObject leftDoor;
    public Transform leftAxis;
    public GameObject rightDoor;
    public Transform rightAxis;
    private float counter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (GameManager.Instance.onDoor && rightDoor.transform.rotation.y <= 0.99f)
        {


            RotateDoor(50);
         

        }


    }

    public void RotateDoor(float angle)
    {

        leftDoor.transform.RotateAround(leftAxis.position, Vector3.up, -angle * Time.deltaTime);
        rightDoor.transform.RotateAround(rightAxis.position, Vector3.up, angle * Time.deltaTime);

    }
}
