using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DrawSquare : MonoBehaviour
{
    public GameObject go;
    Vector3 pos;
    private GameObject prefab;
    private bool CONSTRUTENDO = false;
    private bool vamos = false;
    RayCastChceker rayCastChceker;
    public List<GameObject> puntos;
    public List<Vector2> grid;

    private int px, py = -1;

    private Vector2 centro;
    public GameObject padre;
    private GameObject copiaPadre;

    private Vector2 matrixXY = new Vector2(-1,-1);
    private Vector2 newVectorXY = new Vector2(-5, -5);
    public static bool allowed = true;
    private bool limpio = true;

    public Material[] materials;
    public static Material[] materialsStatic;
    private static Material currentMaterial;

    private GameObject first;
    void Start()
    {
        materialsStatic = materials;
        puntos = new List<GameObject>(100);
        grid = new List<Vector2>(100);
        copiaPadre = padre;

        rayCastChceker = RayCastChceker.Instance;

    }

    public static void setCurrentRoom(int x)
    {
        switch (x)
        {
            case 0:
                currentMaterial = materialsStatic[0];
                break;
            case 1:
                currentMaterial = materialsStatic[1];
                break;
            case 2:
                currentMaterial = materialsStatic[2];
                break;
            default:
                break;


        }
    }
    void Update()
    {
       

        if (!CONSTRUTENDO && GlobalVariables.BUILDING_MODE) 
        {
            if (Input.GetMouseButtonDown(0))
            {
                

                RaycastHit hit = new RaycastHit();
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {


                    if (hit.collider.gameObject.tag == "Floor")
                    {
                        first = null;
                        CONSTRUTENDO = true;
                        padre = new GameObject();
                        padre.tag = "Room";
                        GameObject obj = Instantiate(go);
                        obj.GetComponent<MeshRenderer>().material = currentMaterial;

                        float zCoord = Camera.main.WorldToScreenPoint(

                        gameObject.transform.position).z;


                        int x, z;
                        GetGridPos(hit.point, out x, out z);

                        Vector2 gridPos = GridController.gridToMatrix(x, z);
                        int cv = (int)gridPos.x;
                        int cb= (int)gridPos.y;
                        Debug.Log(cv+ " , " + cb);


                        px = x;
                        py = z;

                        matrixXY = new Vector2(x, z);
                        
                        Vector3 posicion;
                        posicion = GetWorldPosition(x, z);
                        
                        obj.transform.position = new Vector3(posicion.x + 2.5f, posicion.y, posicion.z + 2.5f);
                        centro = new Vector2(obj.transform.position.x,obj.transform.position.z);
                        first = obj;
                    }
                }
            }
        

        }
        else if( GlobalVariables.BUILDING_MODE)
        {
            if (Input.GetMouseButtonDown(0))
            {
                
                int x, y;
                foreach (GameObject item in puntos)
                {
                    int e, r;
                    GetGridPos(new Vector3(item.transform.position.x, 0, item.transform.position.z), out e, out r);



                    Vector2 gridPos = GridController.gridToMatrix(e, r);
                    x = (int)gridPos.x;
                    y = (int)gridPos.y;

                    if (GridController.checkgrid(x,y))
                    {
                        limpio = false;
                        Debug.Log("asdddddd");

                    }

                }
                Debug.Log(limpio);
                if (limpio)
                {
                    Debug.Log("32");
                    foreach (GameObject item in puntos)
                    {
                        int e, r;
                        GetGridPos(new Vector3(item.transform.position.x, 0, item.transform.position.z), out e, out r);



                        Vector2 gridPos = GridController.gridToMatrix(e, r);
                        x = (int)gridPos.x;
                        y = (int)gridPos.y;

                        GridController.setgrid(x, y, item.transform.parent.gameObject);
                    }
                    CONSTRUTENDO = false;
                }
                limpio = true;
                GlobalVariables.BUILDING_MODE = false;

            }

            RaycastHit hit = new RaycastHit();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {


                if (hit.collider.gameObject.tag == "Floor" || hit.collider.gameObject.tag == "NewRoom" || hit.collider.gameObject.tag == "Room")
                {


                    float zCoord = Camera.main.WorldToScreenPoint(

                    gameObject.transform.position).z;


                    int x, z;
                    GetGridPos(hit.point, out x, out z);
                    //Debug.Log(x+" " + z);
                    //Debug.Log(gridToMatrix(x,z));

                    newVectorXY = new Vector2(x, z);
                    if (newVectorXY != matrixXY)
                    {
                        puntos = new List<GameObject>(200);
                        allowed = true;
                        if (padre.transform.childCount != 0)
                        {
                            Destroy(padre);
                            padre = new GameObject();
                            padre.tag = "NewRoom";
                            first.transform.SetParent(padre.transform);
                            padre.AddComponent<RoomController>();

                        }

                        int horiz, vert;
                        bool isXnegative;
                        bool isYnegative;


                        horiz = px - x;

                        vert = py - z;

                        if (horiz > 0)
                        {
                            isXnegative = false;
                        }
                        else
                        {
                            isXnegative = true;
                            horiz = horiz * -1;
                        }

                        if (vert > 0)
                        {
                            isYnegative = false;
                        }
                        else
                        {
                            isYnegative = true;
                            vert = vert * -1;
                        }


                        for (int i = 0; i < horiz+1; i++)
                        {
                            for (int j = 0; j < vert+1; j++)
                            {
                                if(i!=0 || j!=0)
                                {
                                    prefab = Instantiate(go);
                                    int multx;
                                    int multy;

                                    if (isXnegative) multx = 1;
                                    else multx = -1;

                                    if (isYnegative) multy = 1;
                                    else multy = -1;

                                    prefab.transform.position = new Vector3(centro.x + (5 * multx * (i)), 0, centro.y + (5 * multy * (j)));
                                    prefab.GetComponent<MeshRenderer>().material = currentMaterial;
                                    prefab.transform.SetParent(padre.transform);
                                    prefab.tag = "NewRoom";
                                    prefab.layer = 0;

                                    puntos.Add(prefab);

                                    int a = x + i - 1;
                                    int b = z + j - 1;

                                }

                            }
                        }


                        matrixXY = newVectorXY;
                    }
                }
            }
        }


    }



    private Vector3 GetMouseWorldPos()
    {
        //(x,y)
        Vector3 mousePoint = Input.mousePosition;

        //z
        mousePoint.z = 0;

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

}
