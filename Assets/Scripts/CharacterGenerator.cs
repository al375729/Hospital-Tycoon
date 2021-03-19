using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGenerator : MonoBehaviour
{
    public GameObject prefab;

    public Material[] materialesPelo;
    public Material[] materialesPiel;

    public GameObject[] pelos;
    public GameObject[] peloFacial;

    private GameObject[] genertaedCharacters;

    public PopulateWorkerShop workerShop;

    public Material bata;
    public Material camsieta;
    public Material pantalon;

    private int generatingCount = 5;
    void Start()
    {
        genertaedCharacters = new GameObject[generatingCount];

        for (int i = 0; i < generatingCount; i++)
        {
            int colorDePelo = Random.Range(0,materialesPelo.Length);

            genertaedCharacters[i]  = Instantiate(prefab,this.transform.position + new Vector3((15*i)+50f,0,0),Quaternion.identity);

            int randomPelo = Random.Range(0, pelos.Length);
            
            if (randomPelo != materialesPelo.Length)
            {
                GameObject pelo = Instantiate(pelos[randomPelo], genertaedCharacters[i].transform, false);
                pelo.name = "Pelo";
                pelo.transform.rotation = Quaternion.Euler(-90f, 0, 0);
                pelo.transform.localScale = new Vector3(1f, 1f, 1f);
                pelo.transform.localPosition = new Vector3(0f, 0f, 0f);
            }

            int randomBarba = Random.Range(0, 11);
            if (randomBarba == 0 || randomBarba == 1)
            {
                GameObject barba = Instantiate(peloFacial[randomBarba], genertaedCharacters[i].transform, false);
                barba.name = "PeloFacial";
                barba.transform.rotation = Quaternion.Euler(-90f, 0, 0);
                barba.transform.localScale = new Vector3(1f, 1f, 1f);
                barba.transform.localPosition = new Vector3(0f, 0f, 0f);
            }




            int children = genertaedCharacters[i].transform.childCount;
            
            for (int j = 0; j < children; ++j)
            {
                int ran = Random.Range(0, materialesPiel.Length);

                if(genertaedCharacters[i].transform.GetChild(j).GetComponent<SkinnedMeshRenderer>() != null)
                {
                    if (genertaedCharacters[i].transform.GetChild(j).name == "Cejas")
                    {
                        genertaedCharacters[i].transform.GetChild(j).GetComponent<SkinnedMeshRenderer>().material = materialesPelo[colorDePelo];
                    }
                    else if (genertaedCharacters[i].transform.GetChild(j).name == "Piel")
                    {
                        genertaedCharacters[i].transform.GetChild(j).GetComponent<SkinnedMeshRenderer>().material = materialesPiel[ran];
                    }
                    else if (genertaedCharacters[i].transform.GetChild(j).name == "Bata")
                    {
                        genertaedCharacters[i].transform.GetChild(j).GetComponent<SkinnedMeshRenderer>().material = bata;
                    }
                    else if (genertaedCharacters[i].transform.GetChild(j).name == "Camiseta")
                    {
                        genertaedCharacters[i].transform.GetChild(j).GetComponent<SkinnedMeshRenderer>().material = camsieta;
                    }
                    else if (genertaedCharacters[i].transform.GetChild(j).name == "Pantalones")
                    {
                        genertaedCharacters[i].transform.GetChild(j).GetComponent<SkinnedMeshRenderer>().material = pantalon;
                    }
                }
                else if (genertaedCharacters[i].transform.GetChild(j).GetComponent<MeshRenderer>() != null)
                {
                    genertaedCharacters[i].transform.GetChild(j).GetComponent<MeshRenderer>().material = materialesPelo[colorDePelo];
                }



            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(genertaedCharacters.Length);
            workerShop.setUI(genertaedCharacters);
        }
            
    }
}
