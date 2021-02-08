using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid 
{
    private int filas;
    private int columnas;
    private int[,] cuadricula;
    private float tamañoCelda;


    public Grid(int filas, int columnas, float tamañoCelda) 
    {
        this.filas = filas;
        this.columnas = columnas;

        cuadricula = new int[filas, columnas];

        this.tamañoCelda = tamañoCelda;

        for(int i = 0; i< cuadricula.GetLength(0); i++)
        {
            for (int j = 0; j < cuadricula.GetLength(1) ; j++) 
            {
                GameObject Texto = new GameObject("Texto", typeof(TextMesh));
                TextMesh texto = Texto.GetComponent<TextMesh>();

                texto.transform.position = GetWorldPosition(i,j) + new Vector3(tamañoCelda,tamañoCelda)*0.5f;
                texto.text = cuadricula[i, j].ToString();
                texto.alignment = TextAlignment.Center;
                texto.color = Color.white;

                //Debug.Log(cuadricula[i,j].ToString());

                Debug.DrawLine(GetWorldPosition(i, j), GetWorldPosition(i, j + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(i, j), GetWorldPosition(i +1, j), Color.white, 100f);
            }
        }
        Debug.DrawLine(GetWorldPosition(0, columnas), GetWorldPosition(filas, columnas), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(filas, 0), GetWorldPosition(filas, columnas), Color.white, 100f);
    }


    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * tamañoCelda;
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
