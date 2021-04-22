using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceptionList : MonoBehaviour
{
    public static ReceptionList Instance { get; private set; } // static singleton

    public Transform[] seats;
    public Transform[] windows;

    void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void searchPlace(GameObject patient)
    {
        for (int i = 0; i < seats.Length; i++) 
        {
            if (seats[i].childCount == 0 && seats[i].GetComponent<Reception>().isActive)
            {
                patient.GetComponent<Recepcionsit>().goTo(seats[i]);
                patient.GetComponent<Recepcionsit>().indexOfWindow = i;
                patient.GetComponent<Recepcionsit>().state = Recepcionsit.State.DoingTask;
                patient.transform.SetParent(seats[i]);

                this.gameObject.transform.parent.GetChild(this.gameObject.transform.parent.childCount - 2).GetComponent<RoomStatus>().workers = "";
                this.gameObject.transform.parent.GetChild(this.gameObject.transform.parent.childCount - 2).GetComponent<RoomStatus>().updateText();
                break;
            }
        }
    }

    public void receptionEmpty(GameObject receptionist)
    {
        receptionist.transform.SetParent(null);
    }

    public Transform attendWindow(int i)
    {
        return windows[i];
    }
}
