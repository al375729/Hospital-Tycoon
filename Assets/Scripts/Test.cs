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

    public Seleccionador sellecionador;


    private Grid grid;
    public int[,] cuadricula;
    public TextMesh[,] gridTextMesh;

    public BuildingObjects building;

    public  Material material;

    private void Awake()
    {
        grid = new Grid(filas, columnas, tamañoCelda);
        cuadricula = grid.getCuadricula();


        gridTextMesh = new TextMesh[grid.getFilas(), grid.getColumnas()];
    }
    void Start()
    {
        
        Vector3 posicion = this.transform.position;
        posicion.y -= 0.2f;
        transform.position = posicion;

        
    }

    public int[,] getCuadricula()
    {
        return cuadricula;
    }

    public TextMesh[,] getTextMesh()
    {
        return gridTextMesh;
    }
    public Grid getGrid()
    {
        return grid;
    }

    public int getColumnas()
    {
        return columnas;
    }

    public int getFilas()
    {
        return filas;
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
                if (hit.transform.gameObject.tag == "Floor")
                {
                    int x, z;
                    grid.GetGridPos(hit.point, out x, out z);

                    //if (Constructable(building,  x,  z))
                    //{

                    Instantiate(building.prefab, grid.GetWorldPosition(x + 1, z + 1), Quaternion.identity);

                    //setValues(building,x,z);
                    //}
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
