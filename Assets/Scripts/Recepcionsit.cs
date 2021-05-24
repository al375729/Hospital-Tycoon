using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Recepcionsit : MonoBehaviour
{
    private bool sub_task1 = false;

    public State state = State.WaitingForTask;

    private bool runing = false;

    private NavMeshAgent agent;

    public GameObject mancha;

    private bool endedTask = false;

    NavMeshAgent navMeshAgent;

    public ReceptionList reception;

    public bool onQueue = false;

    public int indexOfWindow;

    private CurrentTask currentTask = CurrentTask.nullTask;

    ConsultController consultController;

    WatingRoom waitingRoom;

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
        Working,
    }

    private float maxWaitingTime = 1f;
    private float waitingTime = 1f;

    private Vector3 comporbation = new Vector3(-66f, 321f, 987f);

    private Vector3 target;

    Transform patient;
    int workToChange = -1;

    private void Start()
    {

        consultController = ConsultController.Instance;
        waitingRoom = WatingRoom.Instance;

        navMeshAgent = this.GetComponent<NavMeshAgent>();
        state = State.WaitingForTask;

        agent = this.GetComponent<NavMeshAgent>();

        reception = ReceptionList.Instance;

        target = comporbation;

        //reception.searchPlace(this.gameObject);

        this.GetComponent<Worker>().role = "Recepcionist";
        DoctorInfo.setRole("Recepcionist");

    }

    private void Update()
    {

        if (this.gameObject.GetComponent<Worker>().isWorking() && target != comporbation && agent.remainingDistance >= 2f)
        {

            transform.LookAt(target);
        }

        if (this.gameObject.GetComponent<Worker>().isWorking() && state == State.WaitingForTask)
        {
            reception.searchPlace(this.gameObject);

        }

        if (endedTask)
        {
            RestartValues();
        }

        if(state == State.Working)
        {
            waitingTime -= Time.deltaTime;

            if (waitingTime <= 0)
            {
                waitingTime = maxWaitingTime;
                attendWindow();
            }
        }
    }

    private void attendWindow()
    {
       Transform attend = reception.attendWindow(indexOfWindow);

        if (attend.childCount > 0 && attend.GetChild(0).GetComponent<Patient>().state == Patient.State.WaitingToBeAttendedQueue)
        {
            attend.GetChild(0).GetComponent<Patient>().state = Patient.State.GettinAttended;
            PatientInfo.DisplayState(attend.GetChild(0).gameObject);

            patient = attend.GetChild(0);
            StartCoroutine(DoWork());


        }
        //else;
    }

    public void goTo(Transform target)
    {
        currentTask = CurrentTask.task1;
        agent = this.gameObject.GetComponent<NavMeshAgent>();
        this.target = target.position;
        StartCoroutine(MoveToWork());

    }

    private void RestartValues()
    {

        currentTask = CurrentTask.nullTask;
        sub_task1 = false;

        runing = false;

        target = comporbation;
        endedTask = false;
    }



    IEnumerator MoveToWork()
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
                    sub_task1 = true;
                    runing = false;
                    endedTask = true;
                    onQueue = true;
                    transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                    state = State.Working;
                    yield break;

                }
                yield break;
            }

            yield return null;
        }
    }

    public void changeJob(int i)
    {
        workToChange = i;
        if (i != 0)
        {
            if (gameObject.GetComponent<Worker>().state == Worker.State.Working)
            {
                Debug.Log("Luego cambio que estoy trabajando");
                workToChange = i;
                gameObject.GetComponent<Worker>().waitingToChanheJob = true;
            }


            else
            {
                change(workToChange);
            }
        }
        else Debug.Log("Ya eres consulta");

    }

    public void change(int i)
    {
        switch (workToChange)
        {
            case 0:
                this.gameObject.AddComponent<Recepcionsit>();
                Destroy(this.gameObject.GetComponent<Recepcionsit>());
                gameObject.GetComponent<Worker>().state = Worker.State.WaitingForTask;
                Debug.Log("Imposible");
                break;

            case 2:
                Debug.Log("Cambiado a radiologia");
                this.gameObject.AddComponent<Radiologist>();
                Destroy(this.gameObject.GetComponent<Recepcionsit>());
                gameObject.GetComponent<Worker>().state = Worker.State.WaitingForTask;
                break;

            case 1:
                Debug.Log("Cambiado a consulta");
                this.gameObject.AddComponent<Consult>();
                Destroy(this.gameObject.GetComponent<Recepcionsit>());
                gameObject.GetComponent<Worker>().state = Worker.State.WaitingForTask;
                break;

            case 3:
                Debug.Log("Cambiado a analista");
                this.gameObject.AddComponent<Analist>();
                Destroy(this.gameObject.GetComponent<Recepcionsit>());
                gameObject.GetComponent<Worker>().state = Worker.State.WaitingForTask;
                break;

            default:
                break;
        }
    }


    IEnumerator DoWork()
    {
        Debug.Log("Workinh");
        gameObject.GetComponent<Worker>().state = Worker.State.Working;

        patient.gameObject.GetComponent<Patient>().waiting = false;
        patient.gameObject.GetComponent<Patient>().state = Patient.State.GettinAttended;
        PatientInfo.DisplayState(patient.gameObject);
        float workingTime = 1;//- this.gameObject.GetComponent<Worker>().treatingSpeedBonus;
        yield return new WaitForSeconds(10);
        

        if (gameObject.GetComponent<Worker>().waitingToChanheJob)
        {
            gameObject.GetComponent<Worker>().waitingToChanheJob = false;
            change(workToChange);

        }
        consultController.searchPatient(patient.gameObject);
        waitingRoom.receptionEmpty(indexOfWindow);

        yield break;
    }


}
