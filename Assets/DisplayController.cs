using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayController : MonoBehaviour
{

    private Year currentYear;
    public List<Year> allYears;
    public UIDrawGraphs drawGraphIncome;
    public UIDrawGraphs drawGraphExpenses;
    public InstantiateHeaderYear instantiate;

    public static int yearDisplayed =0;

    public class Year
    {
        public List<Vector2> statistics;
        public int yearIndex;

        public Year(int yearIndex)
        {
            statistics = new List<Vector2>(12);
            this.yearIndex = yearIndex;

        }
    }

    static int yearCount = 0;

    private void Awake ()
    {
        allYears = new List<Year>(10);
        currentYear = new Year(0);

        Vector2 value = new Vector2( 0, 0);
        currentYear.statistics.Add(value);

        drawGraphIncome.points.Add(new Vector2(0, 0));
        drawGraphExpenses.points.Add(new Vector2(0, 0));



        DisplayController.Year years = addNewYear(DayNightCycle.yearCounter++);

        instantiate.addYear(years);


        DayNightCycle.yearCounter++;
    }

    internal void changeDisplay(int yearIndex)
    {
        /*
        List<Vector2> dummy = new List<Vector2>(drawGraphIncome.points.Count);

        for (int i = 0; i < drawGraphIncome.points.Count; i++)
        {

        }

        int counter = 0;
        foreach (Vector2 item in dummy)
        {
            dummy[0] = new Vector2(counter, allYears[yearIndex].statistics[counter].x);
        }
        counter = 0;
        foreach (Vector2 item in dummy)
        {
            dummy[0] = new Vector2(counter, allYears[yearIndex].statistics[counter].y);
        }*/


        drawGraphIncome.SetVerticesDirty();
        drawGraphExpenses.SetVerticesDirty();
    }

    private void Start()
    {
        Debug.Log(this.gameObject.name);
    }

    public void updateMonth(int month, int income,int expenses)
    {
        //Debug.Log(currentYear.yearIndex);

        Vector2 value = new Vector2(income, expenses);
        currentYear.statistics.Add(value);

        drawGraphIncome.points.Add(new Vector2(month,value.x));
        if(yearDisplayed == currentYear.yearIndex)drawGraphIncome.SetVerticesDirty();

        drawGraphExpenses.points.Add(new Vector2(month, value.y));
        if (yearDisplayed == currentYear.yearIndex) drawGraphExpenses.SetVerticesDirty();
    }

    private void Update()
    {            

    }

    internal Year addNewYear(int yearCounter)
    {
        Year newYear = new Year(yearCounter);
        allYears.Add(newYear);
        Debug.Log(newYear.statistics.Count);
        currentYear = newYear;
        Debug.Log(currentYear.statistics.Count);

        Vector2 value = new Vector2(0, 0);
        currentYear.statistics.Add(value);

        drawGraphIncome.points = new List<Vector2>(0);
        drawGraphExpenses.points = new List<Vector2>(0);


        drawGraphIncome.points.Add(new Vector2(0, 0));
        drawGraphExpenses.points.Add(new Vector2(0, 0));

        return newYear;
    }
}
