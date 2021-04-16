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
    private TaskManagement taskManagement;
    private TaskManagement.TaskClean task;

    private Vector3 target;

    Renderer rend;

    public Color c;

    public bool working = false;

    private bool sub_task1 = false;
    private bool sub_task2 = false;
    private bool sub_task3 = false;

    private bool runing = false;

    private TaskManagement.TaskClean taskClean;
    private TaskManagement.TaskCleanStain taskCleanStain;

    private NavMeshAgent agent;

    public GameObject mancha;

    private GameObject Stain;

    private Vector3 comprobacion = new Vector3(123f, 321f, 456f);

    NavMeshAgent navMeshAgent;
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
        DoingTaskClean,
    }

    private void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        taskManagement = TaskManagement.Instance;
        state = State.WaitingForTask;
        currentTask = CurrentTask.nullTask;

        agent = this.GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (target != null && target != comprobacion && agent.remainingDistance >= 1.5f)
        {
            //target = comprobacion;
            //Vector3 rotation = Quaternion.LookRotation(target).eulerAngles;
            //rotation.y = 0f;
            //rotation.z = 0f;

            transform.LookAt(target);
        }

        if (state == State.WaitingForTask && working && gameObject.GetComponent<NavMeshAgent>()!= null)
        {
            waitingTime -= Time.deltaTime;

            if (waitingTime <= 0)
            {
                waitingTime = maxWaitingTime;
                RequestTask();
                RequestTaskClean();
            }
        }

        if (state == State.DoingTask && working)
        {
            ManageTaskClean(taskClean);

        }
        else if (state == State.DoingTaskClean && working)
        {
            Stain = taskCleanStain.trash;
            ManageTaskCleanStain(taskCleanStain);

        }

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
            target = Vector3.zero;

        }
    }

    private void ManageTaskCleanStain(TaskManagement.TaskCleanStain taskClean)
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

        else if (sub_task1 == true && sub_task2)
        {
            Debug.Log("He acabado todo");
            Destroy(Stain.gameObject);
            Stain = null;

            StopAllCoroutines();
            RestartValues();
            navMeshAgent.isStopped = true; ;

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
        if (currentTask == CurrentTask.task2 && state == State.DoingTaskClean)
        {
            target = comprobacion;
            StartCoroutine(FadeOut());
        }
        else if (currentTask == CurrentTask.task2 && state == State.DoingTask)
        {
            sub_task2 = true;
            runing = false;
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

    public void RequestTaskClean()
    {
        taskCleanStain = taskManagement.RequestTaskClean();
        if (taskCleanStain != null)
        {
            state = State.DoingTaskClean;
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
        LeanTween.alpha(Stain, 0f, 2f).setDelay(0f);
        yield return new WaitForSeconds(2);
        sub_task2 = true;
        runing = false;
    }

}