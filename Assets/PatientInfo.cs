using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PatientInfo : MonoBehaviour
{
    public static Text name;
    public static Text gender;
    public static Text state;
    public static GameObject panel;

    void Start()
    {
        panel = this.gameObject;
        name = transform.GetChild(0).GetComponent<Text>();
        gender = transform.GetChild(1).GetComponent<Text>();
        state = transform.GetChild(2).GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void showPanel()
    {
        Color tempColor = panel.GetComponent<Image>().color;
        tempColor.a = 1f;
        panel.gameObject.GetComponent<Image>().color = tempColor;

        name.gameObject.SetActive(true);
        gender.gameObject.SetActive(true);
        state.gameObject.SetActive(true);

    }

    internal static void DisplayState(GameObject patient)
    {
        switch (patient.GetComponent<Patient>().state)
        {
            case Patient.State.WaitingForTask:
                break;
            case Patient.State.DoingTask:
                break;
            case Patient.State.WaitingToBeAttendedQueue:
                PatientInfo.state.text = "Going to queueueue";
                break;
            case Patient.State.GoingToQueue:
                PatientInfo.state.text = "Going to 12312";
                break;
            case Patient.State.NextTask:
                break;
            case Patient.State.GoingHome:
                break;
            case Patient.State.GoingToReception:
                PatientInfo.state.text = "Going to reception";
                break;
            case Patient.State.WaitingToBeAttended:
                PatientInfo.state.text = "Waiting to be attended";
                break;
            case Patient.State.GettinAttended:
                PatientInfo.state.text = "Being attended at reception";
                break;
            case Patient.State.WaitingForConsult:
                PatientInfo.state.text = "Waiting for a consultZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZ";
                break;
            case Patient.State.GoingToConsult:
                PatientInfo.state.text = "Going to consult";
                break;
            case Patient.State.GettingConsult:
                PatientInfo.state.text = "Being attended at a consult";
                break;
            case Patient.State.WaitingForRadiology:
                break;
            case Patient.State.GoingToRadiology:
                break;
            case Patient.State.GettingRadiology:
                break;
            case Patient.State.WaitingForAnalysis:
                break;
            case Patient.State.GoingToAnalysis:
                break;
            case Patient.State.GettingAnalysis:
                break;
            default:
                break;
        }
    }

    public static void disablePanel()
    {

        Color tempColor = panel.GetComponent<Image>().color;
        tempColor.a = 0f;
        panel.gameObject.GetComponent<Image>().color = tempColor;

        name.gameObject.SetActive(false);
        gender.gameObject.SetActive(false);
        state.gameObject.SetActive(false);
    }
}
