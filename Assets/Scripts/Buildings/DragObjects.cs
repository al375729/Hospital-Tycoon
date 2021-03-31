using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class DragObjects : MonoBehaviour
{
    public bool placed = false;
    private float zCoord;

    public GameObject prefab;
        
    private Grid grid;

    public bool isSelected = true;
    bool isColliding = false;

    private Material originalMaterial;

    public Material[] materiales;

    private Quaternion objectToRotate;

    public static bool globalSelection = false;
    private Vector3 position;
    private Quaternion rotation;
    private void Start()
    {

    }


    void OnMouseDown()
    {
        if(! IsMouseOverUI())
        {
            if (!isSelected && !globalSelection && GlobalVariables.EDIT_MODE)
            {
                isSelected = true;
                globalSelection = true;
                position = transform.position;
                rotation = transform.rotation;

            }
            else if (!isSelected && !globalSelection && GlobalVariables.DELETE_MODE)
            {
                Destroy(this.gameObject);

            }
            else
            {
                if (!isColliding)
                {
                    changeMaterialOfChildren(materiales[0]);
                    position = transform.position;
                    rotation = transform.rotation;
                    isSelected = false;
                    globalSelection = false;
                }
            }
        }
        
    }

    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
    private void Update()
    {
        if(GlobalVariables.UI_OPEN)
        {
            transform.position = position;
            transform.rotation = rotation;
            changeMaterialOfChildren(materiales[0]);
            isSelected = false;
            globalSelection = false;
        }
        if  (Input.GetMouseButtonDown(1))
        {
            if(isSelected)
            {
                objectToRotate = this.transform.rotation * Quaternion.Euler(0, -90, 0);
            }
          
        }

        if (isColliding && isSelected)
        {
            changeMaterialOfChildren(materiales[2]);
        }

        else if (!isColliding && isSelected) changeMaterialOfChildren(materiales[1]);

        if (isSelected == true)
        {
            zCoord = Camera.main.WorldToScreenPoint(

            gameObject.transform.position).z;


            int x, z;
            GetGridPos(GetMouseWorldPos(), out x, out z);

            Vector3 posicion;
            posicion = GetWorldPosition(x, z);

            transform.position = new Vector3(posicion.x + 5f, 0, posicion.z + 5f);

        }

        if (GlobalVariables.EDIT_MODE && !isSelected && !isColliding)
        {
            transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = materiales[3];
            transform.GetChild(transform.childCount - 2).gameObject.GetComponent<MeshRenderer>().enabled = true;
            transform.GetChild(transform.childCount - 1).gameObject.GetComponent<SpriteRenderer>().enabled = true;

        }
        else if (GlobalVariables.EDIT_MODE && isSelected)
        {
            transform.GetChild(transform.childCount - 2).gameObject.GetComponent<MeshRenderer>().enabled = false;
            transform.GetChild(transform.childCount - 1).gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        else if (!GlobalVariables.EDIT_MODE && !isSelected && !isColliding)
        {
            transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = materiales[0];
            transform.GetChild(transform.childCount - 2).gameObject.GetComponent<MeshRenderer>().enabled = false;
            transform.GetChild(transform.childCount - 1).gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }

    }

    private void changeMaterialOfChildren(Material material)
    {
        transform.GetComponent<MeshRenderer>().material = material;
        for (int i = 0; i < transform.childCount-2; i++)
        {
            Renderer rend = transform.GetChild(i).GetComponent<MeshRenderer>();
            rend.material = material;
        }

    }

    private void LateUpdate()
    {
        if(!IsQuaternionInvalid(transform.rotation) && !IsQuaternionInvalid(objectToRotate))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, objectToRotate, 70f * Time.deltaTime);
        } 
    }

    private bool IsQuaternionInvalid(Quaternion q)
    {
        bool check = q.x == 0f;
        check &= q.y == 0;
        check &= q.z == 0;
        check &= q.w == 0;

        return check;
    }
    private Vector3 GetMouseWorldPos()
    {
        //(x,y)
        Vector3 mousePoint = Input.mousePosition;

        //z
        mousePoint.z = zCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
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




