using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public int filas;
    public int columnas;
    public float tamañoCelda;

    private Grid grid;
    public int[,] cuadricula;
    void Start()
    {
        grid = new Grid(filas, columnas, tamañoCelda);
        cuadricula = grid.getCuadricula();

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
                //GameObject Texto = new GameObject("Texto", typeof(TextMesh));
                //TextMesh texto = Texto.GetComponent<TextMesh>();

                //texto.transform.position = grid.GetWorldPosition(i, j) + new Vector3(tamañoCelda / 2, 0, tamañoCelda) * 0.5f;
                //texto.transform.rotation = Quaternion.Euler(90, 0, 0);
                //texto.text = "[" + i.ToString() + "," + j.ToString() + "]";
                //texto.alignment = TextAlignment.Center;
                //texto.color = Color.white;

                DrawLine(grid.GetWorldPosition(i, j), grid.GetWorldPosition(i, j + 1));
                DrawLine(grid.GetWorldPosition(i, j), grid.GetWorldPosition(i + 1, j));
                //Gizmos.DrawLine(grid.GetWorldPosition(i, j), grid.GetWorldPosition(i, j + 1));
                //Gizmos.DrawLine(grid.GetWorldPosition(i, j), grid.GetWorldPosition(i + 1, j));
            }
        }
        DrawLine(grid.GetWorldPosition(0, columnas), grid.GetWorldPosition(filas, columnas));
        DrawLine(grid.GetWorldPosition(filas, 0), grid.GetWorldPosition(filas, columnas));
        //Gizmos.DrawLine(grid.GetWorldPosition(0, columnas), grid.GetWorldPosition(filas, columnas));
        //Gizmos.DrawLine(grid.GetWorldPosition(filas, 0), grid.GetWorldPosition(filas, columnas)); */

    }
  

    void DrawLine(Vector3 inicio, Vector3 fin)
    {
        GL.Begin(GL.LINES);
        GL.Color(Color.red);
        GL.Vertex(inicio);
        GL.Vertex(fin);
        GL.End();
    }


}
