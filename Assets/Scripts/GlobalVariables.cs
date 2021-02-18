using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour
{
    public static bool EDIT_MODE;
    public static bool DELETE_MODE;
    void Start()
    {
        EDIT_MODE = false;
        DELETE_MODE = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
