using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditModeButton : MonoBehaviour
{
    private Button button;
    public DeleteModeButton deleteModeButton;
    void Start()
    {
        button = this.gameObject.GetComponent<Button>();
        button.onClick.AddListener(onButtonPressed);
    }

    private void onButtonPressed()
    {
        if((GlobalVariables.DELETE_MODE || GlobalVariables.UI_OPEN) && !GlobalVariables.EDIT_MODE)
        {
           if(GlobalVariables.DELETE_MODE) Console.setText("First deactivate the destruction mode");
           else if (GlobalVariables.UI_OPEN) Console.setText("Sal primero de la tienda");
        }

        else if(!GlobalVariables.EDIT_MODE)
        {
            GlobalVariables.EDIT_MODE = true;
            button.image.color = Color.green;
            deleteModeButton.setDeleteMode(false);
        }
        else
        {
            GlobalVariables.EDIT_MODE = false;
            button.image.color = Color.white;
        }
    }

    public void setEditMode(bool state)
    {
        if(state)
        {
            GlobalVariables.EDIT_MODE = true;
            button.image.color = Color.green;
        }
        else
        {
            GlobalVariables.EDIT_MODE = false;
            button.image.color = Color.white;
        }
    }
}
