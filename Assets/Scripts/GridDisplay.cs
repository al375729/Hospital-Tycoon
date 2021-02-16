using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    void Start()
    {
        grid = test.getGrid();
        cuadricula = test.getCuadricula();
        gridTextMesh = test.getTextMesh();
        filas = test.getFilas();
        columnas = test.getColumnas();
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

                DrawLine(grid.GetWorldPosition(i, j), grid.GetWorldPosition(i, j + 1));
                DrawLine(grid.GetWorldPosition(i, j), grid.GetWorldPosition(i + 1, j));

                /*
                if (i == 0)
                {
                    cuadricula[i, j] = 10;
                    gridTextMesh[i, j].text = cuadricula[i, j].ToString(); 
                }*/
            }
        }
        DrawLine(grid.GetWorldPosition(0, columnas), grid.GetWorldPosition(filas, columnas));
        DrawLine(grid.GetWorldPosition(filas, 0), grid.GetWorldPosition(filas, columnas));
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



}
