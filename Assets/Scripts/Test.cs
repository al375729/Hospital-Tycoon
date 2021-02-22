using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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

    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    private void Update()
    {
    }

  
}
