using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Worker : MonoBehaviour
{
    public string workerName;

    private string type;

    public int stars;

    private bool working = false;

    private bool sub_task1 = false;

    public State state = State.WaitingForTask;

    public currentState currentstate = currentState.GoingToRestRoom;

    private bool runing = false;

    public GameObject mancha;

    private bool endedTask = false;

    private NavMeshAgent agent;

    public bool onQueue = false;

    public int indexOfWindow;

    private CurrentTask currentTask = CurrentTask.nullTask;

    ConsultController consultController;

    private TaskManagement.PatientGoTo task;

    RestRoom restRoom;

    public int salary;

    public string role;

    public int walkingSpeedBonus;
    public int treatingSpeedBonus;
    public int moneyBonus;

    public bool waitingToChanheJob;
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
        WaitingToChangeTask,
        DoingTask,
        Working,
        None
    }

    public enum currentState
    {
        GoingToRestRoom,
        Resting,
        GoingToConsult,
        DoingConsult
    }

    private float maxWaitingTime = 1f;
    private float waitingTime = 1f;

    private Vector3 comporbation = new Vector3(-66f, 321f, 987f);

    private Vector3 target;

    Transform patient;
    public string gender;

    public Sprite sprite;
    private void Start()
    {

        consultController = ConsultController.Instance;

        state = State.WaitingForTask;

        target = comporbation;

        restRoom = RestRoom.Instance;

    }
    public void setType(string s)
    {
        type = s;
    }

    public string getType()
    {
        return type;
    }

    public void setWorking(bool b)
    {
        working = b;
    }

    public bool isWorking()
    {
        return working;
    }
    void Awake()
    {
        int random = Random.Range(1, 100);

        if (random <= 64) stars = 1;
        else if (random > 64 && random <=84) stars = 2;
        else if (random > 84 && random <= 94) stars = 3;

        else if (random > 94 && random <= 99) stars = 4;
        else if (random >= 100) stars = 5;


    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.GetComponent<Worker>().isWorking() && target != comporbation && agent.remainingDistance >= 2f)
        {

            transform.LookAt(target);
        }

        if (endedTask)
        {
            state = State.None;
            RestartValues();
        }

        if(state == State.None)
        {
            //if (this.gameObject.GetComponent<Consult>() != null) Debug.Log("111234");


        }
    }

    public void addTask(TaskManagement.PatientGoTo task1)
    {
        agent = this.GetComponent<NavMeshAgent>();
        RestartValues();

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

    public void callCoroutine()
    {

        StartCoroutine(MoveToTarget());

    }

    public void goTo(Transform target)
    {
        currentTask = CurrentTask.task1;
        agent = this.gameObject.GetComponent<NavMeshAgent>();
        this.target = target.position;
        StartCoroutine(MoveToTarget());

    }

    private void RestartValues()
    {
        //agent.ResetPath();

        currentTask = CurrentTask.nullTask;
        sub_task1 = false;

        runing = false;

        target = comporbation;
        endedTask = false;
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

    public void goToRestRoom(GameObject patient)
    {
        restRoom.searchPlace(patient);
    }
}
