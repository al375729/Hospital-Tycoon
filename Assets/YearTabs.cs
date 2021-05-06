﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class YearTabs : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public YearTabGroup buttonGroup;

    public Text text;

    public Image image;

    public DisplayController.Year year;

    public void OnPointerClick(PointerEventData eventData)
    {
        buttonGroup.OnSelected(this);
        Debug.Log(year.yearIndex);
        buttonGroup.setYear(year.yearIndex);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonGroup.OnEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonGroup.OnExit(this);
    }

    private void Start()
    {
        buttonGroup = this.gameObject.transform.parent.parent.parent.GetComponent<YearTabGroup>();

        text = text.GetComponent<Text>();
        buttonGroup.Suscribe(this);
        image = this.GetComponent<Image>();


    }


}
