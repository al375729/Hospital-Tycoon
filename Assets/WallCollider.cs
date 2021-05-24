using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollider : MonoBehaviour
{

    public bool isColliding = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }/*

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall") 
        {
            isColliding = true;
            Debug.Log("entro");
        }
        


    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            isColliding = false;
            Debug.Log("salgo");
        }

    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            isColliding = true;
            Debug.Log("sigo");
        }

    }*/

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            isColliding = true;
            //Debug.Log("entroTRI");
        }



    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            isColliding = true;
            //Debug.Log("salgoTRI");
        }

    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            isColliding = false;
            //Debug.Log("sigoTRI");
        }

    }


}
