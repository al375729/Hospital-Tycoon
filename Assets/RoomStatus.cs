using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoomStatus : MonoBehaviour
{
    public TextMeshPro textPro;

    public string reachable = "";
    public string workers = "";

    private Material baseMaterial;
    public Material notSuitableRoom;
    void Awake()
    {
        baseMaterial = this.gameObject.transform.parent.GetChild(0).GetComponent<MeshRenderer>().material;

        textPro = gameObject.GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateText()
    {
        if (reachable == "" && workers == "")
        {
            this.gameObject.transform.parent.GetChild(0).GetComponent<MeshRenderer>().material = baseMaterial;
        }
        else
        {
            this.gameObject.transform.parent.GetChild(0).GetComponent<MeshRenderer>().material = notSuitableRoom;
        }
    
        textPro.text = reachable + workers;
    }
}
