using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayStatistics : MonoBehaviour
{
    public GameObject antiClick;
    public GameObject statisitcsPanel;

    public GameObject patientsStatisticsPanel;

    private static GameObject copyPanel;
    public static GameObject publicStatisticsPanel;

    private bool pressed = false;

    public static int patientsOnWaitingRoom = 0;
    public static int patientsWaitingConsultation = 0;
    public static int patientsWaitingRadiology = 0;
    public static int patientsWaitingAnalysis = 0;
    public static int patientsWaitingReception = 0;

    public static int treateadPatients = 0;
    public static int treatedIncome = 0;

    public static int notTreateadPatients = 0;
    public static int notTreatedLoses = 0;

    void Start()
    {
        copyPanel = statisitcsPanel;
        publicStatisticsPanel = patientsStatisticsPanel;

        publicStatisticsPanel.gameObject.transform.GetChild(0).GetComponent<Text>().text = "Succesfully treated patients : " + treateadPatients;
        publicStatisticsPanel.gameObject.transform.GetChild(1).GetComponent<Text>().text = "Succesfully treated patients income: " + treatedIncome + "$";

        publicStatisticsPanel.gameObject.transform.GetChild(2).GetComponent<Text>().text = "Not treated patients : " + notTreateadPatients;
        publicStatisticsPanel.gameObject.transform.GetChild(3).GetComponent<Text>().text = "Not treated patients expenses: " + notTreatedLoses + "$";
    }

    public static void changeNumberOfPatientsWaiting(int i)
    {
        patientsOnWaitingRoom += i;
        updateText();
    }

    public static void changeNumberOfPatientsWaitingConsultation(int i)
    {
        patientsWaitingConsultation += i;
        updateText();
    }

    public static void changeNumberOfPatientsWaitingRadiology(int i)
    {
        patientsWaitingRadiology += i;
        updateText();
    }

    public static void changeNumberOfPatientsWaitingAnalysis(int i)
    {
        patientsWaitingAnalysis += i;
        updateText();
    }

    public static void changeNumberOfPatientsWaitingReception(int i)
    {
        patientsWaitingReception += i;
        updateText();
    }
    private static void updateText()
    {
        copyPanel.gameObject.transform.GetChild(0).GetComponent<Text>().text = "Patients in the waiting room : " + patientsOnWaitingRoom;
        copyPanel.gameObject.transform.GetChild(1).GetComponent<Text>().text = "Patients waiting to be attended at the reception: " + patientsWaitingReception;
        copyPanel.gameObject.transform.GetChild(2).GetComponent<Text>().text = "Patients waiting to be attended at a consultation: " + patientsWaitingConsultation;
        copyPanel.gameObject.transform.GetChild(3).GetComponent<Text>().text = "Patients waiting to be attended at a radiology room: " + patientsWaitingRadiology;
        copyPanel.gameObject.transform.GetChild(4).GetComponent<Text>().text = "Patients waiting to be attended at a analysis room : " + patientsWaitingAnalysis;
    }

    public static void updateTextStatistics()
    {
        publicStatisticsPanel.gameObject.transform.GetChild(0).GetComponent<Text>().text = "Succesfully treated patients : " + treateadPatients;
        publicStatisticsPanel.gameObject.transform.GetChild(1).GetComponent<Text>().text = "Succesfully treated patients income: " + treatedIncome + "$";

        publicStatisticsPanel.gameObject.transform.GetChild(2).GetComponent<Text>().text = "Succesfully treated patients : " + notTreateadPatients;
        publicStatisticsPanel.gameObject.transform.GetChild(1).GetComponent<Text>().text = "Succesfully treated patients income: " + notTreatedLoses + "$";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void mostrarTexto()
    {
        copyPanel.gameObject.transform.GetChild(0).GetComponent<Text>().text = "Patients in the waiting room : " + patientsOnWaitingRoom;
        copyPanel.gameObject.transform.GetChild(1).GetComponent<Text>().text = "Patients waiting to be attended at the reception: " + patientsWaitingReception;
        copyPanel.gameObject.transform.GetChild(2).GetComponent<Text>().text = "Patients waiting to be attended at a consultation: " + patientsWaitingConsultation;
        copyPanel.gameObject.transform.GetChild(3).GetComponent<Text>().text = "Patients waiting to be attended at a radiology room: " + patientsWaitingRadiology;
        copyPanel.gameObject.transform.GetChild(4).GetComponent<Text>().text = "Patients waiting to be attended at a analysis room : " + patientsWaitingAnalysis;


    }
}
