using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragBuildings : MonoBehaviour
{
    public bool placed = false;
    private Vector3 offset;
    private float zCoord;

    public GameObject prefab;
        
    private Grid grid;

    Renderer renderer;
    public Seleccionador seleccionador;

    bool isSelected = true;
    bool isColliding = false;

    public Material originalMaterial;

    float speed = 300f;

    public Material[] materiales;

    private Quaternion objectToRotate;

    static bool globalSelection = false;
    private void Start()
    {

        renderer = this.transform.GetComponent<Renderer>();
        //originalMaterial = renderer.material;
    }


    void OnMouseDown()
    {
        if (!isSelected && !globalSelection) 
        {
            isSelected = true;

        } 
        

        else 
        {
            if (!isColliding)
            {
                renderer.material = originalMaterial;
                isSelected = false;
            }
        }
    }

 /*   void OnMouseUp()
    {
        if (isSelected)
        {
            if (!isColliding)
            {
                renderer.material = originalMaterial;
                isSelected = false;
            }
            else
            {
                //renderer.material = materiales[2];
            }
        }

    }*/

    



    private void Update()
    {

        if  (Input.GetMouseButtonDown(1))
        {
            if(isSelected)
            {
                objectToRotate = this.transform.rotation * Quaternion.Euler(0, 90, 0);
            }
          
        }
        
        if (isColliding && isSelected)
        {
            renderer.material = materiales[2];
        }

        else if (!isColliding && isSelected) renderer.material = materiales[1];

        if (isSelected == true)
        {
            zCoord = Camera.main.WorldToScreenPoint(

            gameObject.transform.position).z;


            int x, z;
            GetGridPos(GetMouseWorldPos(), out x, out z);

            Vector3 posicion;
            posicion = GetWorldPosition(x, z);

            transform.position = new Vector3(posicion.x, 0, posicion.z);

        }

    }

    private void LateUpdate()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, objectToRotate, 70f * Time.deltaTime);
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
 


    public Vector3 GetWorldPosition(int x, int z)
    {
        return new Vector3(x, 0, z) * 10;
    }

    public void GetGridPos(Vector3 posicion, out int x, out int z)
    {
        x = Mathf.FloorToInt(posicion.x / 10);
        z = Mathf.FloorToInt(posicion.z / 10);
    }

    void OnCollisionStay(Collision col)
    {
        if ((col.gameObject.CompareTag("Building") && isSelected ))
        {
            isColliding = true;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if ((other.gameObject.CompareTag("Building") && isSelected))
        {
            isColliding = false;
        }
    }

}




