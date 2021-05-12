using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBaseYears : MonoBehaviour
{
    public static List<DisplayController.Year> years;
    void Awake()
    {


        years = new List<DisplayController.Year>(20);

        for (int i = 0; i < 20; i++)
        {
            years.Add(new DisplayController.Year(i));
            years[i].statistics = new List<Vector2>(12);

            //for (int j = 0; j < 12; j++)
            //{
                //years[i].statistics.Add(new Vector2(0, 0));
            //}
        }

    }

    // Update is called once per frame
    void Update()
    {


    }

    public static void setStatisticYear(Vector2 value,int index,int month)
    {
        /*
        Debug.Log("Antes de la añadicion");
        for (int i = 0; i < years[0].statistics.Count; i++)
        {
            Debug.Log(i + ": " + years[index].statistics[i]);
        }


        Debug.Log("Añadiendo ingresos: " + value.x);
        Debug.Log("Añadiendo gastos: " + value.y);
        Debug.Log("Modificar año: " + index);
        Debug.Log("Modificar mes: " + month);*/

        years[index].statistics.Add(new Vector2(0, 0));
        years[index].statistics[month] = value;
        years[index].countValues++;

        /*
        Debug.Log("Despues de la añadicion");
        for (int i = 0; i < years[0].statistics.Count; i++)
        {
            Debug.Log(i+ ": " + years[index].statistics[i]);
        }*/
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
