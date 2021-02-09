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

    public Vector3 GetWorldPosition(int x, int z)
    {
        return new Vector3(x, 0,z) * tamañoCelda;
    }

    public void SetCellValue(int fila, int columna, int valor) 
    {
        if (fila >= 0 && columna >= 0 && fila < filas && columna < columnas) 
        {
            cuadricula[fila, columna] = valor;
        }
    }
    public void SetCellValue(Vector3 posicion, int valor)
    {
        Vector2Int posicionCuadricula = GetFilaColumna(posicion);

        SetCellValue(posicionCuadricula.x, posicionCuadricula.y, valor);
    }

    public Vector2Int GetFilaColumna(Vector3 posicion)
    {
        Vector2Int posCuadricula = new Vector2Int (0, 0);

        posCuadricula.x = Mathf.FloorToInt(posicion.x / tamañoCelda);
        posCuadricula.y = Mathf.FloorToInt(posicion.y / tamañoCelda);

        return posCuadricula;
    }

    
    
}
