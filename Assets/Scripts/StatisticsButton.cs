using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatisticsButton : MonoBehaviour
{
    private Button button;
    public GameObject antiClick;
    public GameObject statisitcsPanel;
    

    private static GameObject copyPanel;

    private bool pressed = false;

    public static int patientsOnWaitingRoom;


    void Start()
    {
        copyPanel = statisitcsPanel;
        button = this.gameObject.GetComponent<Button>();
        button.onClick.AddListener(onButtonPressed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void onButtonPressed()
    {
        if(!pressed) 
        {
            if ((GlobalVariables.DELETE_MODE || GlobalVariables.EDIT_MODE) && !GlobalVariables.UI_OPEN)
            {
                if (GlobalVariables.DELETE_MODE) Console.setText("Desactiva primero el modo destruccion");
                else if (GlobalVariables.EDIT_MODE) Console.setText("Desactiva primero el modo destruccion");
            }

            else if (!GlobalVariables.EDIT_MODE && !GlobalVariables.DELETE_MODE && !GlobalVariables.UI_OPEN)
            {
                //editModeButton.setEditMode(false);
                //deleteModeButton.setDeleteMode(false);
                statisitcsPanel.SetActive(true);
                antiClick.SetActive(true);
                GlobalVariables.UI_OPEN = true;
                button.image.color = Color.green;
                pressed = true;

                //mostrarTexto();
            }
        }
        else
        {
            statisitcsPanel.SetActive(false);
            antiClick.SetActive(false);
            GlobalVariables.UI_OPEN = false;
            button.image.color = Color.white;
            pressed = false;
        }

    }
    


}
