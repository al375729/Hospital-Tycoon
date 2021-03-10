using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    private float time = 3.0f;
    //private iTween.EaseType easeType;
    bool asd = false;


    public GameObject g1;
         public GameObject g2;
         public GameObject g3;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)) asd = true;



        if (asd)
        {
            //iTween.FadeTo(g2, 0f, time);
        } 

    }
}
