using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class RestRoom : MonoBehaviour
{
    public Transform[] queue;

    static public Queue<GameObject> attendancePriority;
    TaskManagement taskManagement;

    ConsultController consultController;

    private bool found;

    public static RestRoom Instance { get; private set; } // static singleton
    void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }

    }
    void Start()
    {
        taskManagement = TaskManagement.Instance;

        attendancePriority = new Queue<GameObject>(30);

        consultController = ConsultController.Instance;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
    void OnMouseDown()
    {

        if (!IsMouseOverUI())
        {
            if (GlobalVariables.EDIT_MODE)
            {

                Console.setText("No puedes mover la sala de descanso");

            }

            if (GlobalVariables.DELETE_MODE)
            {

                Console.setText("No puedes eliminar la sala de descanso");

            }

        }

     }
        public void searchPlace(GameObject doctor)
    {
        foreach (Transform seat in queue)
        {
            if (seat.childCount == 0)
            {
                TaskManagement.PatientGoTo task1 = taskManagement.createTaskPatientToGo(seat.gameObject);
                doctor.GetComponent<Worker>().addTask(task1);

                break;
            }
        }

    }

    public void receptionEmpty(int index)
    {
        if (attendancePriority.Count != 0)
        {
            Transform target = queue[index];

            GameObject patient = attendancePriority.Peek();
            attendancePriority.Dequeue();

            TaskManagement.PatientGoTo task = new TaskManagement.PatientGoTo(target.gameObject);
            patient.GetComponent<Patient>().addTask(task);
        }
    }
}
