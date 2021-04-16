using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Radiologist : MonoBehaviour
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

    RadiologyController radiologyController;



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

        radiologyController = RadiologyController.Instance;

        navMeshAgent = this.GetComponent<NavMeshAgent>();
        state = State.WaitingForTask;

        agent = this.GetComponent<NavMeshAgent>();

        target = comporbation;

    }

    private void Update()
    {

        if (this.gameObject.GetComponent<Worker>().isWorking() && target != comporbation && agent.remainingDistance >= 2f)
        {

            transform.LookAt(target);
        }

        if (this.gameObject.GetComponent<Worker>().isWorking() && state == State.WaitingForTask)
        {
            radiologyController.searchDoctor(this.gameObject);

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
        Transform attend = radiologyController.attend(indexOfWindow);
        if (attend.childCount > 0 && attend.GetChild(0).GetComponent<Patient>().state == Patient.State.WaitingForTask)
        {
            Debug.Log("atendiendo a:" + attend.GetChild(0).GetComponent<Patient>().name);
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
                    Debug.Log("Fin de la tarea 1");
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



    IEnumerator DoWork()
    {
        yield return new WaitForSeconds(3);
        radiologyController.arrayForPatients[indexOfWindow].GetChild(0).GetComponent<Patient>().state = Patient.State.GoingToRadiology;
        radiologyController.arrayForPatients[indexOfWindow].GetChild(0).GetComponent<Patient>().ChangeState();

    }
}
