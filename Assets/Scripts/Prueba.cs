using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prueba : MonoBehaviour
{
    public GameObject prefab;
    public Material material;
    private Mesh mesh;
    void Start()
    {
        mesh = prefab.transform.GetComponent<MeshFilter>().mesh;
        
    }

    // Update is called once per frame
    void Update()
    {
        Graphics.DrawMesh(mesh, new Vector3(90.0f, 10.0f, 80.0f), Quaternion.identity,material,0,null);
    }
}
