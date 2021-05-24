using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetJobsController : MonoBehaviour
{
    private Button reception;
    private Button consult;
    private Button radiologist;
    private Button analysis;

    private static GameObject worker;
    ConsultController consultController;
    RadiologyController radiologyController;
    AnalisisController analisisController;
    ReceptionList receptionList;
    public int jobOrigin = -1;
    public int jobToChange = -1;

    /*
     Recepcion  --> 0
     Consulta   --> 1
     Radiologia --> 2
     Analisis   --> 3
    */
    void Start()
    {
        reception = this.gameObject.transform.GetChild(0).GetComponent<Button>();
        consult = this.gameObject.transform.GetChild(1).GetComponent<Button>();
        radiologist = this.gameObject.transform.GetChild(2).GetComponent<Button>();
        analysis = this.gameObject.transform.GetChild(3).GetComponent<Button>();

        reception.onClick.AddListener(receptionClick);
        consult.onClick.AddListener(consultClick);
        radiologist.onClick.AddListener(radiolgyClick);
        analysis.onClick.AddListener(analisisClick);


        receptionList = ReceptionList.Instance;
        consultController = ConsultController.Instance;
        radiologyController = RadiologyController.Instance;
        analisisController = AnalisisController.Instance;
       
    }

    private void receptionClick()
    {
        jobToChange = 0;
        changeJob();
    }
    private void consultClick()
    {
        jobToChange = 1;
        changeJob();
    }
    private void radiolgyClick()
    {
        jobToChange = 2;
        changeJob();
    }
    private void analisisClick()
    {
        jobToChange = 3;
        changeJob();
    }

    private void changeJob()
    {
        detectPreviousJob();
        int i;
        switch (jobOrigin)
        {   

            case 0:
                 i = worker.gameObject.GetComponent<Recepcionsit>().indexOfWindow;
                //consultController.updateIndexOfDoctors(i);
                worker.gameObject.GetComponent<Recepcionsit>().changeJob(jobToChange);
                break;
            case 1:
                 i = worker.gameObject.GetComponent<Consult>().indexOfWindow;
                consultController.updateIndexOfDoctors(i);
                worker.gameObject.GetComponent<Consult>().changeJob(jobToChange);
                break;
            case 2:
                 i = worker.gameObject.GetComponent<Radiologist>().indexOfWindow;
                radiologyController.updateIndexOfDoctors(i);
                worker.gameObject.GetComponent<Radiologist>().changeJob(jobToChange);
                break;
            case 3:
                 i = worker.gameObject.GetComponent<Analist>().indexOfWindow;
                analisisController.updateIndexOfDoctors(i);
                worker.gameObject.GetComponent<Analist>().changeJob(jobToChange);
                break;
            default:
                break;
        }
        jobOrigin = -1;

        closeWindow();

    }

    private void detectPreviousJob()
    {
        if (worker.GetComponent<Recepcionsit>() == true) 
        {
            jobOrigin = 0;
            Debug.Log("Es recepcion");
        } 
        else if(worker.GetComponent<Consult>() == true)
        {
            jobOrigin = 1;
            Debug.Log("Es consukta");
        }
        else if (worker.GetComponent<Radiologist>() == true)
        {
            jobOrigin = 2;
            Debug.Log("Es radio");
        }
        else if (worker.GetComponent<Analist>() == true)
        {
            jobOrigin = 3;
            Debug.Log("Es analisis");
        }
    }

    private void closeWindow()
    {
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void setWorker(GameObject gameObject)
    {
        worker = gameObject;
    }
}
