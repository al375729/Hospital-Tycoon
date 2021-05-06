using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        go.transform.parent = this.gameObject.transform;

        go.GetComponent<YearTabs>().year = years;
        
    }
}
