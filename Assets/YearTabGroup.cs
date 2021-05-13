using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class YearTabGroup : MonoBehaviour
{
    public List<YearTabs> buttons;

    public Color textColorIdle;
    public Color textColorHover;
    public Color textColorSelected;

    public Color imageColorIdle;
    public Color imageColorHover;
    public Color imageColorSelected;

    private YearTabs buttonSelected;

    public List<GameObject> panelObjects;

    private GameObject activePanel;

    public UIDrawGraphs drawGraphIncome;
    public UIDrawGraphs drawGraphExpenses;

    public DisplayController display;
    internal void setYear(int year)
    {
        display.changeDisplay(year);
        DisplayController.yearDisplayed = year;
    }

    public void Suscribe(YearTabs button)
    {
        if (buttons == null)
        {
            buttons = new List<YearTabs>();
        }

        buttons.Add(button);
        Debug.Log(button.name);
    }

    public void OnEnter(YearTabs button)
    {
        ResetTabs();
        if (buttonSelected == null || button != buttonSelected)
        {
            button.text.color = textColorHover;
            button.image.color = imageColorHover;
        }

    }

    public void OnExit(YearTabs button)
    {
        ResetTabs();
    }
    public void OnSelected(YearTabs button)
    {
        buttonSelected = button;
        ResetTabs();
        button.text.color = textColorSelected;
        button.image.color = imageColorSelected;

    }

    public void ResetTabs()
    {
        foreach (YearTabs button in buttons)
        {
            if (buttonSelected == null || button != buttonSelected)
            {
                button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = textColorIdle;
                button.image.color = imageColorIdle;
            }

        }
    }

    public void ResetAll()
    {
        buttonSelected = null;

        activePanel.SetActive(false);
        foreach (YearTabs button in buttons)
        {
            button.text.color = textColorIdle;
            button.image.color = imageColorIdle;
        }
    }
}
