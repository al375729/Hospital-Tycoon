using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayController : MonoBehaviour
{

    private Year currentYear;
    public List<Year> allYears;
    public UIDrawGraphs drawGraphIncome;
    public UIDrawGraphs drawGraphExpenses;
    public InstantiateHeaderYear instantiate;
    public GameObject moneyChart;

    public static int yearDisplayed =0;

    public class Year
    {
        public List<Vector2> statistics;
        public int yearIndex;
        public Vector2 minMaxValues = new Vector2(0,0);
        public bool normalized = false;
        public int countValues = 0;
        public Vector2 definitiveMinMax = new Vector2(0, 0);

        public Year(int yearIndex)
        {
            statistics = new List<Vector2>(12);
            this.yearIndex = yearIndex;
            normalized = false;
            minMaxValues = new Vector2(0, 0);
            countValues = 0;
        }
    }

    public static int yearCount = 0;

    private void Awake ()
    {
        allYears = new List<Year>(10);
        currentYear = new Year(0);

        DisplayController.Year years = addNewYear(DayNightCycle.yearCounter++);

        instantiate.addYear(years);


        DayNightCycle.yearCounter++;

        allYears[currentYear.yearIndex] = currentYear;
    }

    internal void changeDisplay(int yearIndex)
    {
        Year chartYear = DataBaseYears.GetYear(yearIndex);

        List<Vector2> dummy = new List<Vector2>(allYears[yearIndex].statistics.Count);
        List<Vector2> dummy2 = new List<Vector2>(allYears[yearIndex].statistics.Count);
        drawGraphIncome.points = dummy;
        drawGraphIncome.SetVerticesDirty();

        drawGraphExpenses.points = dummy2;
        drawGraphExpenses.SetVerticesDirty();

        for (int i = 0; i < allYears[yearIndex].statistics.Count; i++)
        {

            dummy.Add(new Vector2(i, allYears[yearIndex].statistics[i].x));
        }

        for (int i = 0; i < allYears[yearIndex].statistics.Count; i++)
        {
            dummy2.Add(new Vector2(i, allYears[yearIndex].statistics[i].y));

        }

        Vector2 minMax = updateCharts(yearIndex);
        if(chartYear.countValues >2)allYears[yearIndex].minMaxValues = minMax;


        drawGraphIncome.points = dummy;
        drawGraphIncome.SetVerticesDirty();

        drawGraphExpenses.points = dummy2;
        drawGraphExpenses.SetVerticesDirty();
    }

    private List<Vector2> normalize(List<Vector2> dummy,Vector2 minMax)
    {
        for (int i = 0; i < dummy.Count; i++)
        {
            Vector2 vec = dummy[i];
            vec.y = (vec.y - minMax.x) / ((minMax.y - minMax.x) / 10);

            dummy[i] = vec;
        }

        return dummy;
    }

    private void normalizeValues(int yearIndex, int month)
    {
        float valor = (allYears[yearIndex].minMaxValues.y - currentYear.minMaxValues.x) / 10;
        Vector2 vec = allYears[yearIndex].statistics[month];
        //vec.x = (allYears[yearIndex].statistics[month].x * 10) / currentYear.minMaxValues.y;
        //vec.y = (allYears[yearIndex].statistics[month].y * 10) / currentYear.minMaxValues.y;

        vec.x = (allYears[yearIndex].statistics[month].x - currentYear.minMaxValues.x) / valor;
        vec.y = (allYears[yearIndex].statistics[month].y - currentYear.minMaxValues.x) / valor;


        allYears[yearIndex].statistics[month] = vec;
    }


    private void normalizeAll(int yearIndex,Vector2 minMax)
    {
        Year chartYear = DataBaseYears.GetYear(yearIndex);
        drawGraphIncome.points = new List<Vector2>(0);
        drawGraphExpenses.points = new List<Vector2>(0);
 
        for (int i = 0; i < allYears[yearIndex].statistics.Count; i++)
        {
            float valor = (allYears[yearIndex].minMaxValues.y - currentYear.minMaxValues.x) / 10;


            if (chartYear.statistics[i] != new Vector2 (0,0))
            {

                Vector2 vec = allYears[yearIndex].statistics[i];
                vec.x = (chartYear.statistics[i].x - allYears[yearIndex].minMaxValues.x) / valor;
                vec.y = (chartYear.statistics[i].y - allYears[yearIndex].minMaxValues.x) / valor;

                allYears[yearIndex].statistics[i] = vec;



                drawGraphIncome.points.Add(new Vector2(i, vec.x));
                drawGraphExpenses.points.Add(new Vector2(i, vec.y));
            }
            
        }

        currentYear.normalized = true;
    }

    private void normalizeNotDisplayingYear(int yearIndex, Vector2 minMax)
    {
        Year chartYear = DataBaseYears.GetYear(yearIndex);
        drawGraphIncome.points = new List<Vector2>(0);
        drawGraphExpenses.points = new List<Vector2>(0);

        for (int i = 0; i < allYears[yearIndex].statistics.Count; i++)
        {
            if (chartYear.statistics[i] != new Vector2(0, 0))
            {
                Vector2 vec = allYears[yearIndex].statistics[i];
                vec.x = (allYears[yearIndex].statistics[i].x * 10) / minMax.y;
                vec.y = (allYears[yearIndex].statistics[i].y * 10) / minMax.y;

                allYears[yearIndex].statistics[i] = vec;
            }

        }

        currentYear.normalized = true;
    }

    private Vector2 updateChartsWithoutUpdate(int yearIndex)
    {
        Year chartYear = DataBaseYears.GetYear(yearIndex);
        float min = Mathf.Infinity;
        float max = -Mathf.Infinity;

        for (int i = 0; i < chartYear.statistics.Count; i++)
        {
            if (chartYear.statistics[i].x != 0 && chartYear.statistics[i].x < min) min = chartYear.statistics[i].x;

            if (chartYear.statistics[i].x != 0 && chartYear.statistics[i].x > max) max = chartYear.statistics[i].x;
        }

        for (int i = 0; i < chartYear.statistics.Count; i++)
        {
            if (chartYear.statistics[i].y != 0 && chartYear.statistics[i].y < min) min = chartYear.statistics[i].y;

            if (chartYear.statistics[i].y != 0 && chartYear.statistics[i].y > max) max = chartYear.statistics[i].y;
        }

        return new Vector2(min, max);
    }
       

    private Vector2 updateCharts(int yearIndex)
    {
        Year chartYear = DataBaseYears.GetYear(yearIndex);
        float min = Mathf.Infinity;
        float max = -Mathf.Infinity;

        for (int i = 0; i < chartYear.statistics.Count; i++)
        {
            Debug.Log(i+":  " +chartYear.statistics[i].x);
            if (chartYear.statistics[i].x != 0 && chartYear.statistics[i].x < min) min = chartYear.statistics[i].x;

            if (chartYear.statistics[i].x != 0 && chartYear.statistics[i].x > max) max = chartYear.statistics[i].x;
        }

        for (int i = 0; i < chartYear.statistics.Count; i++)
        {
            Debug.Log(i + ":  " + chartYear.statistics[i].y);
            if (chartYear.statistics[i].y != 0 && chartYear.statistics[i].y < min) min = chartYear.statistics[i].y;

            if (chartYear.statistics[i].y != 0 &&  chartYear.statistics[i].y > max) max = chartYear.statistics[i].y;
        }

        float x = max - min;


        float y = x / 10;

        for (int i = 0; i < 11; i++)
        {
            moneyChart.transform.GetChild(i).GetComponent<Text>().text = (min + (y * i)).ToString();

        }
        Debug.Log(min + "  -  " + max);
        return new Vector2(min, max);
    }

    private void Start()
    {
    }

    public void updateMonth(int month, int income, int expenses)
    {

        Vector2 value = new Vector2(income, expenses);
        currentYear.statistics.Add(value);


        DataBaseYears.setStatisticYear(value, currentYear.yearIndex, month);


        if (yearDisplayed == currentYear.yearIndex) 
        {
            Vector2 minMax = updateCharts(currentYear.yearIndex);
            //if (currentYear.minMaxValues == new Vector2(0, 0)) currentYear.minMaxValues = minMax;

            if (allYears[currentYear.yearIndex].statistics.Count >2 && minMax !=currentYear.minMaxValues) 
            {
                currentYear.minMaxValues = minMax;

                normalizeAll(currentYear.yearIndex, minMax);

                drawGraphIncome.SetVerticesDirty();
                drawGraphExpenses.SetVerticesDirty();

            }
            else 
            {
               if(currentYear.normalized)
               {
                    normalizeValues(currentYear.yearIndex,month);
                    drawGraphIncome.points.Add(new Vector2(month, allYears[currentYear.yearIndex].statistics[month].x));
                    drawGraphIncome.SetVerticesDirty();

                    drawGraphExpenses.points.Add(new Vector2(month, allYears[currentYear.yearIndex].statistics[month].y));
                    drawGraphExpenses.SetVerticesDirty();

                    if (drawGraphExpenses.points.Count == 12)
                    {
                        currentYear.definitiveMinMax = currentYear.minMaxValues;
                    }
                        
                       
               }
               else
               {
                    drawGraphIncome.points.Add(new Vector2(month, allYears[currentYear.yearIndex].statistics[month].x));
                    drawGraphIncome.SetVerticesDirty();

                    drawGraphExpenses.points.Add(new Vector2(month, allYears[currentYear.yearIndex].statistics[month].y));
                    drawGraphExpenses.SetVerticesDirty();
               }

            }
        } 
        else
        {
            
            Vector2 minMax = updateChartsWithoutUpdate(currentYear.yearIndex);
            


            if (allYears[currentYear.yearIndex].statistics.Count > 2 && minMax != currentYear.minMaxValues)
            {
                currentYear.minMaxValues = minMax;
                normalizeNotDisplayingYear(currentYear.yearIndex, currentYear.minMaxValues);
            }
            else
            {
                if (currentYear.normalized)
                {
                    normalizeValues(currentYear.yearIndex, month);
                }
                else
                {
                   //Nada
                }

            }
        }
        //allYears[currentYear.yearIndex] = currentYear;
    }


    private void Update()
    {            

    }

    internal Year addNewYear(int yearCounter)
    {
        Year newYear = new Year(yearCounter);
        
        allYears.Add(newYear);
        currentYear = newYear;
        currentYear.normalized = false;
        currentYear.minMaxValues = new Vector2(0, 0);

        //Vector2 value = new Vector2(0, 0);
        //currentYear.statistics.Add(value);



        return newYear;
    }
}
