using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patient : MonoBehaviour
{
    private State state = State.WaitingForTask;
    private CurrentTask currentTask = CurrentTask.nullTask;

    private float maxWaitingTime = 1f;
    private float waitingTime = 1f;

    //[SerializeField]
    private TaskManagement taskManagement;
    private TaskManagement.PatientGoTo task;

    private Vector3 target;

    public Color c;

    private bool working = true;

    private bool sub_task1 = false;
    private bool sub_task2 = false;
    private bool sub_task3 = false;

    private bool runing = false;

    private NavMeshAgent agent;

    public GameObject mancha;

    private bool endedTask = false;

    NavMeshAgent navMeshAgent;

    public WatingRoom waitingRoom;

    public bool onQueue = false;
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
        DoingTaskClean
    }

    public void addTask(TaskManagement.PatientGoTo task1)
    {
        task = task1;
        if (task != null)
        {
            state = State.DoingTask;
        }
    }

    private void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        taskManagement = TaskManagement.Instance;
        state = State.WaitingForTask;
        currentTask = CurrentTask.nullTask;

        agent = this.GetComponent<NavMeshAgent>();

        waitingRoom = WatingRoom.Instance;

        waitingRoom.searchPlace(this.gameObject);

    }

    private void Update()
    {
        if (target != null && agent.remainingDistance >= 0.25f)
        {

            transform.LookAt(target);
        }

        
        if (state == State.WaitingForTask && working && gameObject.GetComponent<NavMeshAgent>() != null && !onQueue)
        {
            waitingTime -= Time.deltaTime;

            if (waitingTime <= 0)
            {
                Debug.Log("12");
                waitingTime = maxWaitingTime;
                RequestTask();
            }
        }

        if (state == State.DoingTask && working)
        {
            ManageTaskClean(task);

        }
    }

    private void ManageTaskClean(TaskManagement.PatientGoTo task)
    {
        if (sub_task1 == false && !runing)
        {
            this.transform.SetParent(task.target.transform);
            target = task.target.transform.position;
            currentTask = CurrentTask.task1;
            callCoroutine();
        }

        else if (endedTask)
        {
            Debug.Log("He acabado todo");



            StopAllCoroutines();
            RestartValues();
            target = Vector3.zero;

        }
    }


    private void RestartValues()
    {

        state = State.WaitingForTask;

        sub_task1 = false;
        sub_task2 = false;
        sub_task3 = false;

        runing = false;

        endedTask = false;
    }

    public void callCoroutine()
    {
        runing = true;
        if (currentTask == CurrentTask.task2 && state == State.DoingTaskClean)
        {
           
        }
        else if (currentTask == CurrentTask.task2 && state == State.DoingTask)
        {
            sub_task2 = true;
            runing = false;
        }
        else
        {
            StartCoroutine(MoveToTarget());
        }

    }

    public void RequestTask()
    {
        Debug.Log("DameAlgo");
        task = taskManagement.RequestTaskPatientGoTo();
        if (task != null)
        {
            Debug.Log("Got a task");
            state = State.DoingTask;
        }
    }

    IEnumerator MoveToTarget()
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

                if (currentTask == CurrentTask.task1)
                {
                    //Debug.Log("Fin de la tarea 1");
                    sub_task1 = true;
                    runing = false;
                    endedTask = true;
                    onQueue = true;
                    yield break;

                }
                yield break;
            }

            yield return null;
        }
    }
}
