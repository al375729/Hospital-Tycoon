using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorDePersonajes : MonoBehaviour
{
    public GameObject prefab;

    public Material[] materiales;

    public GameObject[] genertaedCharacters;

    public testing1 test;

    private int generatingCount = 5;
    void Start()
    {
        genertaedCharacters = new GameObject[generatingCount];

        for (int i = 0; i < generatingCount; i++)
        {
            genertaedCharacters[i]  = Instantiate(prefab,this.transform.position + new Vector3((15*i)+50f,0,0),Quaternion.identity);


            int children = genertaedCharacters[i].transform.childCount;
            
            for (int j = 0; j < children; ++j)
            {
                int random = Random.Range(0, materiales.Length - 1);
                genertaedCharacters[i].transform.GetChild(j).GetComponent<MeshRenderer>().material = materiales[random];
            }
                

        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            test.setUI(genertaedCharacters);
        }
            
    }
}
