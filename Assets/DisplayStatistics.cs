using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayStatistics : MonoBehaviour
{
    public GameObject antiClick;
    public GameObject statisitcsPanel;


    private static GameObject copyPanel;

    private bool pressed = false;

    public static int patientsOnWaitingRoom = 0;
    void Start()
    {
        Debug.Log(this.gameObject.name);
        copyPanel = statisitcsPanel;
    }

    public static void changeNumberOfPatientsWaiting(int i)
    {
        patientsOnWaitingRoom += i;
        updateText();
    }

    private static void updateText()
    {
        copyPanel.gameObject.transform.GetChild(0).GetComponent<Text>().text = "El numero de apcientes en la salad e espera son: " + patientsOnWaitingRoom;
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public static void mostrarTexto()
    {
        copyPanel.gameObject.transform.GetChild(0).GetComponent<Text>().text = "El numero de apcientes en la salad e espera son: " + patientsOnWaitingRoom;
        copyPanel.gameObject.transform.GetChild(1).GetComponent<Text>().text = "El numero de apcientes en la salad e espera son: " + patientsOnWaitingRoom;
        copyPanel.gameObject.transform.GetChild(2).GetComponent<Text>().text = "El numero de apcientes en la salad e espera son: " + patientsOnWaitingRoom;
        copyPanel.gameObject.transform.GetChild(3).GetComponent<Text>().text = "El numero de apcientes en la salad e espera son: " + patientsOnWaitingRoom;
        copyPanel.gameObject.transform.GetChild(4).GetComponent<Text>().text = "El numero de apcientes en la salad e espera son: " + patientsOnWaitingRoom;


    }
}
