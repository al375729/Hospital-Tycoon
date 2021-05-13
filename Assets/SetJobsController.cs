using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetJobsController : MonoBehaviour
{
    private Button radiologist;
    private static GameObject worker;
    ConsultController consultController;
    void Start()
    {
        radiologist = this.gameObject.transform.GetChild(2).GetComponent<Button>();
        radiologist.onClick.AddListener(changeJobToRadiologist);

        consultController = ConsultController.Instance;
    }

    private void changeJobToRadiologist()
    {
        int i = worker.gameObject.GetComponent<Consult>().indexOfWindow;
        consultController.updateIndexOfDoctors(i);
        worker.AddComponent<Radiologist>();

        worker = null;

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
