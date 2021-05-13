using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DetectClicksOnDoctors : MonoBehaviour
{
    public static bool locked = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && GlobalVariables.UI_OPEN == false && locked == false && !DoctorInfo.checkMouse() && DetectClicksOnCharacters.locked == false)
        {
            RaycastHit hit = new RaycastHit();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {


                if (hit.collider.gameObject.tag == "Doctor")
                {
                    DoctorInfo.setActiveCharacter(hit.collider.gameObject);
                    DoctorInfo.name.text = hit.collider.gameObject.GetComponent<Worker>().name;
                    DoctorInfo.gender.text = hit.collider.gameObject.GetComponent<Worker>().gender;
                    //PatientInfo.DisplayState(hit.collider.gameObject);
                    //PatientInfo.activatePatientBar(hit.collider.gameObject.GetComponent<Patient>().patience);

                    CameraController.setObjectToFollow(hit.collider.gameObject);

                    DoctorInfo.showPanel();

                    SetJobsController.setWorker(hit.collider.gameObject);
                }
                else if(locked == false)
                {
                    DoctorInfo.disablePanel();
                    CameraController.deleteObjectToFollow();
                }
            }
        }
    }
}
