using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GridController : MonoBehaviour
{
    public static GameObject[,] casillas;
    public LayerMask IgnoreMe;

    void Start()
    {
        casillas = new GameObject[40, 40];

        for (int i = 0; i < 40; i++)
        {
            for (int j = 0; j < 40; j++)
            {
                casillas[i, j] = null;
            }
        }
    }

    public static bool checkgrid(int x,int y)
    {
        if (casillas[x, y] != null) return true;
        else return false;
    }

    public static void setgrid(int x, int y,GameObject obj)
    {
        casillas[x, y] = obj;
    }

    public static GameObject getgrid(int x, int y)
    {
        if (casillas[x, y] != null) return casillas[x, y];
        else return null;

    }

    public static Vector2 gridToMatrix(int x, int y)
    {
        int xValue, yValue;

        xValue = x + 20;
        yValue = 19 - y;

        Vector2 matrix = new Vector2(xValue, yValue);
        return matrix;
    }

    public static void setPrefabRoom(int x,int y,GameObject obj)
    {
        casillas[x - 2, y - 1] = obj;
        casillas[x - 2, y ] = obj;
        casillas[x - 2, y +1] = obj;
        casillas[x - 2, y  +2] = obj;

        casillas[x - 1, y - 1] = obj;
        casillas[x - 1, y] = obj;
        casillas[x - 1, y + 1] = obj;
        casillas[x - 1, y  +2] = obj;

        casillas[x - 0, y - 1] = obj;
        casillas[x - 0, y] = obj;
        casillas[x - 0, y + 1] = obj;
        casillas[x - 0, y  +2] = obj;

        casillas[x +1, y - 1] = obj;
        casillas[x +1, y] = obj;
        casillas[x +1, y + 1] = obj;
        casillas[x +1, y  +2] = obj;
    }
    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !IsMouseOverUI())
        {


            RaycastHit hit = new RaycastHit();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit,Mathf.Infinity, ~IgnoreMe))
            {

                /*
                if (hit.collider.gameObject.tag == "Floor")
                {
                    
                    Debug.Log("suelo");
                    int x, z;
                    GetGridPos(hit.point, out x, out z);

                    Vector2 gridPos = GridController.gridToMatrix(x, z);
                    int q = (int)gridPos.x;
                    int w = (int)gridPos.y;

                    Debug.Log(q + " , " + w);
                    GameObject hitObj = getgrid(q, w);
                    if (hitObj != null) 
                    {
                        Debug.Log(hitObj.name);
                        hitObj.transform.position = new Vector3(hitObj.transform.position.x, hitObj.transform.position.y + 0.1f, hitObj.transform.position.z);
                    } 
                }*/
            }
        }
    }

    public void GetGridPos(Vector3 posicion, out int x, out int z)
    {
        x = Mathf.FloorToInt(posicion.x / 5);
        z = Mathf.FloorToInt(posicion.z / 5);
    }
}
