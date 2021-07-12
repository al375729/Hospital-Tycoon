using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DoctorInfo : MonoBehaviour
{
    public static TextMeshProUGUI name;
    public static TextMeshProUGUI role;
    public static TextMeshProUGUI gender;
    public static TextMeshProUGUI state;
    public static GameObject panel;
    public static GameObject clickedDoctor;
    public static Image image;
    public static GameObject subPanel;
    public static Button button;
    public static Button changebutton;

    public TextMeshProUGUI publicName;
    public TextMeshProUGUI publicRole;
    public TextMeshProUGUI publicGender;
    public TextMeshProUGUI publicState;
    public  GameObject publicPanel;
    public GameObject publicSubPanel;
    public  Image publicImage;
    public  Button publicButton;
    public  Button publicChangebutton;

    public static GameObject canvas;
    public Color color;
    public Worker.currentState workerState;

    public GameObject changeJobsPanel;
    //public static GameObject patienceBar;

    //public static bool isCharacterWaiting;
    //public static bool change = false;

    private static LTDescr animation;

    //public static float clickedDoctor;

    void Start()
    {
        panel = this.gameObject;
        name = publicName;
        gender = publicGender;
        state = publicState;
        image = publicImage;
        button = publicButton;
        changebutton = publicChangebutton;
        role = publicRole;
        subPanel = publicSubPanel;
        //patienceBar = transform.GetChild(3).gameObject;
        canvas = this.gameObject.transform.parent.gameObject;

        button.onClick.AddListener(lockPanel);
        changebutton.onClick.AddListener(openChangeJobs);

        color = button.colors.normalColor;
    }

    private void openChangeJobs()
    {
        changeJobsPanel.SetActive(true);
    }

    public static void setRole(string work)
    {
        role.text = work;
    }
    void lockPanel()
    {
        Debug.Log("click");
        if (DetectClicksOnDoctors.locked == true)
        {
            DetectClicksOnDoctors.locked = false;
            button.gameObject.GetComponent<Image>().color = Color.white;
        }

        else 
        {
            button.gameObject.GetComponent<Image>().color = Color.green;
            DetectClicksOnDoctors.locked = true;
        }



    }

    // Update is called once per frame
    void Update()
    {
        if (clickedDoctor != null)
        {
            detectState();/*

            if (!clickedCharacter.GetComponent<Patient>().waiting && animation != null)
            {

                stopAnimationPatienceBar();

            }

            if (clickedCharacter.GetComponent<Patient>().waiting && animation == null)
            {
                startAnimationPatienceBar();
            }*/
        }
        

    }

    private void detectState()
    {
        
        switch (clickedDoctor.GetComponent<Worker>().currentstate)
        {
            case Worker.currentState.GoingToRestRoom:
                DoctorInfo.state.text = "Going to restroom";
                break;
            case Worker.currentState.Resting:
                DoctorInfo.state.text = "Resting";
                break;
            case Worker.currentState.GoingToConsult:
                DoctorInfo.state.text = "Going to consult";
                break;
            case Worker.currentState.DoingConsult:
                DoctorInfo.state.text = "Doing to consult";
                break;
            default:
                break;
        }
    }

    internal static void activatePatientBar(float patience)
    {
        //clickedCharachterPatient = patience;
    }

    internal static void setActiveCharacter(GameObject gameObject)
    {
        clickedDoctor = gameObject;
        //change = true;
    }

    public static void showPanel()
    {
        Color tempColor = panel.GetComponent<Image>().color;
        tempColor.a = 1f;
        panel.gameObject.GetComponent<Image>().color = tempColor;

        tempColor = subPanel.GetComponent<Image>().color;
        tempColor.a = 1f;
        subPanel.gameObject.GetComponent<Image>().color = tempColor;

        canvas.gameObject.SetActive(true);
        name.gameObject.SetActive(true);
        image.gameObject.SetActive(true);
        image.gameObject.GetComponent<Image>().sprite = clickedDoctor.GetComponent<Worker>().sprite;
        gender.gameObject.SetActive(true);
        state.gameObject.SetActive(true);
        state.gameObject.SetActive(true);
        button.gameObject.SetActive(true);
        //changebutton.gameObject.SetActive(true);
        role.gameObject.SetActive(true);

        startAnimationPatienceBar();

    }

    private static void startAnimationPatienceBar()
    {
        /*
        float targetTime = clickedCharachterPatient * 120;
        Debug.Log(clickedCharachterPatient);
        Debug.Log(patienceBar.transform.GetChild(0).gameObject.name);
        patienceBar.transform.GetChild(0).gameObject.transform.GetComponent<RectTransform>().localScale = new Vector3(clickedCharachterPatient, 1f, 1f);
        patienceBar.SetActive(true);

        if (animation != null) stopAnimationPatienceBar();
        animation = LeanTween.scaleX(patienceBar.transform.GetChild(0).gameObject, 0, targetTime).setOnComplete(patientReturnHome);
        */

    }

    private static void stopAnimationPatienceBar()
    {
        if (animation != null)
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

        tempColor = subPanel.GetComponent<Image>().color;
        tempColor.a = 0f;
        subPanel.gameObject.GetComponent<Image>().color = tempColor;

        clickedDoctor = null;

        name.gameObject.SetActive(false);
        image.gameObject.SetActive(false);
        gender.gameObject.SetActive(false);
        role.gameObject.SetActive(false);
        state.gameObject.SetActive(false);
        canvas.gameObject.SetActive(false);
        button.gameObject.SetActive(false);
        changebutton.gameObject.SetActive(false);
        //patienceBar.SetActive(false);
        //stopAnimationPatienceBar();
    }

    public static bool checkMouse()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return true;
        else return false;
    }
}
