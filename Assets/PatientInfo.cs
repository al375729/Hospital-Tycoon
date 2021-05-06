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
    public static GameObject clickedCharacter;
    public static GameObject patienceBar;

    public static bool isCharacterWaiting;
    public static bool change = false;

    private static LTDescr animation;

    public static float clickedCharachterPatient;

    public static Image image;
    void Start()
    {
        panel = this.gameObject;
        name = transform.GetChild(0).GetComponent<Text>();
        gender = transform.GetChild(1).GetComponent<Text>();
        state = transform.GetChild(2).GetComponent<Text>();
        patienceBar = transform.GetChild(3).gameObject;
        image = transform.GetChild(4).GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (clickedCharacter != null)
        {
            detectState();

            if (!clickedCharacter.GetComponent<Patient>().waiting && animation != null )
            {

                stopAnimationPatienceBar();

            }

            if (clickedCharacter.GetComponent<Patient>().waiting && animation == null) 
            {
                startAnimationPatienceBar();
            } 
        } 

        
    }

    private void detectState()
    {
        switch (clickedCharacter.GetComponent<Patient>().state)
        {
            case Patient.State.WaitingForTask:
                break;
            case Patient.State.DoingTask:
                break;
            case Patient.State.WaitingToBeAttendedQueue:
                PatientInfo.state.text = "Waiting Recepcionist";
                break;
            case Patient.State.GoingToQueue:
                PatientInfo.state.text = "Going to being attended on reception";
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
                PatientInfo.state.text = "Waiting for a consult";
                break;
            case Patient.State.GoingToConsult:
                PatientInfo.state.text = "Going to consult";
                break;
            case Patient.State.GettingConsult:
                PatientInfo.state.text = "Being attended at a consult";
                break;
            case Patient.State.WaitingForRadiology:
                PatientInfo.state.text = "Waiting for radiology";
                break;
            case Patient.State.GoingToRadiology:
                PatientInfo.state.text = "Going to radiology";
                break;
            case Patient.State.GettingRadiology:
                PatientInfo.state.text = "Being attended at radiology";
                break;
            case Patient.State.WaitingForAnalysis:
                PatientInfo.state.text = "Waiting for analysis";
                break;
            case Patient.State.GoingToAnalysis:
                PatientInfo.state.text = "Going to analysis";
                break;
            case Patient.State.GettingAnalysis:
                PatientInfo.state.text = "Being attended at analysis";
                break;
            case Patient.State.WaitingForDoctor:
                PatientInfo.state.text = "Waiting for doctor";
                break;
            default:
                break;
        }
    }

    internal static void activatePatientBar(float patience)
    {
        clickedCharachterPatient = patience;
    }

    internal static void setActiveCharacter(GameObject gameObject)
    {
        clickedCharacter = gameObject;
        change = true;
    }

    public static void showPanel()
    {
        Color tempColor = panel.GetComponent<Image>().color;
        tempColor.a = 1f;
        panel.gameObject.GetComponent<Image>().color = tempColor;

        name.gameObject.SetActive(true);
        gender.gameObject.SetActive(true);
        state.gameObject.SetActive(true);
        state.gameObject.SetActive(true);
        image.gameObject.SetActive(true);
        image.gameObject.GetComponent<Image>().sprite = clickedCharacter.GetComponent<Patient>().sprite;


        startAnimationPatienceBar();

    }

    private static void startAnimationPatienceBar()
    {
        float targetTime = clickedCharachterPatient * 120; 
        Debug.Log(clickedCharachterPatient);
        Debug.Log(patienceBar.transform.GetChild(0).gameObject.name);
        patienceBar.transform.GetChild(0).gameObject.transform.GetComponent<RectTransform>().localScale = new Vector3(clickedCharachterPatient, 1f, 1f);
        patienceBar.SetActive(true);

        if (animation != null) stopAnimationPatienceBar();
        animation = LeanTween.scaleX(patienceBar.transform.GetChild(0).gameObject, 0, targetTime).setOnComplete(patientReturnHome);

    }

    private static void stopAnimationPatienceBar()
    {
        if(animation != null)
        {
            LeanTween.cancel(animation.id);
            animation = null;
        }
            
            
    }


    private static void patientReturnHome()
    {
        Debug.Log("yata");
    }

    internal static void DisplayState(GameObject patient)
    {
        
    }

    public static void disablePanel()
    {

        Color tempColor = panel.GetComponent<Image>().color;
        tempColor.a = 0f;
        panel.gameObject.GetComponent<Image>().color = tempColor;

        clickedCharacter = null;

        name.gameObject.SetActive(false);
        gender.gameObject.SetActive(false);
        state.gameObject.SetActive(false);
        patienceBar.SetActive(false);
        stopAnimationPatienceBar();
        image.gameObject.SetActive(false);
    }
}
