using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsultController : MonoBehaviour
{
    public static ConsultController Instance { get; private set; } // static singleton

    public List<Transform> seats;
    public List<Transform> queue;

    public Queue<GameObject> attendancePriorityConsult;

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

        attendancePriorityConsult = new Queue<GameObject>(30);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {

            foreach (Transform item in queue)
            {
                Debug.Log(item.transform.gameObject.name + "Mi posicion es :" + item.position);
            }
        }


        if (attendancePriorityConsult.Count != 0 )
        {
            waitingTime -= Time.deltaTime;

            if (waitingTime <= 0)
            {
                waitingTime = maxWaitingTime;

                GameObject patient = attendancePriorityConsult.Peek();
                searchOnlyConsult(patient);
            }
        }
       
    }

    private void searchOnlyConsult(GameObject patient)
    {
        for (int i = 0; i < queue.Count; i++)
        {
            if (queue[i].childCount == 0)
            {
                attendancePriorityConsult.Dequeue();
                TaskManagement.PatientGoTo task1 = taskManagement.createTaskPatientToGo(queue[i].gameObject);
                patient.GetComponent<Patient>().addTask(task1);
                patient.transform.SetParent(queue[i]);
                break;
            }
        }
    }

    public void updateIndexQueue(int index)
    {
        queue.RemoveAt(index);

        for (int i = index; i < queue.Count; i++)
        {
            queue[i].GetComponent<ObjectsOnRoom>().indexInList2--;
        }
    }

    public void updateIndexSeat(int index)
    {
        seats.RemoveAt(index);

        for (int i = index; i < seats.Count; i++)
        {
            seats[i].GetComponent<ObjectsOnRoom>().indexInList--;
        }
    }

    public int addSeats(Transform position)
    {
        seats.Add(position);
        return seats.Count - 1;
    }

    public int addQueue(Transform position)
    {
        queue.Add(position);
        return queue.Count - 1;
    }

    public void searchConsultPatient(GameObject patient)
    {
        bool found = false;
        for (int i = 0; i < queue.Count; i++)
        {
            if (queue[i].childCount == 0)
            {
                TaskManagement.PatientGoTo task1 = taskManagement.createTaskPatientToGo(queue[i].gameObject);
                patient.GetComponent<Patient>().addTask(task1);
                patient.transform.SetParent(queue[i]);
                found = true;
                break;
            }
        }

        
        if (!found)
        {
            foreach (Transform seat in waitingRoom.seats)
            {
                if (seat.childCount == 0 && patient.GetComponent<Patient>().state != Patient.State.SearchingConsult)
                {

                    patient.GetComponent<Patient>().state = Patient.State.SearchingConsult;
                    TaskManagement.PatientGoTo task1 = taskManagement.createTaskPatientToGo(seat.gameObject);
                    patient.GetComponent<Patient>().addTask(task1);
                    attendancePriorityConsult.Enqueue(patient);
                    break;
                }
            }
        }
    }

    public void searchConsultDoctor(GameObject patient)
    {
        
        for (int i = 0; i < seats.Count; i++)
        {
            if (seats[i].childCount == 0 )
            {
                patient.GetComponent<Consult>().goTo(seats[i]);
                patient.GetComponent<Consult>().indexOfWindow = i;
                patient.GetComponent<Consult>().state = Consult.State.DoingTask;
                patient.transform.SetParent(seats[i]);

                break;
            }
        }
        
    }

    public Transform attend(int i)
    {
        return queue[i];
    }
}
