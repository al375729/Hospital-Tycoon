using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;

public class DragBuildings : MonoBehaviour
{
    public bool placed = false;
    private float zCoord;

    public GameObject prefab;

    private Grid grid;

    public bool isSelected = true;
    bool isColliding = false;

    public Material originalMaterial;

    public Material[] materiales;

    private Quaternion objectToRotate;

    public static bool globalSelection = false;
    private Vector3 position;
    private Quaternion rotation;

    private bool lastFrameWasEditMode;

    private bool addedReferences = false;

    ConsultController consultController;
    RadiologyController radiologyController;
    AnalisisController analisisController;
    private enum State
    {
        WaitingForTask,
        DoingTask,
        DoingTaskClean,
    }
    private void Start()
    {
        consultController = ConsultController.Instance;
        radiologyController = RadiologyController.Instance;
        analisisController = AnalisisController.Instance;

        transform.GetChild(transform.childCount - 2).gameObject.GetComponent<MeshRenderer>().enabled = false;
        this.gameObject.transform.GetChild(gameObject.transform.childCount - 2).GetComponent<RoomStatus>().workers = "THERE ARE NOT WORKERS ASSIGNED" + "\n" + "\n";
        
        //this.gameObject.transform.GetChild(gameObject.transform.childCount - 2).GetComponent<RoomStatus>().updateText();
    }


