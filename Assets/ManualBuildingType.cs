using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManualBuildingType : MonoBehaviour
{
    public Button buttonConsult;
    public Button buttonRadiology;
    public Button buttonAnalysis;

    public GameObject panel;
    void Start()
    {
        buttonConsult.onClick.AddListener(consultClick);
        buttonRadiology.onClick.AddListener(radiologyClick);
        buttonAnalysis.onClick.AddListener(analysisClick);
    }

    private void analysisClick()
    {
        DrawSquare.setCurrentRoom(2);
        GlobalVariables.BUILDING_MODE = true;
        panel.SetActive(false);
    }

    private void radiologyClick()
    {
        DrawSquare.setCurrentRoom(1);
        GlobalVariables.BUILDING_MODE = true;
        panel.SetActive(false);
    }

    private void consultClick()
    {
        DrawSquare.setCurrentRoom(0);
        GlobalVariables.BUILDING_MODE = true;
        panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
