using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBaseYears : MonoBehaviour
{
    public static List<DisplayController.Year> years;
    void Awake()
    {
        List<Vector2> zero = new List<Vector2>(12);
        for (int i = 0; i < 12; i++)
        {
            zero.Add(new Vector2(0, 0));
        }

        years = new List<DisplayController.Year>(20);

        for (int i = 0; i < 20; i++)
        {
            years.Add(new DisplayController.Year(i));
            years[i].statistics = zero;
        }

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < years[0].statistics.Count; i++)
        {
            if (years[0].statistics[i] == new Vector2(300, 6000)) 
            {
                Debug.Log("ERROR ERROR ERROR ERROR ERROR ERROR ERROR ERROR ERROR ERROR ERROR ERROR ERROR ERROR ERROR ERROR ERROR ERROR ERROR ERROR ERROR ERROR ERROR ERROR ");
            }
           
        }

    }

    public static void setStatisticYear(Vector2 value,int index,int month)
    {
        if (value == new Vector2(300,6000)) Debug.Log("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
        Debug.Log(value.x);
        Debug.Log(value.y);
        Debug.Log(index);
        years[index].statistics[month] = value;
        years[index].countValues++;
    }

    public static Vector2 GetStatisticYear(int index, int month)
    {
        return years[index].statistics[month];
    }

    public static DisplayController.Year GetYear(int index)
    {
        return years[index];
    }
}
