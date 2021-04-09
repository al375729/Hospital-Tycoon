using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsultController : MonoBehaviour
{
    public static ConsultController Instance { get; private set; } // static singleton

    public List<Transform> seats;
    public List<Transform> queue;
    int a = 1;

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
        if (Input.GetKeyDown("space"))
        {

            foreach (Transform item in seats)
            {
                Debug.Log(item.transform.gameObject + "Mi posicion es :" + item.position);
            }
        }
    }

    public void updateIndexQueue(int index)
    {
        queue.RemoveAt(index);

        for (int i = index; i < queue.Count; i++)
        {
            queue[i].GetComponent<ObjectsOnRoom>().indexInList--;
        }
    }

    public void updateIndexSeat(int index)
    {
        seats.RemoveAt(index);

        for (int i = index; i < seats.Count; i++)
        {
            seats[i].GetComponent<ObjectsOnRoom>().indexInList--;
        }
    }

    public int addSeats(Transform position)
    {
        seats.Add(position);
        return seats.Count-1;
    }

    public int addQueue(Transform position)
    {
        queue.Add(position);
        return queue.Count-1;
    }
}
