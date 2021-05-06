using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectClicksOnDoctors : MonoBehaviour
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


                if (hit.collider.gameObject.tag == "Doctor")
                {
                    DoctorInfo.setActiveCharacter(hit.collider.gameObject);
                    DoctorInfo.name.text = hit.collider.gameObject.GetComponent<Worker>().name;
                    DoctorInfo.gender.text = hit.collider.gameObject.GetComponent<Worker>().gender;
                    //PatientInfo.DisplayState(hit.collider.gameObject);
                    //PatientInfo.activatePatientBar(hit.collider.gameObject.GetComponent<Patient>().patience);

                    DoctorInfo.showPanel();
                }
                else
                {
                    DoctorInfo.disablePanel();

                }
            }
        }
    }
}
