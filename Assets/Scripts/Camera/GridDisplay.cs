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

    private Vector3 Origin; 
    private Vector3 Diference;
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
      
    }

    private void OnPostRender()
    {
        for (int i = 0; i < cuadricula.GetLength(0); i++)
        {

            for (int j = 0; j < cuadricula.GetLength(1); j++)
            {
                DrawLine(grid.GetWorldPosition(i, j) - new Vector3(filas * 2.5f, 0, columnas * 2.5f) , grid.GetWorldPosition(i, j + 1) - new Vector3(filas * 2.5f, 0, columnas * 2.5f));
                DrawLine(grid.GetWorldPosition(i, j) - new Vector3(filas * 2.5f, 0, columnas * 2.5f), grid.GetWorldPosition(i + 1, j) - new Vector3(filas * 2.5f, 0, columnas * 2.5f));

            }
        }
        DrawLine(grid.GetWorldPosition(0, columnas) - new Vector3(filas * 2.5f, 0, columnas * 2.5f), grid.GetWorldPosition(filas, columnas) - new Vector3(filas * 2.5f, 0, columnas * 2.5f));
        DrawLine(grid.GetWorldPosition(filas, 0) - new Vector3(filas * 2.5f, 0, columnas * 2.5f), grid.GetWorldPosition(filas, columnas) - new Vector3(filas * 2.5f, 0, columnas * 2.5f));
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
    Vector3 MousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

}
