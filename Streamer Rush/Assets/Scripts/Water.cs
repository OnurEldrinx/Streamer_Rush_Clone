using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{

    float speed = 0.1f;
    Renderer r;
    public Material m;

    // Start is called before the first frame update
    void Start()
    {

        r = GetComponent<MeshRenderer>();

    }

    // Update is called once per frame
    void Update()
    {

        float offset = Time.time * speed;
        m.mainTextureOffset = new Vector2(offset, 0);
        


    }
}
