using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    private int filas;
    private int columnas;
    public int[,] cuadricula;
    private float tamañoCelda;


    public Grid(int filas, int columnas, float tamañoCelda) 
    {
        this.filas = filas;
        this.columnas = columnas;

        cuadricula = new int[filas, columnas];

        this.tamañoCelda = tamañoCelda;
    }


    public int[,] getCuadricula() 
    {
        return cuadricula;
    }

    public int getFilas()
    {
        return filas;
    }

    public int getColumnas()
    {
        return columnas;
    }

    public Vector3 GetWorldPosition(int x, int z)
    {
        return new Vector3(x ,0,z ) * tamañoCelda;
    }

    public void SetCellValueXZ(int fila, int columna, int valor) 
    {
        if (fila >= 0 && columna >= 0 && fila < filas && columna < columnas) 
        {
            cuadricula[fila, columna] = valor;
        }
    }
    public void SetCellValueWorldPos(Vector3 posicion, int valor)
    {
        int x, z;
        GetGridPos(posicion, out x, out z);


        cuadricula[x, z] = valor;
    }

    public void GetGridPos(Vector3 posicion, out int x, out int z)
    {
        x = Mathf.FloorToInt(posicion.x / tamañoCelda);
        z = Mathf.FloorToInt(posicion.z / tamañoCelda);
    }

}
