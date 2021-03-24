using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGenerator : MonoBehaviour
{
    public GameObject prefab;

    public Material[] materialesPelo;
    public Material[] materialesPiel;
    public Material[] camsieta;
    public Material[] pantalon;
    public Material[] ojos;

    public GameObject[] pelos;
    public GameObject[] peloFacial;

    private List<GameObject> genertaedCharactersList;

    public PopulateWorkerShop workerShop;

    public Material bata;

    private int generatingCount = 5;
    void Start()
    {
        genertaedCharactersList = new List<GameObject>();

        for (int i = 0; i < generatingCount; i++)
        {
            int colorDePelo = Random.Range(0,materialesPelo.Length);

            GameObject instance = Instantiate(prefab, this.transform.position + new Vector3((15 * i) + 50f, 0, 0), Quaternion.identity);
            genertaedCharactersList.Add(instance);

            int randomPelo = Random.Range(0, pelos.Length);
            
            if (randomPelo != materialesPelo.Length)
            {
                GameObject pelo = Instantiate(pelos[randomPelo], genertaedCharactersList[i].transform, false);
                pelo.name = "Pelo";
                pelo.transform.rotation = Quaternion.Euler(-90f, 0, 0);
                pelo.transform.localScale = new Vector3(1f, 1f, 1f);
                pelo.transform.localPosition = new Vector3(0f, 0f, 0f);
            }

            int randomBarba = Random.Range(0, 11);
            if (randomBarba == 0 || randomBarba == 1)
            {
                GameObject barba = Instantiate(peloFacial[randomBarba], genertaedCharactersList[i].transform, false);
                barba.name = "PeloFacial";
                barba.transform.rotation = Quaternion.Euler(-90f, 0, 0);
                barba.transform.localScale = new Vector3(1f, 1f, 1f);
                barba.transform.localPosition = new Vector3(0f, 0f, 0f);
            }




            int children = genertaedCharactersList[i].transform.childCount;
            
            for (int j = 0; j < children; ++j)
            {
                int ran = Random.Range(0, materialesPiel.Length);

                if(genertaedCharactersList[i].transform.GetChild(j).GetComponent<SkinnedMeshRenderer>() != null)
                {
                    if (genertaedCharactersList[i].transform.GetChild(j).name == "Cejas")
                    {
                        genertaedCharactersList[i].transform.GetChild(j).GetComponent<SkinnedMeshRenderer>().material = materialesPelo[colorDePelo];
                    }
                    else if (genertaedCharactersList[i].transform.GetChild(j).name == "Piel")
                    {
                        genertaedCharactersList[i].transform.GetChild(j).GetComponent<SkinnedMeshRenderer>().material = materialesPiel[ran];
                    }
                    else if (genertaedCharactersList[i].transform.GetChild(j).name == "Bata")
                    {
                        genertaedCharactersList[i].transform.GetChild(j).GetComponent<SkinnedMeshRenderer>().material = bata;
                    }
                    else if (genertaedCharactersList[i].transform.GetChild(j).name == "Camiseta")
                    {
                        int randomCamiseta = Random.Range(0, camsieta.Length);
                        genertaedCharactersList[i].transform.GetChild(j).GetComponent<SkinnedMeshRenderer>().material = camsieta[randomCamiseta];
                    }
                    else if (genertaedCharactersList[i].transform.GetChild(j).name == "Pantalones")
                    {
                        int randomPantalon = Random.Range(0, camsieta.Length);
                        genertaedCharactersList[i].transform.GetChild(j).GetComponent<SkinnedMeshRenderer>().material = pantalon[randomPantalon];
                    }
                    else if (genertaedCharactersList[i].transform.GetChild(j).name == "Ojos")
                    {
                        int ojosRandom = Random.Range(0, ojos.Length);
                        genertaedCharactersList[i].transform.GetChild(j).GetComponent<SkinnedMeshRenderer>().material = ojos[ojosRandom];
                    }
                }
                else if (genertaedCharactersList[i].transform.GetChild(j).GetComponent<MeshRenderer>() != null)
                {
                    genertaedCharactersList[i].transform.GetChild(j).GetComponent<MeshRenderer>().material = materialesPelo[colorDePelo];
                }



            }
        }

        workerShop.setUI(genertaedCharactersList);
    }


}
