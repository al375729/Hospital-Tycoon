using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalisisController : MonoBehaviour
{
    public static AnalisisController Instance { get; private set; } // static singleton

    public List<Transform> arrayForDoctors;
    public List<Transform> arrayForPatients;

    public Queue<GameObject> attendancePriorityAnalisis;
    public Queue<GameObject> priorityAnalisisDoctor;

    public TaskManagement taskManagement;
    public TaskManagement.PatientGoTo task;

    WatingRoom waitingRoom;

    private float maxWaitingTime = 1f;
    private float waitingTime = 1f;

    void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }

    }
    void Start()
    {
        taskManagement = TaskManagement.Instance;
        waitingRoom = WatingRoom.Instance;

        attendancePriorityAnalisis = new Queue<GameObject>(30);
        priorityAnalisisDoctor = new Queue<GameObject>(30);
    }

    // Update is called once per frame
    void Update()
    {

        if (attendancePriorityAnalisis.Count != 0)
        {
            waitingTime -= Time.deltaTime;

            if (waitingTime <= 0)
            {
                waitingTime = maxWaitingTime;

                GameObject patient = attendancePriorityAnalisis.Peek();
                patientSearchIfIsAnEmptyRadiology(patient);
            }
        }

        if (priorityAnalisisDoctor.Count != 0)
        {
            Debug.Log("acb");
            waitingTime -= Time.deltaTime;

            if (waitingTime <= 0)
            {
                waitingTime = maxWaitingTime;

                GameObject patient = priorityAnalisisDoctor.Peek();
                Debug.Log(patient.gameObject.name);
                DoctorSearchIfIsAnEmptyRadiology(patient);
            }
        }

    }

    private void patientSearchIfIsAnEmptyRadiology(GameObject patient)
    {
        for (int i = 0; i < arrayForPatients.Count; i++)
        {
            if (arrayForPatients[i].childCount == 0)
            {
                attendancePriorityAnalisis.Dequeue();
                TaskManagement.PatientGoTo task1 = taskManagement.createTaskPatientToGo(arrayForPatients[i].gameObject);
                patient.GetComponent<Patient>().addTask(task1);
                patient.transform.SetParent(arrayForPatients[i]);
                break;
            }
        }
    }

    private void DoctorSearchIfIsAnEmptyRadiology(GameObject patient)
    {
        for (int i = 0; i < arrayForDoctors.Count; i++)
        {
            if (arrayForDoctors[i].childCount == 0)
            {
                Debug.Log(priorityAnalisisDoctor.Peek());
                priorityAnalisisDoctor.Dequeue();
                TaskManagement.PatientGoTo task1 = taskManagement.createTaskPatientToGo(arrayForDoctors[i].gameObject);
                patient.GetComponent<Analist>().goTo(task1.target.transform);
                patient.transform.SetParent(arrayForDoctors[i]);
                break;
            }
        }
    }

    public void updateIndexOfPatients(int index)
    {
        arrayForPatients.RemoveAt(index);

        for (int i = index; i < arrayForPatients.Count; i++)
        {
            arrayForPatients[i].GetComponent<ObjectsOnRoom>().indexInList--;
        }
    }

    public void updateIndexOfDoctors(int index)
    {
        arrayForDoctors.RemoveAt(index);

        for (int i = index; i < arrayForDoctors.Count; i++)
        {
            arrayForDoctors[i].GetComponent<ObjectsOnRoom>().indexInList--;
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

    public void searchRadiologyPatient(GameObject patient)
    {
        bool found = false;
        for (int i = 0; i < arrayForPatients.Count; i++)
        {
            if (arrayForPatients[i].childCount == 0)
            {
                TaskManagement.PatientGoTo task1 = taskManagement.createTaskPatientToGo(arrayForPatients[i].gameObject);
                patient.GetComponent<Patient>().addTask(task1);
                patient.transform.SetParent(arrayForPatients[i]);
                found = true;
                break;
            }
        }


        if (!found)
        {
            returnToWaitingRoom(patient);
        }
    }

    public void searchRadiologyDoctor(GameObject patient)
    {
        bool found = false;
        for (int i = 0; i < arrayForDoctors.Count; i++)
        {
            if (arrayForDoctors[i].childCount == 0)
            {
                Debug.Log("a");
                patient.GetComponent<Analist>().goTo(arrayForDoctors[i]);
                patient.GetComponent<Analist>().indexOfWindow = i;
                patient.GetComponent<Analist>().state = Analist.State.DoingTask;
                patient.transform.SetParent(arrayForDoctors[i]);
                found = true;
                break;
            }
        }

        if (!found)
        {
            goToRestRoom(patient);
        }

    }

    private void goToRestRoom(GameObject patient)
    {
        patient.GetComponent<Analist>().state = Analist.State.DoingTask;
        priorityAnalisisDoctor.Enqueue(patient);
        Debug.Log(priorityAnalisisDoctor.Peek());
        Debug.Log(priorityAnalisisDoctor.Count);
        patient.GetComponent<Worker>().goToRestRoom(patient);
    }

    internal void searchPatient(GameObject patient)
    {
        if (attendancePriorityAnalisis.Count == 0)
        {
            searchRadiologyPatient(patient);
        }
        else
        {
            returnToWaitingRoom(patient);
        }

    }

    public void searchDoctor(GameObject doctor)
    {
        if (priorityAnalisisDoctor.Count == 0)
        {
            searchRadiologyDoctor(doctor);
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
            if (seat.childCount == 0 && patient.GetComponent<Patient>().state != Patient.State.SearchingConsult)
            {

                patient.GetComponent<Patient>().state = Patient.State.SearchingConsult;
                TaskManagement.PatientGoTo task1 = taskManagement.createTaskPatientToGo(seat.gameObject);
                patient.GetComponent<Patient>().addTask(task1);
                attendancePriorityAnalisis.Enqueue(patient);
                break;
            }
        }
    }
}
