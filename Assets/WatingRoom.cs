using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WatingRoom : MonoBehaviour
{
    public Transform[] seats;
    public Transform[] queue;

    static public Queue<GameObject> attendancePriority;
    TaskManagement taskManagement;

    private bool found;

    public static WatingRoom Instance { get; private set; } // static singleton
    void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }

    }
    void Start()
    {
        taskManagement = TaskManagement.Instance;

        attendancePriority = new Queue<GameObject>(30);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("space"))
        {

            GameObject patient = queue[0].GetChild(0).gameObject;
            receptionEmpty(patient.transform.parent);
            Destroy(patient);
        }
    }
    public void searchPlace(GameObject patient)
    {
        found = false;
        foreach (Transform seat in queue)
        {
            if (seat.childCount == 0 && seat.GetComponent<Reception>().isActive)
            {
                taskManagement.AddTaskPatientGoTo(seat.gameObject);
                found = true;
                break;
            }
        }

        if(!found)
        {
            foreach (Transform seat in seats)
            {
                if (seat.childCount == 0)
                {
                    taskManagement.AddTaskPatientGoTo(seat.gameObject);
                    attendancePriority.Enqueue(patient);
                    break;
                }
            }
        }
    }

    public void receptionEmpty(Transform target)
    {
        GameObject patient = attendancePriority.Peek();
        attendancePriority.Dequeue();

        TaskManagement.PatientGoTo task = new TaskManagement.PatientGoTo(target.gameObject);
        patient.GetComponent<Patient>().addTask(task);
    }
}
