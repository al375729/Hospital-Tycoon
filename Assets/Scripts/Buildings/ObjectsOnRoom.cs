using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsOnRoom : MonoBehaviour
{
    public Material[] materiales;

    public type objectType = type.None;

    public bool isSelected;

    public int indexInList = -1;
    public enum type 
    {
        ConsultDoctor,
        ConsultPatient,
        None
    }
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

    public void setNewPosition()
    { 
    
    }
}
