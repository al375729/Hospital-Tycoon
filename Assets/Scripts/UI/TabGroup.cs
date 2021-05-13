using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabGroup : MonoBehaviour
{
    public List<TapButton> buttons;

    public Color textColorIdle;
    public Color textColorHover;
    public Color textColorSelected;

    public Color imageColorIdle;
    public Color imageColorHover;
    public Color imageColorSelected;

    private TapButton buttonSelected;

    public List<GameObject> panelObjects;

    private GameObject activePanel;

    public void Suscribe(TapButton button)
    {
        if (buttons == null)
        {
            buttons = new List<TapButton>();
        }

        buttons.Add(button);
    }

    public void OnEnter(TapButton button)
    {
        ResetTabs();
        if(buttonSelected == null || button!=buttonSelected)
        {
            button.text.color = textColorHover;
            button.image.color = imageColorHover;
        }
        
    }

    public void OnExit(TapButton button)
    {
        ResetTabs();
    }
    public void OnSelected(TapButton button)
    {
        buttonSelected = button;
        ResetTabs();
        button.text.color = textColorSelected;
        button.image.color = imageColorSelected;

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
        foreach (TapButton button in buttons)
        {
            if (buttonSelected == null || button!=buttonSelected)
            {
                button.text.color = textColorIdle;
                button.image.color = imageColorIdle;
            }

        }
    }

    public void ResetAll()
    {
        buttonSelected = null;

        if(activePanel != null)activePanel.SetActive(false);
        foreach (TapButton button in buttons)
        {
            button.text.color = textColorIdle;
            button.image.color = imageColorIdle;
        }
    }
}
