using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILineRenderer : Graphic
{
    public float thickness = 10;
    public Vector2Int gridSize = new Vector2Int(1, 1);

    public float widthOffset = 250;
    public float heightOffset = 175f;

    float width;
    float height;
    float cellWidth;
    float cellheight;

    protected override void OnPopulateMesh(VertexHelper vh)
    {
       

        vh.Clear();

         width = rectTransform.rect.width ;
         height = rectTransform.rect.height ;
       
         cellWidth = (float)width/gridSize.x;
         cellheight = (float)height/gridSize.y;


        int count = 0;

        for (int y = 0; y < gridSize.y; y++)
        {
            for (int x = 0; x < gridSize.x; x++)
            {
                DrawCell(x, y, count, vh);
                count++;
            }
        }


    }

    private void DrawCell(int x, int y,int index, VertexHelper vh)
    {
        float xPos = cellWidth * x;
        float yPos = cellheight * y;
        UIVertex vertex = UIVertex.simpleVert;
        vertex.color = color;

        vertex.position = new Vector3(xPos- widthOffset, yPos- heightOffset);
        vh.AddVert(vertex);

        vertex.position = new Vector3(xPos - widthOffset, yPos + cellheight - heightOffset);
        vh.AddVert(vertex);

        vertex.position = new Vector3(cellWidth + xPos - widthOffset, cellheight + yPos- heightOffset);
        vh.AddVert(vertex);

        vertex.position = new Vector3(xPos + cellWidth - widthOffset, yPos - heightOffset);
        vh.AddVert(vertex);

        //vh.AddTriangle(0, 1, 2);
        //vh.AddTriangle(2, 3, 0);

        float widthSqr = thickness * thickness;
        float dsitanceSqr = widthSqr / 2f;
        float distance = Mathf.Sqrt(dsitanceSqr);

        vertex.position = new Vector3(xPos + distance - widthOffset, yPos + distance - heightOffset);
        vh.AddVert(vertex);

        vertex.position = new Vector3(xPos + distance - widthOffset, yPos + cellheight - heightOffset - distance);
        vh.AddVert(vertex);

        vertex.position = new Vector3(cellWidth + xPos- widthOffset - distance, cellheight+ yPos - heightOffset - distance);
        vh.AddVert(vertex);

        vertex.position = new Vector3(cellWidth + xPos - widthOffset - distance, yPos + distance - heightOffset);
        vh.AddVert(vertex);

        int offset = index * 8;
        
        //Left Edge
        vh.AddTriangle(offset + 0, offset + 1, offset + 5);
        vh.AddTriangle(offset + 5, offset + 4, offset + 0);

        //Top Edge
        vh.AddTriangle(offset + 1, offset + 2, offset + 6);
        vh.AddTriangle(offset + 6, offset + 5, offset + 1);

        //Left Edge
        vh.AddTriangle(offset + 2, offset + 3, offset + 7);
        vh.AddTriangle(offset + 7, offset + 6, offset + 2);

        //Left Edge
        vh.AddTriangle(offset + 3, offset + 0, offset + 4);
        vh.AddTriangle(offset + 4, offset + 7, offset + 3);
    }
}
