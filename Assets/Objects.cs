using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objects : MonoBehaviour
{
    public Material[] materiales;


    void Start()
    {
        materiales[0] = this.gameObject.GetComponent<MeshRenderer>().material;
}

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeMaterial(int i)
    {
        this.transform.GetComponent<MeshRenderer>().material = materiales[i];
    }
}
