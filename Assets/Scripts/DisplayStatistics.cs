using System.Collections;
using System.Collections.Generic;
using TMPro;
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

        publicStatisticsPanel.gameObject.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "  "+treateadPatients.ToString();
        publicStatisticsPanel.gameObject.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = "  " + treatedIncome + "$";

        publicStatisticsPanel.gameObject.transform.GetChild(6).GetComponent<TextMeshProUGUI>().text = "  " + notTreateadPatients.ToString();
        publicStatisticsPanel.gameObject.transform.GetChild(7).GetComponent<TextMeshProUGUI>().text = "  " + notTreatedLoses + "$";
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
        copyPanel.gameObject.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = "  " + patientsOnWaitingRoom;
        copyPanel.gameObject.transform.GetChild(6).GetComponent<TextMeshProUGUI>().text = "  " + patientsWaitingReception;
        copyPanel.gameObject.transform.GetChild(7).GetComponent<TextMeshProUGUI>().text = "  " + patientsWaitingConsultation;
        copyPanel.gameObject.transform.GetChild(8).GetComponent<TextMeshProUGUI>().text = "  " + patientsWaitingRadiology;
        copyPanel.gameObject.transform.GetChild(9).GetComponent<TextMeshProUGUI>().text ="  " + patientsWaitingAnalysis;
    }

    public static void updateTextStatistics()
    {
        publicStatisticsPanel.gameObject.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "  " + treateadPatients.ToString();
        publicStatisticsPanel.gameObject.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = "  " + treatedIncome + "$";

        publicStatisticsPanel.gameObject.transform.GetChild(6).GetComponent<TextMeshProUGUI>().text = "  " + notTreateadPatients.ToString();
        publicStatisticsPanel.gameObject.transform.GetChild(7).GetComponent<TextMeshProUGUI>().text = "  " + notTreatedLoses + "$";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void mostrarTexto()
    {
        copyPanel.gameObject.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = "  " + patientsOnWaitingRoom;
        copyPanel.gameObject.transform.GetChild(6).GetComponent<TextMeshProUGUI>().text = "  " + patientsWaitingReception;
        copyPanel.gameObject.transform.GetChild(7).GetComponent<TextMeshProUGUI>().text = "  " + patientsWaitingConsultation;
        copyPanel.gameObject.transform.GetChild(8).GetComponent<TextMeshProUGUI>().text = "  " + patientsWaitingRadiology;
        copyPanel.gameObject.transform.GetChild(9).GetComponent<TextMeshProUGUI>().text = "  " + patientsWaitingAnalysis;


    }
}
