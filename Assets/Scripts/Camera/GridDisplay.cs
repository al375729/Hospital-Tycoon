using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GridDisplay : MonoBehaviour
{
    // Start is called before the first frame update

    public Test test;
    private Grid grid;
    private int[,] cuadricula;
    private TextMesh[,] gridTextMesh;
    private int filas;
    private int columnas;
    public Material material;

    private Vector3 Origin; // place where mouse is first pressed
    private Vector3 Diference; // change in position of mouse relative to origin
    void Start()
    {
        grid = test.getGrid();
        cuadricula = test.getCuadricula();
        gridTextMesh = test.getTextMesh();
        filas = test.getFilas();

        columnas = test.getColumnas();
    }

    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (!IsMouseOverUI())
        {
            float fov;
            fov = Camera.main.fieldOfView;
            fov += Input.GetAxis("Mouse ScrollWheel") * sensitivity;
            fov = Mathf.Clamp(fov, minFov, maxFov);
            Camera.main.fieldOfView = fov;

        }
        */
    }

    private void OnPostRender()
    {
        for (int i = 0; i < cuadricula.GetLength(0); i++)
        {

            for (int j = 0; j < cuadricula.GetLength(1); j++)
            {

                if (gridTextMesh[i, j] == null)
                {/*
                    GameObject Texto = new GameObject("Texto", typeof(TextMesh));
                    TextMesh texto = Texto.GetComponent<TextMesh>();
                    gridTextMesh[i,j] = texto;
                    texto.transform.position = grid.GetWorldPosition(i, j) + new Vector3(tamañoCelda / 2, 0, tamañoCelda) * 0.5f;
                    texto.transform.rotation = Quaternion.Euler(90, 0, 0);
                    //texto.text = "[" + i.ToString() + "," + j.ToString() + "]";
                    texto.text = cuadricula[i,j].ToString();
                    texto.alignment = TextAlignment.Center;
                    texto.color = Color.white;
                    */
                }
                
                DrawLine(grid.GetWorldPosition(i, j) - new Vector3(filas * 5, 0, columnas * 5) , grid.GetWorldPosition(i, j + 1) - new Vector3(filas * 5, 0, columnas * 5));
                DrawLine(grid.GetWorldPosition(i, j) - new Vector3(filas * 5, 0, columnas * 5), grid.GetWorldPosition(i + 1, j) - new Vector3(filas * 5, 0, columnas * 5));

                /*
                if (i == 0)
                {
                    cuadricula[i, j] = 10;
                    gridTextMesh[i, j].text = cuadricula[i, j].ToString(); 
                }*/
            }
        }
        DrawLine(grid.GetWorldPosition(0, columnas) - new Vector3(filas * 5, 0, columnas * 5), grid.GetWorldPosition(filas, columnas) - new Vector3(filas * 5, 0, columnas * 5));
        DrawLine(grid.GetWorldPosition(filas, 0) - new Vector3(filas * 5, 0, columnas * 5), grid.GetWorldPosition(filas, columnas) - new Vector3(filas * 5, 0, columnas * 5));
    }


    void DrawLine(Vector3 inicio, Vector3 fin)
    {

        GL.Begin(GL.LINES);
        material.SetPass(0);
        GL.Color(Color.black);
        GL.Vertex(inicio);
        GL.Vertex(fin);

        GL.End();

    }

    void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Origin = MousePos();
        }
        if (Input.GetMouseButton(0))
        {
            Diference = MousePos() - transform.position;
            transform.position = Origin - Diference;
        }
        
    }
    // return the position of the mouse in world coordinates (helper method)
    Vector3 MousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

}
