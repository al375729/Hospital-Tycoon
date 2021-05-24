using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TabGroupStats : MonoBehaviour
{
    public List<TapButtonStats> buttons;

    public Color textColorIdle;
    public Color textColorHover;
    public Color textColorSelected;

    public Color imageColorIdle;
    public Color imageColorHover;
    public Color imageColorSelected;

    private TapButtonStats buttonSelected;

    public List<GameObject> panelObjects;

    private GameObject activePanel;

    public void Suscribe(TapButtonStats button)
    {
        if (buttons == null)
        {
            buttons = new List<TapButtonStats>();
        }

        buttons.Add(button);
    }

    public void OnEnter(TapButtonStats button)
    {
        ResetTabs();
        if (buttonSelected == null || button != buttonSelected)
        {
            button.text.color = textColorHover;
            button.image.color = imageColorHover;
        }

    }

    public void OnExit(TapButtonStats button)
    {
        ResetTabs();
    }
    public void OnSelected(TapButtonStats button)
    {
        buttonSelected = button;
        ResetTabs();
        button.text.color = textColorSelected;
        button.image.color = imageColorSelected;

        if (button.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text == "ROOM INFO")
        {
            DisplayStatistics.mostrarTexto();
        }

        else if (button.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text == "STATISTICS")
        {
        }

        int index = button.transform.GetSiblingIndex();
        for (int i = 0; i < panelObjects.Count; i++)
        {
            if (i == index)
            {
                panelObjects[i].SetActive(true);
                activePanel = panelObjects[i];
            }
            else panelObjects[i].SetActive(false);
        }
    }

    public void ResetTabs()
    {
        foreach (TapButtonStats button in buttons)
        {
            if (buttonSelected == null || button != buttonSelected)
            {
                button.text.color = textColorIdle;
                button.image.color = imageColorIdle;
            }

        }
    }

    public void ResetAll()
    {
        buttonSelected = null;

        activePanel.SetActive(false);
        foreach (TapButtonStats button in buttons)
        {
            button.text.color = textColorIdle;
            button.image.color = imageColorIdle;
        }
    }
}
