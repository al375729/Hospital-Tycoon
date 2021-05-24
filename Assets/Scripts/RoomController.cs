using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void checkAll()
    {
        foreach (Transform child in transform)
        {
            this.gameObject.transform.tag = "Room";
            child.tag = "Room";
            child.gameObject.layer = 8;
            Destroy(child.GetComponent<CheckCollidersRoom>());
        }
    }
}
