using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DateController : MonoBehaviour
{
    public static Text date;

    public static int day = 1;
    public static int month = 1;
    public static int year = 2021;
    void Start()
    {
        date = GetComponent<Text>();
        changeDate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void setDay(int i)
    {
        day = i;
        changeDate(); 
    }


    public static void setMonth(int i)
    {
        month = i;
        changeDate();
    }

    public static void setYear(int i)
    {
        year = i;
        changeDate();
    }

    public static void changeDate()
    {
        date.text = day + "/" + month + "/" + year;
    }


}
