using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsultController : MonoBehaviour
{
    public static ConsultController Instance { get; private set; } // static singleton

    public List<Transform> arrayForDoctors;
    public List<Transform> arrayForPatients;

    public Queue<GameObject> attendancePriorityConsult;
    public Queue<GameObject> priorityDoctorConsult;

    public TaskManagement taskManagement;
    public TaskManagement.PatientGoTo task;

    WatingRoom waitingRoom;

    private float maxWaitingTime = 1f;
    private float waitingTime = 1f;

    public int workersOnRoom = 0;
    void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }

    }
    void Start()
    {
        taskManagement = TaskManagement.Instance;
        waitingRoom = WatingRoom.Instance;

        attendancePriorityConsult = new Queue<GameObject>(30);
        priorityDoctorConsult = new Queue<GameObject>(30);
    }

    // Update is called once per frame
    void Update()
    {

        if (attendancePriorityConsult.Count != 0 )
        {
            waitingTime -= Time.deltaTime;

            if (waitingTime <= 0)
            {
                waitingTime = maxWaitingTime;

                GameObject patient = attendancePriorityConsult.Peek();
                patientSearchIfIsAnEmptyConsult(patient);
            }
        }

        if (priorityDoctorConsult.Count != 0)
        {
            waitingTime -= Time.deltaTime;

            if (waitingTime <= 0)
            {
                waitingTime = maxWaitingTime;

                GameObject patient = priorityDoctorConsult.Peek();
                DoctorSearchIfIsAnEmptyConsult(patient);
            }
        }

    }

    private void patientSearchIfIsAnEmptyConsult(GameObject patient)
    {
        for (int i = 0; i < arrayForPatients.Count; i++)
        {
            if (arrayForPatients[i].childCount == 0 && arrayForDoctors[i].childCount != 0)
            {
                attendancePriorityConsult.Dequeue();
                TaskManagement.PatientGoTo task1 = taskManagement.createTaskPatientToGo(arrayForPatients[i].gameObject);
                patient.GetComponent<Patient>().addTask(task1);
                patient.transform.SetParent(arrayForPatients[i]);

                patient.gameObject.GetComponent<Patient>().state = Patient.State.GoingToConsult;
                PatientInfo.DisplayState(patient.gameObject);

                break;
            }
        }
    }

    private void DoctorSearchIfIsAnEmptyConsult(GameObject patient)
    {
        for (int i = 0; i < arrayForDoctors.Count; i++)
        {
            if (arrayForDoctors[i].childCount == 0)
            {
                Debug.Log(priorityDoctorConsult.Peek());
                priorityDoctorConsult.Dequeue();
                TaskManagement.PatientGoTo task1 = taskManagement.createTaskPatientToGo(arrayForDoctors[i].gameObject);
                patient.GetComponent<Consult>().goTo(task1.target.transform);
                patient.transform.SetParent(arrayForDoctors[i]);

                patient.gameObject.transform.parent.parent.GetChild(patient.gameObject.transform.parent.parent.childCount - 2).GetComponent<RoomStatus>().workers = "";
                patient.gameObject.transform.parent.parent.GetChild(patient.gameObject.transform.parent.parent.childCount - 2).GetComponent<RoomStatus>().updateText();
                break;
            }
        }
    }

    public void updateIndexOfPatients(int index)
    {
        arrayForPatients.RemoveAt(index);

        if(arrayForPatients.Count > 0)
        {
            for (int i = index; i < arrayForPatients.Count; i++)
            {
                arrayForPatients[i].GetComponent<ObjectsOnRoom>().indexInList--;
            }
        }
       
    }

    public void updateIndexOfDoctors(int index)
    {
        arrayForDoctors.RemoveAt(index);

        if (arrayForDoctors.Count > 0)
        {
            for (int i = index; i < arrayForDoctors.Count; i++)
            {
                arrayForDoctors[i].GetComponent<ObjectsOnRoom>().indexInList--;
            }
        }
    }

    public int addDoctor(Transform position)
    {
        arrayForDoctors.Add(position);
        return arrayForDoctors.Count - 1;
    }

    public int addPatient(Transform position)
    {
        arrayForPatients.Add(position);
        return arrayForPatients.Count - 1;
    }

    public void searchConsultPatient(GameObject patient)
    {
        bool found = false;
        for (int i = 0; i < arrayForPatients.Count; i++)
        {
            if (arrayForPatients[i].childCount == 0 && arrayForDoctors[i].childCount != 0)
            {
                TaskManagement.PatientGoTo task1 = taskManagement.createTaskPatientToGo(arrayForPatients[i].gameObject);
                patient.GetComponent<Patient>().addTask(task1);
                patient.transform.SetParent(arrayForPatients[i]);
                found = true;

                patient.gameObject.GetComponent<Patient>().state = Patient.State.GoingToConsult;
                PatientInfo.DisplayState(patient.gameObject);

                break;
            }
        }


        if (!found)
        {
            Debug.Log("9009");
            patient.gameObject.GetComponent<Patient>().state = Patient.State.WaitingForConsult;
            PatientInfo.DisplayState(patient.gameObject);

            returnToWaitingRoom(patient);
        }
    }

    public void searchConsultDoctor(GameObject patient)
    {
        bool found = false;
        for (int i = 0; i < arrayForDoctors.Count; i++)
        {
            if (arrayForDoctors[i].childCount == 0 )
            {
                patient.GetComponent<Consult>().goTo(arrayForDoctors[i]);
                patient.GetComponent<Consult>().indexOfWindow = i;
                patient.GetComponent<Consult>().state = Consult.State.DoingTask;
                patient.transform.SetParent(arrayForDoctors[i]);
                found = true;
                patient.GetComponent<Consult>().state = Consult.State.DoingTask;
                patient.gameObject.transform.parent.parent.GetChild(patient.gameObject.transform.parent.parent.childCount - 2).GetComponent<RoomStatus>().workers = "";
                patient.gameObject.transform.parent.parent.GetChild(patient.gameObject.transform.parent.parent.childCount - 2).GetComponent<RoomStatus>().updateText();

                break;
            }
        }

        if (!found)
        {
            goToRestRoom(patient);
            patient.GetComponent<Consult>().state = Consult.State.DoingTask;


        }

    }

    public void searchPatient(GameObject patient)
    {
        if (attendancePriorityConsult.Count == 0)
        {
            searchConsultPatient(patient);
        }
        else
        {
            returnToWaitingRoom(patient);
        }
        
    }

    public void searchDoctor(GameObject doctor)
    {
        if (priorityDoctorConsult.Count == 0)
        {
            searchConsultDoctor(doctor);
        }
        else
        {
            goToRestRoom(doctor);
        }

    }

    public Transform attend(int i)
    {
        return arrayForPatients[i];
    }


    private void returnToWaitingRoom(GameObject patient)
    {
        foreach (Transform seat in waitingRoom.seats)
        {
            if (seat.childCount == 0)
            {

                TaskManagement.PatientGoTo task1 = taskManagement.createTaskPatientToGo(seat.gameObject);
                patient.GetComponent<Patient>().addTask(task1);
                attendancePriorityConsult.Enqueue(patient);
                Debug.Log(attendancePriorityConsult.Count);

                patient.gameObject.GetComponent<Patient>().state = Patient.State.WaitingForConsult;
                PatientInfo.DisplayState(patient.gameObject);
                patient.gameObject.GetComponent<Patient>().waiting = true;
                patient.gameObject.GetComponent<Patient>().patienceBool = false;
                break;
            }
        }
    }

    private void goToRestRoom(GameObject patient)
    {

        patient.GetComponent<Consult>().state = Consult.State.DoingTask;
        priorityDoctorConsult.Enqueue(patient);
        Debug.Log(priorityDoctorConsult.Count);

        patient.GetComponent<Worker>().goToRestRoom(patient);
    }
}

