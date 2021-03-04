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

    public enum TaskType
    {
        MoveTo,
    }

    private List<TaskClean> taskList;

    private void Start()
    {
        taskList = new List<TaskClean>();
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
    
    public void AddTask(Vector3 position,  Vector3 position2)
    {
        TaskClean task = new TaskClean(position,position2);
        taskList.Add(task);
        Debug.Log("Añadido");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) AddTask(target1.transform.position, target2.transform.position);


    }
}


