using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawWalls : MonoBehaviour
{
    public GameObject go;
    Vector3 pos;
    private GameObject prefab;
    private bool CONSTRUTENDO = false;
    private bool vamos = false;
    RayCastChceker rayCastChceker;
    public List<GameObject> puntos;
    public List<Vector2> grid;
    public GameObject wall;

    private int px, py = -1;

    private Vector2 centro;
    public GameObject padre;
    private GameObject copiaPadre;

    private Vector2 matrixXY = new Vector2(-1, -1);
    private Vector2 newVectorXY = new Vector2(-5, -5);
    public static bool allowed = true;
    private bool limpio = true;
    GameObject obj;
    public Material[] materials;
    public static Material[] materialsStatic;
    private static Material currentMaterial;

    private GameObject first;
    public GameObject cosa;
    public float zCoord;

    Material material;
    public Material materialBase;
    public Material materialMal;
    public bool buildable;

    private int buildType = -1;

    void Start()
    {
        buildable = true;
        materialsStatic = materials;
        puntos = new List<GameObject>(100);
        grid = new List<Vector2>(100);
        copiaPadre = padre;

        rayCastChceker = RayCastChceker.Instance;
        currentMaterial = materials[0];
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

    public void buildWall()
    {
        go.AddComponent<MeshRenderer>();
    }
    void Update()
    {

        if (!CONSTRUTENDO && GlobalVariables.WALL_MODE)
        {
            zCoord = Camera.main.WorldToScreenPoint(

           go.transform.position).z;


            int c, v;
            GetGridPos(GetMouseWorldPos(), out c, out v);
            Vector3 posic;
            posic = GetWorldPosition(c, v);

            go.transform.position = new Vector3(posic.x, 0, posic.z);

            if (Input.GetMouseButtonDown(0))
            {

                RaycastHit hit = new RaycastHit();
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {


                    if (hit.collider.gameObject.tag == "Floor" || hit.collider.gameObject.tag == "NewRoom")
                    {
                        first = null;
                        CONSTRUTENDO = true;
                        padre = new GameObject();
                        padre.tag = "Room";
                        obj = Instantiate(go);
                        obj.GetComponent<MeshRenderer>().material = currentMaterial;

                        zCoord = Camera.main.WorldToScreenPoint(

                        gameObject.transform.position).z;


                        int x, z;
                        GetGridPos(hit.point, out x, out z);

                        Vector2 gridPos = GridController.gridToMatrix(x, z);

                        px = x;
                        py = z;

                        matrixXY = new Vector2(x, z);

                        Vector3 posicion;
                        posicion = GetWorldPosition(x, z);

                        //Destroy(obj.GetComponent<MeshRenderer>());
                        obj.transform.position = new Vector3(posicion.x, posicion.y, posicion.z);

                        centro = new Vector2(go.transform.position.x, go.transform.position.z);


                        first = obj;
                        Destroy(obj);

                    }
                }
            }


        }
        else if (GlobalVariables.WALL_MODE)
        {

            if (Input.GetMouseButtonDown(0) && buildable)
            {

                CONSTRUTENDO = false;

                GlobalVariables.WALL_MODE = false;

                padre.tag = "Wall";
                foreach (Transform child in padre.transform)
                {
                    child.gameObject.tag = "Wall";
                    child.gameObject.layer = 10;
                }

                buildWalls();

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

                    newVectorXY = new Vector2(x, z);
                    if (newVectorXY != matrixXY)
                    {
                        puntos = new List<GameObject>(200);
                        allowed = true;
                        if (padre.transform.childCount != 0)
                        {
                            Destroy(padre);
                            padre = new GameObject();
                            Rigidbody rb = padre.AddComponent<Rigidbody>();
                            rb.useGravity = false;
                            padre.tag = "Wall";
                            go.transform.position = new Vector3(1000f,0,0);

                            padre.AddComponent<RoomController>();

                        }

                        GameObject primero = Instantiate(go);
                        primero.name = "FFFFFFFFFFFF";

                        primero.transform.position = new Vector3(centro.x, 0, centro.y);
                        primero.transform.SetParent(padre.transform);

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

                        if (newVectorXY.y == matrixXY.y)
                        {
                            for (int i = 1; i < horiz + 1; i++)
                            {
                                prefab = Instantiate(go);
                                int multx;
                                int multy;

                                if (isXnegative) multx = 1;
                                else multx = -1;

                                if (isYnegative) multy = 1;
                                else multy = -1;

                                prefab.transform.position = new Vector3(centro.x + (5 * multx * (i)), 0, centro.y);

                                prefab.transform.SetParent(padre.transform);
                                prefab.tag = "NewItem";
                                prefab.layer = 11;
                                
                                puntos.Add(prefab);
                                buildType = 0;
                            }
                        }

                        else if (newVectorXY.x == matrixXY.x)
                        {
                            for (int j = 1; j < vert + 1; j++)
                            {
                                prefab = Instantiate(go);
                                int multx;
                                int multy;

                                if (isXnegative) multx = 1;
                                else multx = -1;

                                if (isYnegative) multy = 1;
                                else multy = -1;

                                prefab.transform.position = new Vector3(centro.x, 0, centro.y + (5 * multy * (j)));

                                prefab.transform.SetParent(padre.transform);
                                prefab.tag = "NewItem";
                                prefab.layer = 11;

                                
                                puntos.Add(prefab);
                                buildType = 1;
                            }

                        }
                        matrixXY = newVectorXY;
                        int choca = 0;
                        int noChocca = 0;
                        foreach (Transform child in padre.transform)
                        {
                            GameObject under = rayCastChceker.detectUnder(new Vector3(child.transform.position.x,10f,child.transform.position.z));
                            if (under != null)
                            {
                                choca++;
                                //Debug.Log(choca);
                            }
                            else
                            {
                                noChocca++;
                                //Debug.Log(noChocca);
                            }
                        }

                        if (choca >= 2)
                        {
                            makeNotBuildable();
                        }
                        else
                        {
                            makeBuildable();
                        }
                    }
                }
            }
        }


    }

    private void buildWalls()
    {
        float difference = 0;
        if(buildType ==0)
        {
            difference = (padre.transform.GetChild(0).gameObject.transform.position.x - padre.transform.GetChild(1).gameObject.transform.position.x)/-2;

            for (int i = 0; i < padre.transform.childCount-1; i++)
            {
                Vector3 vec = padre.transform.GetChild(i).gameObject.transform.position;

                GameObject instance = Instantiate(wall);
                instance.transform.position = new Vector3(vec.x + difference, 0f, vec.z);
                instance.transform.localScale= new Vector3(5, 2, 1);
            }
        }
        if (buildType == 1)
        {
            difference = (padre.transform.GetChild(0).gameObject.transform.position.z - padre.transform.GetChild(1).gameObject.transform.position.z) / -2;

            for (int i = 0; i < padre.transform.childCount-1; i++)
            {
                Vector3 vec = padre.transform.GetChild(i).gameObject.transform.position;

                GameObject instance = Instantiate(wall);
                instance.transform.position = new Vector3(vec.x , 0f, vec.z + difference);
                instance.transform.localScale = new Vector3(1, 2, 5);
            }
        }


    }

    private void makeBuildable()
    {
        foreach (Transform child in padre.transform)
        {
            child.GetComponent<MeshRenderer>().material = materialBase;
        }
        buildable = true;
    }

    private void makeNotBuildable()
    {
        foreach (Transform child in padre.transform)
        {
            child.GetComponent<MeshRenderer>().material = materialMal;
        }
        buildable = false;
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

}
