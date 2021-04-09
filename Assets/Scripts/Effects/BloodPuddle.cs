using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodPuddle : MonoBehaviour
{
    private TaskManagement taskManagement;
    private TaskManagement.TaskClean task;
    void Start()
    {
        taskManagement = TaskManagement.Instance;

        taskManagement.AddTaskCleanStain(this.transform.position,this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //if(this.gameObject.GetComponent<SpriteRenderer>().color.a == 0) Destroy(this.gameObject);
    }
}
