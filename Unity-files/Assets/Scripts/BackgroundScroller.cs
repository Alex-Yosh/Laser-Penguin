using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] float backgroundScrollSpeed = 0.5f;
    Material myMaterial;
    Vector2 offSet;
    Camera cam;
    float width;


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        myMaterial = GetComponent<Renderer>().material;       
        width = 2f * cam.orthographicSize * cam.aspect;
        offSet = new Vector2(width/backgroundScrollSpeed, 0f); 

        Debug.Log(offSet.x);
    }

    // Update is called once per frame
    void Update()
    {
        myMaterial.mainTextureOffset += offSet * Time.deltaTime;
    }

    public void SetScrollSpeed(float spd)
    {
        backgroundScrollSpeed = spd;
        offSet = new Vector2(backgroundScrollSpeed/width, 0f); 
    }
}
