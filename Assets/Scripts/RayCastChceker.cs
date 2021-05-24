using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastChceker : MonoBehaviour
{
    public Material material;

    public static RayCastChceker Instance { get; private set; } // static singleton

    public GameObject hitObject;
    public float maxDist;
    public float sphereRadius;

    private Vector3 origin;
    private Vector3 direction;
    public LayerMask layer;

    private float currentDistance;

    public static GameObject detect;

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
        origin = transform.position;
        direction = transform.forward;

        RaycastHit hit;

        if (Physics.SphereCast(origin, sphereRadius, direction, out hit, maxDist, layer))
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
        Gizmos.DrawWireSphere(origin + direction * currentDistance, sphereRadius);
    }

    public GameObject detectUnder(Vector3 pos)
    {
        origin = transform.position;
        direction = transform.forward;

        RaycastHit hit;

        if (Physics.SphereCast(origin, sphereRadius, direction, out hit, maxDist, layer))
        {
            hitObject = hit.transform.gameObject;
            currentDistance = hit.distance;
        }
        else
        {
            currentDistance = maxDist;
            hitObject = null;
        }

        this.gameObject.transform.position = pos;
        //transform.position = new Vector3(-600f, 10, 0f);
        if (hitObject != null) return hitObject;
        else return null;

       

    }
}
