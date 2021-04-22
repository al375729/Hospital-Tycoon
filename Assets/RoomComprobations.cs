using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RoomComprobations : MonoBehaviour
{
    public GameObject dummy;
    public GameObject target;

    NavMeshAgent agente;

    void Start()
    {
        dummy = Instantiate(dummy, new Vector3(-10f, 0f, -105f), Quaternion.identity);
        agente = dummy.AddComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool isReachable()
    {
        NavMeshPath path = new NavMeshPath();
        agente.CalculatePath(target.transform.position, path);
        if (path.status == NavMeshPathStatus.PathPartial)
        {
            Debug.Log("No Hay camino");
            return false;

        }
        else Debug.Log(" Hay camino"); return true;
    }
}
