using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TaskManagement : MonoBehaviour
{
    public GameObject target1;
    public GameObject target2;
    public class TaskClean
    {
        public Vector3 position;
        public GameObject trash;
        public Vector3 position2;

        

        public TaskClean(Vector3 position, Vector3 position2)
        {
            this.position = position;
            this.position2 = position2;
        }
    }

    public class TaskCleanStain
    {
        public Vector3 position;
        public GameObject trash;


        public TaskCleanStain(Vector3 position, GameObject objectToClean)
        {
            this.position = position;
            trash = objectToClean;
        }
    }

    public class PatientGoTo
    {
        public GameObject target;


        public PatientGoTo(GameObject target)
        {
            this.target = target;
        }
    }


    public static TaskManagement Instance { get; private set; } // static singleton
    void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }

    }

    private List<TaskClean> taskList;
    private List<TaskCleanStain> taskListStain;
    private List<PatientGoTo> taskPatientGoTo;
    public GameObject prueba;

    private void Start()
    {
        taskList = new List<TaskClean>();
        taskListStain = new List<TaskCleanStain>();
        taskPatientGoTo = new List<PatientGoTo>();
    }

   
    
    public TaskClean RequestTask()
    {
        if (taskList.Count > 0)
        {
            TaskClean task = taskList[0];
            taskList.RemoveAt(0);
            return task;
        }
        else
        {
            return null;
        }
    }

    public TaskCleanStain RequestTaskClean()
    {
        if (taskListStain.Count > 0)
        {
            TaskCleanStain task = taskListStain[0];
            taskListStain.RemoveAt(0);
            return task;
        }
        else
        {
            return null;
        }
    }

    public PatientGoTo RequestTaskPatientGoTo()
    {
        if (taskPatientGoTo.Count > 0)
        {
            PatientGoTo task = taskPatientGoTo[0];
            taskPatientGoTo.RemoveAt(0);
            Debug.Log(task + "12");
            return task;
        }
        else
        {
            return null;
        }
    }

    public void AddTask(Vector3 position,  Vector3 position2)
    {
        TaskClean task = new TaskClean(position,position2);
        taskList.Add(task);
        Debug.Log("add");
    }

    public void AddTaskCleanStain(Vector3 position, GameObject objectToClean)
    {
        TaskCleanStain task = new TaskCleanStain(position,objectToClean);
        Debug.Log(task);
        taskListStain.Add(task);
    }

    public void AddTaskPatientGoTo(GameObject target)
    {
        PatientGoTo task = new PatientGoTo(target);
        taskPatientGoTo.Add(task);
    }

    private void Update()
    {
        //if (Input.GetMouseButtonDown(2)) AddTask(target1.transform.position, target2.transform.position);

    }
}


