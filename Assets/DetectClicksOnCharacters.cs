using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectClicksOnCharacters : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit = new RaycastHit();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {


                if (hit.collider.gameObject.tag == "Patient")
                {
                    PatientInfo.setActiveCharacter(hit.collider.gameObject);
                    PatientInfo.name.text = hit.collider.gameObject.GetComponent<Patient>().name;
                    PatientInfo.gender.text = hit.collider.gameObject.GetComponent<Patient>().gender;
                    PatientInfo.DisplayState(hit.collider.gameObject);
                    PatientInfo.activatePatientBar(hit.collider.gameObject.GetComponent<Patient>().patience);

                    PatientInfo.showPanel();
                }
                else
                {
                    PatientInfo.disablePanel();

                }
            }
        }
    }

}

