using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstantiateHeaderYear : MonoBehaviour
{
    public GameObject prefab;
    public DisplayController displayController;

     DisplayController.Year year;
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addYear(DisplayController.Year years)
    {
        GameObject go = Instantiate(prefab);

        go.transform.SetParent(this.gameObject.transform);

        go.GetComponent<YearTabs>().year = years;

        int year = 2021 + DayNightCycle.yearCounter-1;
        go.transform.GetChild(0).GetComponent<Text>().text = year.ToString();


    }
}