    void OnMouseDown()
    {
        if (!IsMouseOverUI())
        {
            if (!isSelected && !globalSelection && GlobalVariables.EDIT_MODE)
            {
                if (this.gameObject.name == "Recepcion")
                {
                    Console.setText("You can't move the reception");
                }
                
                else
                {
                    if(this.gameObject.transform.GetChild(1).transform.childCount!=0 || this.gameObject.transform.GetChild(2).transform.transform.childCount != 0)
                    {
                        Console.setText("You can't move the room because there is someone on it");

                    }
                    else
                    {
                        isSelected = true;
                        globalSelection = true;
                        position = transform.position;
                        rotation = transform.rotation;

                        if (addedReferences)
                        {
                            deleteReferences();
                            addedReferences = false;
                        }
                    }

                }

               

            }
            else if (!isSelected && !globalSelection && GlobalVariables.DELETE_MODE)
            {
                if (this.gameObject.name == "Recepcion")
                {
                    Console.setText("You can't delte the reception");
                }

                else
                {
                    if (this.gameObject.transform.GetChild(1).transform.childCount != 0 || this.gameObject.transform.GetChild(2).transform.transform.childCount != 0)
                    {
                        Console.setText("You can't delete the room because there is someone on it");

                    }
                    else
                    {
                        if (addedReferences)
                        {
                            deleteReferences();
                            addedReferences = false;
                        }
                        Destroy(this.gameObject);
                    }

                }


            }
            else
            {
                if (!isColliding && isSelected)
                {
                    if(this.gameObject.GetComponent<RoomComprobations>().isReachable())
                    {
                        addReferences();
                        addedReferences = true;
   
                        this.gameObject.transform.GetChild(this.gameObject.transform.childCount - 2).GetComponent<RoomStatus>().reachable = "";
                        this.gameObject.transform.GetChild(this.gameObject.transform.childCount - 2).GetComponent<RoomStatus>().updateText();
                    }
                    else
                    {
                        this.gameObject.transform.GetChild(this.gameObject.transform.childCount - 2).GetComponent<RoomStatus>().reachable = "THIS ROOM IS UNREACHABLE" + "\n" + "\n";
                        this.gameObject.transform.GetChild(this.gameObject.transform.childCount - 2).GetComponent<RoomStatus>().updateText();
                    }
                   
                    changeMaterialOfChildren(0);
                    position = transform.position;
                    rotation = transform.rotation;

                    int x, z;
                    GetGridPos(GetMouseWorldPos(), out x, out z);

                    Vector3 posicion;
                    posicion = GetWorldPosition(x, z);

                    Vector2 vec = GridController.gridToMatrix(x, z);
                    x = (int)vec.x;
                    z = (int)vec.y;
                    GridController.setPrefabRoom(x, z, this.gameObject);
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
        if (GlobalVariables.UI_OPEN)
        {
            changeMaterialOfChildren(0);
            isSelected = false;
            globalSelection = false;
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (isSelected)
            {
                objectToRotate = this.transform.rotation * Quaternion.Euler(0, -90, 0);
            }

        }

        if (isColliding && isSelected)
        {
            changeMaterialOfChildren(2);
        }

        else if (!isColliding && isSelected) changeMaterialOfChildren(1);

        if (isSelected == true)
        {
            zCoord = Camera.main.WorldToScreenPoint(

            gameObject.transform.position).z;


            int x, z;
            GetGridPos(GetMouseWorldPos(), out x, out z);
            Vector3 posicion;
            posicion = GetWorldPosition(x, z);
            /*
            Vector2 vec = GridController.gridToMatrix(x, z);
            x = (int)vec.x;
            z = (int)vec.y;

            Debug.Log(x + " , " + z);*/

            transform.position = new Vector3(posicion.x, 0, posicion.z);

        }



        //if(lastFrameWasEditMode != GlobalVariables.EDIT_MODE)
        //{
        if (GlobalVariables.EDIT_MODE && !isSelected && !isColliding)
        {
            transform.GetChild(0).gameObject.GetComponent<ObjectsOnRoom>().changeMaterial(0);
            this.gameObject.transform.GetChild(gameObject.transform.childCount - 2).GetComponent<RoomStatus>().updateText();
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
            transform.GetChild(0).gameObject.GetComponent<ObjectsOnRoom>().changeMaterial(3);
            transform.GetChild(transform.childCount - 2).gameObject.GetComponent<MeshRenderer>().enabled = false;
            transform.GetChild(transform.childCount - 1).gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }

        lastFrameWasEditMode = GlobalVariables.EDIT_MODE;
        //}

    }

    private void changeMaterialOfChildren(int index)
    {
        //transform.GetComponent<MeshRenderer>().material = material;
        for (int i = 0; i < transform.childCount - 2; i++)
        {
            if (transform.GetChild(i).GetComponent<ObjectsOnRoom>() != null)
            {
                transform.GetChild(i).GetComponent<ObjectsOnRoom>().changeMaterial(index);
            }
            else
            {
                for (int j = 0; j < transform.GetChild(i).childCount; j++)
                {
                    if (transform.GetChild(i).GetChild(j).GetComponent<ObjectsOnRoom>() != null) transform.GetChild(i).GetChild(j).GetComponent<ObjectsOnRoom>().changeMaterial(index);
                }


            }

        }


    }

    private void LateUpdate()
    {
        if (!IsQuaternionInvalid(transform.rotation) && !IsQuaternionInvalid(objectToRotate))
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
        return new Vector3(x, 0, z) * 5;
    }

    public void GetGridPos(Vector3 posicion, out int x, out int z)
    {
        x = Mathf.FloorToInt(posicion.x / 5);
        z = Mathf.FloorToInt(posicion.z / 5);
    }


    void OnCollisionStay(Collision col)
    {
        if ((col.gameObject.CompareTag("Building") && isSelected))
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


    public void addReferences()
    {

        for (int i = 0; i < transform.childCount - 2; i++)
        {
            if (transform.GetChild(i).GetComponent<ObjectsOnRoom>() != null)
            {
                ObjectsOnRoom obj = transform.GetChild(i).GetComponent<ObjectsOnRoom>();

                int index;

                switch (obj.objectType)
                {
                    case ObjectsOnRoom.type.ConsultDoctor:
                        index = consultController.addDoctor(transform.GetChild(i).transform);
                        obj.indexInList = index;
                        break;

                    case ObjectsOnRoom.type.ConsultPatient:
                        index = consultController.addPatient(transform.GetChild(i).transform);
                        obj.indexInList = index;
                        break;

                    case ObjectsOnRoom.type.None:
                        break;

                    case ObjectsOnRoom.type.RadiologyDoctor:
                        index = radiologyController.addDoctor(transform.GetChild(i).transform);
                        obj.indexInList = index;
                        break;

                    case ObjectsOnRoom.type.RadiologyPatient:
                        index = radiologyController.addPatient(transform.GetChild(i).transform);
                        obj.indexInList = index;
                        break;

                    case ObjectsOnRoom.type.AnalysisDoctor:
                        index = analisisController.addDoctor(transform.GetChild(i).transform);
                        obj.indexInList = index;
                        break;

                    case ObjectsOnRoom.type.AnalysisPatient:
                        index = analisisController.addPatient(transform.GetChild(i).transform);
                        obj.indexInList = index;
                        break;

                }
            }
        }
    }

    public void deleteReferences()
    {
        for (int i = 0; i < transform.childCount - 2; i++)
        {
            if (transform.GetChild(i).GetComponent<ObjectsOnRoom>() != null)
            {
                ObjectsOnRoom obj = transform.GetChild(i).GetComponent<ObjectsOnRoom>();

                switch (obj.objectType)
                {
                    case ObjectsOnRoom.type.ConsultDoctor:
                        consultController.updateIndexOfDoctors(obj.indexInList);
                        break;

                    case ObjectsOnRoom.type.ConsultPatient:
                        consultController.updateIndexOfPatients(obj.indexInList);
                        break;

                    case ObjectsOnRoom.type.None:
                        break;
                }
            }
        }

    }






}




