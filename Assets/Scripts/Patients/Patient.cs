using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patient : MonoBehaviour
{
    public State state = State.WaitingForTask;
    private CurrentTask currentTask = CurrentTask.nullTask;


    private TaskManagement.PatientGoTo task;

    private Vector3 target;

    public Color c;

    private bool working = true;

    private bool runing = false;

    private NavMeshAgent agent;

    public GameObject mancha;

    private bool endedTask = false;

    NavMeshAgent navMeshAgent;

    public WatingRoom waitingRoom;

    public  Diseases diseases;
    public Diseases.Disease patientDisease;

    public int stars;

    public bool onQueue = false;
    private enum CurrentTask
    {
        task1,
        task2,
        task3,
        nullTask,
    }
    public enum State
    {
        WaitingForTask,
        DoingTask,
        GettinAttended,
        WaitingForConsult,
        SearchingConsult
    }

    public void addTask(TaskManagement.PatientGoTo task1)
    {
        task = task1;
           
        if (task != null)
        {
            state = State.DoingTask;
            currentTask = CurrentTask.task1;
            this.transform.SetParent(task.target.transform);
            target = task.target.transform.position;
            callCoroutine();
        }
    }

    private void Start()
    {
        int ran = Random.Range(0, 101);

        if (ran < 80)
        {
            stars = 1;
            int ran2 = Random.Range(0, Diseases.diseasesLevel1.Count);
            patientDisease = Diseases.diseasesLevel1[ran2];
        }
        else
        {
            stars = 2;
            int ran2 = Random.Range(0, Diseases.diseasesLevel2.Count);
            patientDisease = Diseases.diseasesLevel2[ran2];
        }

        Debug.Log(patientDisease.name);

        navMeshAgent = this.GetComponent<NavMeshAgent>();
        state = State.WaitingForTask;
        currentTask = CurrentTask.nullTask;

        agent = this.GetComponent<NavMeshAgent>();

        waitingRoom = WatingRoom.Instance;

        waitingRoom.searchPlace(this.gameObject);

        diseases = Diseases.Instance;

       
    }

    private void Update()
    {
        if (target != null && agent.remainingDistance >= 0.25f)
        {

            transform.LookAt(target);
        }


        else if (endedTask)
        {
            Debug.Log("Stop");
            StopAllCoroutines();
            RestartValues();
            target = Vector3.zero;

        }


    } 
    private void RestartValues()
    {

        state = State.WaitingForTask;

        runing = false;

        endedTask = false;
    }

    public void callCoroutine()
    {

        StartCoroutine(MoveToTarget());

    }



    IEnumerator MoveToTarget()
    {
        Debug.Log("Move");
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
                    runing = false;
                    endedTask = true;
                    onQueue = true;
                    transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                    yield break;

                }
                yield break;
            }

            yield return null;
        }
    }
}
