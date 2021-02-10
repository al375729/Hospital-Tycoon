using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public int filas;
    public int columnas;
    public float tamañoCelda;
    public GameObject test;

    private Grid grid;
    public int[,] cuadricula;
    public TextMesh[,] gridTextMesh;

    public BuildingObjects building;
    void Start()
    {
        grid = new Grid(filas, columnas, tamañoCelda);
        cuadricula = grid.getCuadricula();

        gridTextMesh = new TextMesh[grid.getFilas(), grid.getColumnas()];
        Vector3 posicion = this.transform.position;
        posicion.y -= 0.2f;
        transform.position = posicion;
    }


  void OnDrawGizmos()
    {
        for (int i = 0; i < cuadricula.GetLength(0); i++)
        {
            
            for (int j = 0; j < cuadricula.GetLength(1); j++)
            {
 
                if (gridTextMesh[i,j] == null)
                {
                    GameObject Texto = new GameObject("Texto", typeof(TextMesh));
                    TextMesh texto = Texto.GetComponent<TextMesh>();
                    gridTextMesh[i,j] = texto;
                    texto.transform.position = grid.GetWorldPosition(i, j) + new Vector3(tamañoCelda / 2, 0, tamañoCelda) * 0.5f;
                    texto.transform.rotation = Quaternion.Euler(90, 0, 0);
                    //texto.text = "[" + i.ToString() + "," + j.ToString() + "]";
                    texto.text = cuadricula[i,j].ToString();
                    texto.alignment = TextAlignment.Center;
                    texto.color = Color.white;
                }
                
                DrawLine(grid.GetWorldPosition(i, j), grid.GetWorldPosition(i, j + 1));
                DrawLine(grid.GetWorldPosition(i, j), grid.GetWorldPosition(i + 1, j));


                if (i == 0)
                {
                    cuadricula[i, j] = 10;
                    gridTextMesh[i, j].text = cuadricula[i, j].ToString(); 
                }
            }
        }
        DrawLine(grid.GetWorldPosition(0, columnas), grid.GetWorldPosition(filas, columnas));
        DrawLine(grid.GetWorldPosition(filas, 0), grid.GetWorldPosition(filas, columnas));
    }
  

    void DrawLine(Vector3 inicio, Vector3 fin)
    {
        GL.Begin(GL.LINES);
        GL.Color(Color.red);
        GL.Vertex(inicio);
        GL.Vertex(fin);
        GL.End();
    }

    private void Update()
    {
        Vector3 mouse = Input.mousePosition;
        Ray castPoint = Camera.main.ScreenPointToRay(mouse);
        RaycastHit hit;

        if (Input.GetButtonDown("Fire1"))
        {
            if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
            {
                int x, z;
                grid.GetGridPos(hit.point, out x, out z);

                if (Constructable(building,  x,  z))
                {
                    
                    Instantiate(building.prefab, grid.GetWorldPosition(x + 1, z + 1), Quaternion.identity);

                    setValues(building,x,z);
                }
                
            }
        }     
    }

    private void setValues(BuildingObjects building,int x, int z)
    {
        for (int i = 0; i < building.xValues.Length; i++)
        {
            grid.SetCellValueXZ(x+building.xValues[i], z + building.yValues[i], 10);
            gridTextMesh[x +building.xValues[i], z + building.yValues[i]].text = cuadricula[x + building.xValues[i], z + building.yValues[i]].ToString();
        }
    }

    private bool Constructable(BuildingObjects building, int x, int y)
    {
        if (building.symetric)
        {
            return CheckConstructable(building.xValues,building.yValues,x,y);
        }
        else return false;
    }

    private bool CheckConstructable(int[] xValues, int[] yValues,int x, int y)
    {
        for (int i = 0;i< xValues.Length; i++)
        {
            if (cuadricula[x,y] != 0) return false;

            else if(cuadricula[x + building.xValues[i], y + building.yValues[i]] != 0)
            {
                return false;
            }
            
        }
        return true;
    }

    public bool aviableToBuild()
    {
        return transform == null;
    }
}
