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
    public ConsultController consultController;
    public AnalisisController analisisController;
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

    private bool end = false;

    private bool atHome = false;
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

         ran = Random.Range(88, 90);

        if (ran < 60)
        {
            stars = 1;
            int ran2 = Random.Range(0, Diseases.diseasesLevel1.Count);
            patientDisease = Diseases.GetDiseaseLevel1(ran2);
        }
        else if (ran>60 && ran < 85)
        {
            stars = 2;
            int ran2 = Random.Range(0, Diseases.diseasesLevel2.Count);
            patientDisease = Diseases.GetDiseaseLevel2(ran2);
        }
        else
        {
            stars = 3;
            int ran2 = Random.Range(0, Diseases.diseasesLevel3.Count);
            patientDisease = Diseases.GetDiseaseLevel3(ran2);
        }

        copiaCola = new Queue<string>();

        foreach (string item in patientDisease.tasks)
        {
            copiaCola.Enqueue(item);
        }



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
        analisisController = AnalisisController.Instance;
        consultController = ConsultController.Instance;

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

        if(!end)
        {
            if (waiting && patience > 0 && !patienceBool) InvokeRepeating("DoSomething",0f,1.0f);
            else if (!waiting)
            {
                CancelInvoke();
                patienceBool = false;
            }

            if (patience <= 0 && patience!=-5) endHome();
        }
        
        
    }

    // happens every 1.0 seconds
    void DoSomething()
    {
        patienceBool = true;
        patience -= 0.00833333333f;
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
            returnHome();
        }

        else
        {
            if (patientDisease.stars == 1)
            {
                returnHome();
            }
            else
            {
                if(copiaCola.Count>0 && copiaCola.Peek()=="Radiologia")
                {
                    radiologyController.searchPatient(this.gameObject);
                    copiaCola.Dequeue();

                }
                else if (copiaCola.Count > 0 && copiaCola.Peek() == "Analisis")
                {
                    analisisController.searchPatient(this.gameObject);
                    copiaCola.Dequeue();

                }



            }
            
        }

    }
    private void endHome()
    {
        patience = -5;
        if (state != State.WaitingForDoctor || state != State.GoingToAnalysis || state != State.GettingRadiology || state != State.GoingToConsult || state != State.GoingToReception) DisplayStatistics.changeNumberOfPatientsWaiting(-1);

        switch (state)
        {
            case State.WaitingForConsult:
                DisplayStatistics.changeNumberOfPatientsWaitingConsultation(-1);
                consultController.attendancePriorityConsult.Dequeue();
                break;

            case State.WaitingForAnalysis:
                DisplayStatistics.changeNumberOfPatientsWaitingAnalysis(-1);
                analisisController.attendancePriorityAnalisis.Dequeue();
                break;

            case State.WaitingForRadiology:
                DisplayStatistics.changeNumberOfPatientsWaitingRadiology(-1);
                radiologyController.attendancePriorityRadiology.Dequeue();
                break;

            case State.WaitingToBeAttended:
                WatingRoom.attendancePriority.Dequeue();
                DisplayStatistics.changeNumberOfPatientsWaitingReception(-1);
                break;
        }

        waiting = false;
        state = State.GoingHome;

        //end = true;

        DisplayStatistics.notTreateadPatients += 1;

        addPos(new Vector3(-102f, 0.1f, -107f));

        if (stars == 1)
        {
            GlobalVariables.MONEY -= 100;
            DisplayStatistics.notTreatedLoses -= 100;
            GlobalVariables.MONTH_EXPENSES += 100;
        }
        else if (stars == 2)
        {
            GlobalVariables.MONEY -= 200;
            DisplayStatistics.notTreatedLoses -= 200;
            GlobalVariables.MONTH_EXPENSES += 200;
        }
        else if (stars == 3)
        {
            GlobalVariables.MONEY -= 300;
            DisplayStatistics.notTreatedLoses -= 300;
            GlobalVariables.MONTH_EXPENSES += 300;
        }
        


        


        DisplayStatistics.updateTextStatistics();
    }

    private void returnHome()
    {
        //end = true;
        waiting = false;
        state = State.GoingHome;
        DisplayStatistics.treateadPatients += 1;
        addPos(new Vector3(-102f, 0.1f, -107f));

        if (stars == 1)
        {
            GlobalVariables.MONEY += 100;
            DisplayStatistics.treatedIncome += 100;
            GlobalVariables.MONTH_INCOMES += 100;
        }
        else if (stars == 2)
        {
            GlobalVariables.MONEY += 200;
            DisplayStatistics.treatedIncome += 200;
            GlobalVariables.MONTH_INCOMES += 200;
        }
        else if (stars == 3)
        {
            GlobalVariables.MONEY += 300;
            DisplayStatistics.treatedIncome += 300;
            GlobalVariables.MONTH_INCOMES += 300;
        }
        DisplayStatistics.updateTextStatistics();
    }

    public void callCoroutine()
    {

        StartCoroutine(MoveToTarget());

    }



    IEnumerator MoveToTarget()
    {
        bool end = false;
        target = new Vector3(target.x, this.transform.position.y, target.z);
        agent.destination = target;
        while (!end)
        {
            if (!atHome && agent.remainingDistance <= 0.1f && agent.pathPending == false)
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
                    else if (state == State.GoingToRadiology || state == State.GoingToAnalysis || state == State.GoingToConsult)
                    {
                        state = State.WaitingForDoctor;
                        waiting = true;
                    }
                    else if (state == State.GoingHome)
                    {
                        atHome = true;
                        print("Me voy");
                        waiting = false;
                        PatientInfo.check(this.gameObject.name);
                        Destroy(this.gameObject.transform.GetComponent<Patient>());
                        Destroy(this.gameObject);

                    }


                    yield break;

                }
                yield break;
            }

            yield return null;
        }
    }
}
