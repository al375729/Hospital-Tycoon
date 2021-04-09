using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : MonoBehaviour
{
    public string workerName;

    private string type;

    private bool working = false;
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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
