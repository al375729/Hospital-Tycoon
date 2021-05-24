using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollidersRoom : MonoBehaviour
{
    public Material material;

    public GameObject hitObject;
    public float maxDist;
    public float sphereRadius;

    private Vector3 origin;
    private Vector3 direction;

    private float currentDistance;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        origin = transform.position;
        direction = transform.forward;

        RaycastHit hit;

        if (Physics.SphereCast(origin, sphereRadius, direction, out hit, maxDist))
        {
            hitObject = hit.transform.gameObject;
            currentDistance = hit.distance;
        }
        else
        {
            currentDistance = maxDist;
            hitObject = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(origin, origin + direction * currentDistance);
        Gizmos.DrawWireSphere(origin + direction * currentDistance,sphereRadius);
    }
    void OnCollisionEnter(Collision collision)
    {

    }

    void OnCollisionExit(Collision collision)
    {
        
    }

    private void setAllRed()
    {
        DrawSquare.allowed = false;
        Transform parent = this.gameObject.transform.parent;

        foreach (Transform child in transform)
        {
            child.GetComponent<MeshRenderer>().material = material;
        }
    }
}
