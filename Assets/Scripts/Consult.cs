using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Consult : MonoBehaviour
{
    private bool working = false;

    private bool sub_task1 = false;

    public State state = State.WaitingForTask;

    private bool runing = false;

    private NavMeshAgent agent;

    public GameObject mancha;

    private bool endedTask = false;

    NavMeshAgent navMeshAgent;

    public bool onQueue = false;

    public int indexOfWindow;

    private CurrentTask currentTask = CurrentTask.nullTask;

    ConsultController consultController;

    int workToChange = -1;

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


    private void Start()
    {

        consultController = ConsultController.Instance;

        navMeshAgent = this.GetComponent<NavMeshAgent>();
        state = State.WaitingForTask;

        agent = this.GetComponent<NavMeshAgent>();

        target = comporbation;

        this.GetComponent<Worker>().role = "Consult";
        DoctorInfo.setRole("Consult");

    }

    private void Update()
    {

        if (this.gameObject.GetComponent<Worker>().isWorking() && target != comporbation && agent.remainingDistance >= 2f)
        {

            transform.LookAt(target);
        }

        if (this.gameObject.GetComponent<Worker>().isWorking() && state == State.WaitingForTask)
        {
            consultController.searchDoctor(this.gameObject);

        }

        if (endedTask)
        {
            RestartValues();
        }

        if (state == State.Working)
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
        Transform attend = consultController.attend(indexOfWindow);
        if (attend.childCount > 0 && attend.GetChild(0).GetComponent<Patient>().state == Patient.State.WaitingForDoctor)
        {

            attend.GetChild(0).GetComponent<Patient>().state = Patient.State.GettinAttended;
            patient = attend.GetChild(0);
            StartCoroutine(DoWork());


        }
        //else Debug.Log("No hay nadie en ventanilla");
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
        if (i != 1)
        {
            if (gameObject.GetComponent<Worker>().state == Worker.State.Working)
            {
                Debug.Log("Luego cambio que estoy trabajando");
                gameObject.GetComponent<Worker>().waitingToChanheJob = true;
            }


            else
            {
                Debug.Log("Cambio");
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
                Destroy(this.gameObject.GetComponent<Consult>());
                gameObject.GetComponent<Worker>().state = Worker.State.WaitingForTask;
                Debug.Log("Cambiado a Recepcion");
                break;

            case 2:
                Debug.Log("Cambiado a Radiologo");
                this.gameObject.AddComponent<Radiologist>();
                Destroy(this.gameObject.GetComponent<Consult>());
                gameObject.GetComponent<Worker>().state = Worker.State.WaitingForTask;
                break;

            case 1:
                Debug.Log("Imposible");
                this.gameObject.AddComponent<Consult>();
                Destroy(this.gameObject.GetComponent<Consult>());
                gameObject.GetComponent<Worker>().state = Worker.State.WaitingForTask;
                break;

            case 3:
                Debug.Log("Cambiado a analista");
                this.gameObject.AddComponent<Analist>();
                Destroy(this.gameObject.GetComponent<Consult>());
                gameObject.GetComponent<Worker>().state = Worker.State.WaitingForTask;
                break;

            default:
                break;
        }
    }

        IEnumerator DoWork()
        {
            gameObject.GetComponent<Worker>().state = Worker.State.Working;
            patient.gameObject.GetComponent<Patient>().state = Patient.State.GettingConsult;
            PatientInfo.DisplayState(patient.gameObject);

            yield return new WaitForSeconds(10);

        if (gameObject.GetComponent<Worker>().waitingToChanheJob) 
        {
            gameObject.GetComponent<Worker>().waitingToChanheJob = false;
            change(workToChange);

        } 

            Debug.Log("Tira para radiologia");
           consultController.arrayForPatients[indexOfWindow].GetChild(0).GetComponent<Patient>().state = Patient.State.GoingToRadiology;
           consultController.arrayForPatients[indexOfWindow].GetChild(0).GetComponent<Patient>().ChangeState();

    
        }
}