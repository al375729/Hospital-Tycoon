using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeleteModeButton : MonoBehaviour
{
    private Button button;
    public EditModeButton editModeButton;
    void Start()
    {
        button = this.gameObject.GetComponent<Button>();
        button.onClick.AddListener(onButtonPressed);
    }

    private void onButtonPressed()
    {
        if ((GlobalVariables.EDIT_MODE || GlobalVariables.UI_OPEN) && !GlobalVariables.DELETE_MODE)
        {
            if (GlobalVariables.EDIT_MODE) Console.setText("First deactivate the edition mode");
            else if (GlobalVariables.UI_OPEN) Console.setText("Sal primero de la tienda");
        }
        else if (!GlobalVariables.DELETE_MODE)
        {
            GlobalVariables.DELETE_MODE = true;
            button.image.color = Color.green;
            editModeButton.setEditMode(false);
        }
        else
        {
            GlobalVariables.DELETE_MODE = false;
            button.image.color = Color.white;
        }
    }

    public void setDeleteMode(bool state)
    {
        if (state)
        {
            GlobalVariables.DELETE_MODE = true;
            button.image.color = Color.green;
        }
        else
        {
            GlobalVariables.DELETE_MODE = false;
            button.image.color = Color.white;
        }
    }
}
