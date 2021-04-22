using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaySalaries : MonoBehaviour
{
    private int moneyToPaySalaries;

    public static PaySalaries Instance { get; private set; } // static singleton
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

    public void addSalary(int i)
    {
        moneyToPaySalaries += i;
    }

    public void restSalary(int i)
    {
        moneyToPaySalaries -= i;
    }

    public void paySalaries()
    {
        //GlobalVariables.MONEY -= moneyToPaySalaries;
    }
}
