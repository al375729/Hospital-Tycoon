using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class Seleccionador : MonoBehaviour
{
    public Button seleccionar;
    public Button noSeleccionar;
    public Image imagen;
    public GameObject plane;

    public Test test;

    public  int selection = -1;

    void Start()
    {
        hideButtons();

        seleccionar.onClick.AddListener(Selected);
        noSeleccionar.onClick.AddListener(notSelected);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public  void makeSelection()
    {
        showButtons();

    }

    private  void notSelected()
    {
        //DragBuildings.selection = false;
        hideButtons();
    }

    public void showButtons()
    {
        seleccionar.gameObject.SetActive(true);
        noSeleccionar.gameObject.SetActive(true);
        imagen.gameObject.SetActive(true);
        plane.SetActive(true);

        RotateWithMouse.UIDisplaying = true;
    }

    private  void  Selected()
    {
        //DragBuildings.selection = true;
        hideButtons();
    }

    public void hideButtons()
    {
        seleccionar.gameObject.SetActive(false);
        noSeleccionar.gameObject.SetActive(false);
        imagen.gameObject.SetActive(false);
        plane.SetActive(false);

        RotateWithMouse.UIDisplaying = false;
    }
}
