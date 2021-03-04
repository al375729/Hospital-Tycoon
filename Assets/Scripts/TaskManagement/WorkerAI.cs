using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorkerAI : MonoBehaviour
{
    private State state = State.WaitingForTask;
    private CurrentTask currentTask = CurrentTask.nullTask;

    private float maxWaitingTime = 1f;
    private float waitingTime = 1f;

    //[SerializeField]
    public TaskManagement taskManagement;
    public TaskManagement.TaskClean task;

    private Vector3 target;

    Renderer rend;

    public Color c;

    private bool sub_task1 = false;
    private bool sub_task2 = false;
    private bool sub_task3 = false;

    private bool runing = false;

    private TaskManagement.TaskClean taskClean;

    private NavMeshAgent agent;

    public GameObject mancha;
    private enum CurrentTask 
    {
        task1,
        task2,
        task3,
        nullTask,
    }
    private enum State
    {
        WaitingForTask,
        DoingTask,
    }

    private void Start()
    {
        //rend = target1.GetComponent<Renderer>();
        //c = rend.material.color;
        state = State.WaitingForTask;
        currentTask = CurrentTask.nullTask;

        agent = this.GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (state == State.WaitingForTask)
        {
            waitingTime -= Time.deltaTime;

            if (waitingTime <= 0)
            {
                waitingTime = maxWaitingTime;
                RequestTask();
            }
        }

        if (state == State.DoingTask)
        {
            ManageTaskClean(taskClean);

        }

        /*
        if (Input.GetMouseButtonDown(0))
            if(state == State.WaitingForTask) RequestTask(); */
        
    }

    private void ManageTaskClean(TaskManagement.TaskClean taskClean)
    {
        if (sub_task1 == false && !runing)
        {
            target = taskClean.position;
            currentTask = CurrentTask.task1;
            callCoroutine();
        }

        else if (sub_task1 == true && !sub_task2 && !runing)
        {
            currentTask = CurrentTask.task2;
            callCoroutine();
        }

        
        else if (sub_task1 == true && sub_task2 &&  !sub_task3 && !runing)
        {
            Debug.Log("r2");
            target = taskClean.position2;
            currentTask = CurrentTask.task3;
            callCoroutine();
        }
        else if (sub_task1 == true && sub_task2 && sub_task3)
        {
            Debug.Log("He acabado todo");

           
            
            StopAllCoroutines();
            RestartValues();

        }
    }

    private void RestartValues()
    {
        //agent.isStopped = true;
        taskClean = null;
        
        state = State.WaitingForTask;

        sub_task1 = false;
        sub_task2  = false;
        sub_task3 = false;

        runing = false;
}

    public void callCoroutine()
    {
        runing = true;
        if (currentTask == CurrentTask.task2)
        {
            StartCoroutine(FadeOut());
        }
        else
        {
            StartCoroutine(ExampleFunction());
        }
        
    }

    public void RequestTask()
    {
        taskClean = taskManagement.RequestTask();
        if (taskClean != null)
        {
            state = State.DoingTask;
        }
    }



    IEnumerator ExampleFunction()
    {
        bool end = false;
        agent.destination = target;
        while (!end)
        {
            if (agent.remainingDistance <= 0.1f && agent.pathPending == false)
            {
                end = true;
            }

            if (end)
            {
                //state = State.WaitingForTask;

                if (currentTask == CurrentTask.task1)
                {
                    //Debug.Log("Fin de la tarea 1");
                    sub_task1 = true;
                    runing = false;
                    yield break;
                }
                else if (currentTask == CurrentTask.task3)
                {
                    //Debug.Log("Fin de la tarea 2");
                    sub_task3 = true;
                    runing = false;
                    yield break;
                    
                }
                yield break;
            }

            yield return null;
        }       
    }


    IEnumerator FadeOut()
    {
        iTween.FadeTo(mancha, 0f, 2f);
        yield return new WaitForSeconds(2);
        sub_task2 = true;
        runing = false;
    }

}