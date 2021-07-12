using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallShop : MonoBehaviour
{
    public Button button;
    public Button button2;

    public DrawWalls drawWalls;
    void Start()
    {
        
        button.onClick.AddListener(onButtonPressed);


        button2.onClick.AddListener(onButtonPressed);
    }

    private void onButtonPressed()
    {
        GlobalVariables.WALL_MODE = true;
        drawWalls.buildWall();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
