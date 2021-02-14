using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragBuildings : MonoBehaviour
{
    private Vector3 offset;
    private float zCoord;

    private Grid grid;

    Renderer renderer;

    bool placed = false;
    bool placeable;
    bool selection;

    Material originalMaterial;

    public Material[] materiales;
    private void Start()
    {
        renderer = this.transform.GetChild(0).GetComponent<Renderer>();
        originalMaterial = renderer.material;
    }
    void OnMouseDown()
    {
        if (!placed)
        {
            renderer.material = materiales[0];

            zCoord = Camera.main.WorldToScreenPoint(

            gameObject.transform.position).z;

            // Store offset = gameobject world pos - mouse world pos

            //offset = gameObject.transform.position - GetMouseWorldPos();
        }
        
    }

    private void Update()
    {
        if(Input.GetKey("up"))
        {
            Debug.Log("up");
            placed = true;
            renderer.material = originalMaterial;
        }
    }
    private Vector3 GetMouseWorldPos()
    {
        //(x,y)
        Vector3 mousePoint = Input.mousePosition;

        //z
        mousePoint.z = zCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    // Update is called once per frame
    void OnMouseDrag()
    {
        if (!placed)
        {
            int x, z;
            GetGridPos(GetMouseWorldPos(), out x, out z);

            Vector3 posicion;
            posicion = GetWorldPosition(x, z);

            transform.position = new Vector3(posicion.x, 0, posicion.z);
        }
    }


    public Vector3 GetWorldPosition(int x, int z)
    {
        return new Vector3(x, 0, z) * 10;
    }

    public void GetGridPos(Vector3 posicion, out int x, out int z)
    {
        x = Mathf.FloorToInt(posicion.x / 10);
        z = Mathf.FloorToInt(posicion.z / 10);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Building"))
        {
            renderer.material = materiales[2];
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Building"))
        {
            renderer.material = materiales[1];
        }
    }

}




