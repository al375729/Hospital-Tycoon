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

    public string gender;

    NavMeshAgent navMeshAgent;

    public WatingRoom waitingRoom;
    public RadiologyController radiologyController;
    public TaskManagement taskManagement;

    public  Diseases diseases;
    public Diseases.Disease patientDisease;

    public int stars;

    public bool onQueue = false;

    public GameObject exit;
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
        SearchingConsult,
        NextTask,
        GoingHome,
        
        GoingToReception,
        GoingToQueue,
        WaitingToBeAttended,
        WaitingToBeAttendedQueue,
        GettinAttended,

        WaitingForConsult,
        GoingToConsult,
        GettingConsult,

        WaitingForRadiology,
        GoingToRadiology,
        GettingRadiology,

        WaitingForAnalysis,
        GoingToAnalysis,
        GettingAnalysis,

    }

    public void addPos(Vector3 pos)
    {
        state = State.DoingTask;
        currentTask = CurrentTask.task1;
        this.transform.SetParent(null);
        target = pos;
        callCoroutine();
    } 

    public void addTask(TaskManagement.PatientGoTo task1)
    {
        task = task1;
           
        if (task != null)
        {
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
        currentTask = CurrentTask.nullTask;

        agent = this.GetComponent<NavMeshAgent>();

        waitingRoom = WatingRoom.Instance;

        
        state = State.GoingToReception;
        PatientInfo.DisplayState(this.gameObject);

        waitingRoom.searchPlace(this.gameObject);

        

        diseases = Diseases.Instance;

        radiologyController = RadiologyController.Instance;

       
    }

    private void Update()
    {
        if (target != null && agent.remainingDistance >= 1.25f)
        {

            transform.LookAt(target);
        }


        else if (endedTask)
        {
            StopAllCoroutines();
            RestartValues();
            target = Vector3.zero;

        }

    } 
    private void RestartValues()
    {


        runing = false;

        endedTask = false;
    }

    public void ChangeState()
    {

        if (state == State.GoingToRadiology)
        {
            //Debug.Log(this.gameObject.name);
            if (patientDisease.stars == 1)
            {
                addPos(new Vector3(-103f,0f,-103f));
            }
            else
            {
               
                if(patientDisease.tasks.Count>0)
                {
                    patientDisease.tasks.Dequeue();
                    radiologyController.searchPatient(this.gameObject);
                }
                else addPos(new Vector3(-103f, 0f, -103f));

            }
            
        }
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

                    if(state == State.GoingToQueue)
                    {
                        Debug.Log("asd");
                        state = State.WaitingToBeAttendedQueue;
                    }
                    yield break;

                }
                yield break;
            }

            yield return null;
        }
    }
}
