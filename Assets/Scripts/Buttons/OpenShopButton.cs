﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenShopButton : MonoBehaviour
{
    private Button button;
    public EditModeButton editModeButton;
    public DeleteModeButton deleteModeButton;
    public GameObject antiClick;
    public GameObject shop;
    void Start()
    {
        button = this.gameObject.GetComponent<Button>();
        button.onClick.AddListener(onButtonPressed);
    }

    private void onButtonPressed()
    {
        if ((GlobalVariables.DELETE_MODE || GlobalVariables.EDIT_MODE) && !GlobalVariables.UI_OPEN)
        {
            if (GlobalVariables.DELETE_MODE) Console.setText("Desactiva primero el modo destruccion");
            else if (GlobalVariables.EDIT_MODE) Console.setText("Desactiva primero el modo destruccion");
        }

        else if (!GlobalVariables.EDIT_MODE && !GlobalVariables.DELETE_MODE)
        {
                //editModeButton.setEditMode(false);
                //deleteModeButton.setDeleteMode(false);
                shop.SetActive(true);
                antiClick.SetActive(true);
                GlobalVariables.UI_OPEN = true;
                button.image.color = Color.green;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
