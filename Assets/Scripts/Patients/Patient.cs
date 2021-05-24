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

    private Queue<string> copiaCola;

    public int stars;

    public bool onQueue = false;

    public GameObject exit;

    private int ran;

    public float patience = 1;

    public bool patienceBool = false;

    public bool waiting = false;

    public Sprite sprite;
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
        WaitingForDoctor,

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

         ran = Random.Range(0, Diseases.diseasesLevel2.Count-1);

        /*if (ran < 80)
        {
            stars = 1;
            int ran2 = Random.Range(0, Diseases.diseasesLevel1.Count);
            patientDisease = Diseases.GetDisease(ran);
            copyPatientDisease = patientDisease;
        }*/
        //else
        //{
            stars = 2;
            //int ran2 = Random.Range(0, Diseases.diseasesLevel2.Count);
            patientDisease = Diseases.GetDisease(ran);

            copiaCola = new Queue<string>();

        foreach (string item in patientDisease.tasks)
        {
            copiaCola.Enqueue(item);
        }
       // }

        Debug.Log(patientDisease.name);

        navMeshAgent = this.GetComponent<NavMeshAgent>();
        currentTask = CurrentTask.nullTask;

        agent = this.GetComponent<NavMeshAgent>();

        waitingRoom = WatingRoom.Instance;

        
        state = State.GoingToReception;
        PatientInfo.DisplayState(this.gameObject);

        waitingRoom.searchPlace(this.gameObject);
        //waiting = true;

        

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

        if (waiting && patience > 0 && !patienceBool) InvokeRepeating("DoSomething", 0, 0.1F);
        else if (!waiting) CancelInvoke();
        else if (patience < 0) 
        {
            CancelInvoke();
            Debug.Log("Ya ta");
        } 
        
    }

    // happens every 1.0 seconds
    void DoSomething()
    {
        patienceBool = true;
        patience -= 0.00083f;
    }


    private void RestartValues()
    {


        runing = false;

        endedTask = false;
    }

    public void ChangeState()
    {
       
        if (copiaCola.Count <= 0)
            {
                Debug.Log("111");
                //state = State.GoingHome;
                addPos(new Vector3(-103f, 0f, -103f));
        }

        else if (state == State.GoingToRadiology)
        {
            if (patientDisease.stars == 1)
            {
                addPos(new Vector3(-103f,0f,-103f));
            }
            else
            {
                if(copiaCola.Count>0)
                {
                    radiologyController.searchPatient(this.gameObject);
                    Debug.Log("Delete");
                    copiaCola.Dequeue();

                }
                
                   

            }
            
        }
    }

    public void callCoroutine()
    {

        StartCoroutine(MoveToTarget());

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
                    runing = false;
                    endedTask = true;
                    onQueue = true;
                    transform.rotation = Quaternion.Euler(0f, 0f, 0f);

                    if(state == State.GoingToQueue)
                    {
                        state = State.WaitingToBeAttendedQueue;
                        waiting = true;
                    }
                    else if (state == State.GoingToRadiology || state == State.GoingToConsult || state == State.GoingToConsult)
                    {
                        state = State.WaitingForDoctor;
                    }
                        
                    yield break;

                }
                yield break;
            }

            yield return null;
        }
    }
}
