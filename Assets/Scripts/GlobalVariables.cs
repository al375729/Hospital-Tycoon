﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour
{
    public static bool EDIT_MODE;
    public static bool DELETE_MODE;
    public static bool UI_OPEN;
    public static int MONEY = 1000;
    void Awake()
    {
        EDIT_MODE = false;
        DELETE_MODE = false;
        UI_OPEN = false;
        MONEY = 1000;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
