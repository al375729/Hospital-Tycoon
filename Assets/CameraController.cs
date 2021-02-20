using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    public Transform camera;
    public Transform objectToFollow;

    public float movementSpeed;
    public float speed;
    public float normalSpeed;
    public float fastSpeed;
    public float time;
    public float rotation;


    public Vector3 zoom;

    

    public Vector3 newPosition;
    public Quaternion newRotation;
    public Vector3 newZoom;

    public Vector3 dragStartPos;
    public Vector3 dragCurrentPos;

    public Vector3 rotateStartPos;
    public Vector3 rotateCurrenttPos;

    void Start()
    {
        instance = this;
        newPosition = transform.position;
        newRotation = transform.rotation;
        newZoom = camera.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(objectToFollow != null)
        {
            transform.position = objectToFollow.position;

        }
        else 
        {
            HandlePlayerKeyboardInput();
            HandlePlayerMouseInput();
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            objectToFollow = null;
        }
        
    }

    private void HandlePlayerMouseInput()
    {
        if (!IsMouseOverUI())
        {
            if(Input.GetMouseButtonDown(0) && !DragBuildings.globalSelection )
            {
                Plane plane = new Plane(Vector3.up, Vector3.zero);

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                float hitPoint;

                if(plane.Raycast(ray , out hitPoint))
                {
                    dragStartPos = ray.GetPoint(hitPoint);
                }
            }

            if (Input.GetMouseButton(0) && !DragBuildings.globalSelection)
            {
                Plane plane = new Plane(Vector3.up, Vector3.zero);

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                float hitPoint;

                if (plane.Raycast(ray, out hitPoint))
                {
                    dragCurrentPos = ray.GetPoint(hitPoint);

                    newPosition = transform.position + dragStartPos - dragCurrentPos;
                }
            }

            if (Input.mouseScrollDelta.y != 0) 
            {
                newZoom += Input.mouseScrollDelta.y * zoom;
            }

            if (Input.GetMouseButtonDown(2) && !DragBuildings.globalSelection)
            {
                rotateStartPos = Input.mousePosition;
            }

            if (Input.GetMouseButton(2) && !DragBuildings.globalSelection)
            {
                rotateCurrenttPos = Input.mousePosition;

                Vector3 rotation = rotateStartPos - rotateCurrenttPos;

                rotateStartPos = rotateCurrenttPos;

                newRotation *= Quaternion.Euler(Vector3.up * (rotation.x / 5f));
            }

        }
    }
    void HandlePlayerKeyboardInput()
    {
        if (!IsMouseOverUI())
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = fastSpeed;
            }
            else 
            {
                speed = normalSpeed;
            }

            if(Input.GetKey(KeyCode.UpArrow)  || Input.GetKey(KeyCode.W))
            {
                newPosition += transform.forward * speed;
            }

            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                newPosition += transform.forward * -speed;
            }

            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                newPosition += transform.right * speed;
            }

            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                newPosition += transform.right * -speed;
            }

            if (Input.GetKey(KeyCode.Q) )
            {
                newRotation *= Quaternion.Euler(Vector3.up * -rotation);
            }

            if (Input.GetKey(KeyCode.E))
            {
                newRotation *= Quaternion.Euler(Vector3.up * rotation);
            }

            if (Input.GetKey(KeyCode.R))
            {
                newZoom += zoom;
            }
            
            if (Input.GetKey(KeyCode.T))
            {
                newZoom -= zoom;
            }

            

            transform.position = Vector3.Lerp(transform.position, newPosition, time * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, time * Time.deltaTime);
            camera.localPosition = Vector3.Lerp(camera.transform.localPosition, newZoom, time * Time.deltaTime);
        }
    }

    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
