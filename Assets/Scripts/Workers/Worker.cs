using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : MonoBehaviour
{
    public string workerName;

    private string type;

    public int stars;

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
        
    }
}
