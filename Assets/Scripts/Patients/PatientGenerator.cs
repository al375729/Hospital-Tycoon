using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;

public class PatientGenerator : MonoBehaviour
{
    public GameObject[] prefabs;
    public GameObject[] especialPrefabs;

    public Material[] todosColores;
    public Material[] materialesPelo;
    public Material[] materialesPiel;
    public Material[] ojos;


    public GameObject[] HairStyleMen;
    public GameObject[] HairStyleWoman;
    public GameObject[] peloFacial;

    public GameObject entradaHospital;


    private int Xoffset = 0;

    public GameObject parent;

    public WatingRoom waitingRoom;

    public ImageGenerator generator;

    Vector3 positionSpawn = new Vector3(0f,0f,-95f);

    public Transform g1;
    void Start()
    {
        GameObject[] personajes = new GameObject[10];
        personajes[0] = null;

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(2))
        {
            generatePatient();
        }
    }


    void generatePatient()
    {
        int probabildiadEspecial = Random.Range(0, 10);

        if(probabildiadEspecial != 0 )
        {
            int numeroDePrefab = Random.Range(0, 8);
            int genero = Random.Range(0, 2);// 0 --> M || 1 --> F
            int colorDePelo = Random.Range(0, todosColores.Length);
            int ojosRandom = Random.Range(0, ojos.Length);
            int randomCamiseta = Random.Range(0, todosColores.Length);
            int randomPantalon = Random.Range(0, todosColores.Length);
            int randomPiel = Random.Range(0, materialesPiel.Length);
            int randomCinturon = Random.Range(0, todosColores.Length);
            int randomAdorno1 = Random.Range(0, todosColores.Length);
            int randomAdorno2 = Random.Range(0, todosColores.Length);


            GameObject instance = Instantiate(prefabs[numeroDePrefab], positionSpawn, Quaternion.identity);
            createPatient(instance);
            

            switch (numeroDePrefab)
            {
                case 0://Casual
                    instance.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = todosColores[colorDePelo];//Cejas
                    instance.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = ojos[ojosRandom];//Ojos
                    instance.transform.GetChild(2).GetComponent<SkinnedMeshRenderer>().material = todosColores[randomCamiseta];//Camiseta
                    instance.transform.GetChild(3).GetComponent<SkinnedMeshRenderer>().material = todosColores[randomPantalon];//Pantalon
                    instance.transform.GetChild(4).GetComponent<SkinnedMeshRenderer>().material = materialesPiel[randomPiel];//Piel
                    break;

                case 1://Casual2
                    instance.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = todosColores[colorDePelo];//Cejas
                    instance.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = ojos[ojosRandom];//Ojos
                    instance.transform.GetChild(2).GetComponent<SkinnedMeshRenderer>().material = todosColores[randomCamiseta];//Camiseta
                    instance.transform.GetChild(3).GetComponent<SkinnedMeshRenderer>().material = todosColores[randomPantalon];//Pantalon
                    instance.transform.GetChild(4).GetComponent<SkinnedMeshRenderer>().material = materialesPiel[randomPiel];//Piel
                    instance.transform.GetChild(5).GetComponent<SkinnedMeshRenderer>().material = todosColores[randomCinturon];//Cinturon
                    break;

                case 2://Casual3
                    instance.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = todosColores[colorDePelo];//Cejas
                    instance.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = ojos[ojosRandom];//Ojos
                    instance.transform.GetChild(2).GetComponent<SkinnedMeshRenderer>().material = todosColores[randomCamiseta];//Camiseta
                    instance.transform.GetChild(3).GetComponent<SkinnedMeshRenderer>().material = todosColores[randomPantalon];//Pantalon
                    instance.transform.GetChild(4).GetComponent<SkinnedMeshRenderer>().material = materialesPiel[randomPiel];//Piel
                    instance.transform.GetChild(5).GetComponent<SkinnedMeshRenderer>().material = todosColores[randomCinturon];//Cinturon
                    break;

                case 3://Chef
                    instance.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = todosColores[colorDePelo];//Cejas
                    instance.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = ojos[ojosRandom];//Ojos
                    instance.transform.GetChild(2).GetComponent<SkinnedMeshRenderer>().material = todosColores[randomCamiseta];//Camiseta
                    instance.transform.GetChild(3).GetComponent<SkinnedMeshRenderer>().material = todosColores[randomPantalon];//Pantalon
                    instance.transform.GetChild(4).GetComponent<SkinnedMeshRenderer>().material = materialesPiel[randomPiel];//Piel
                    instance.transform.GetChild(5).GetComponent<SkinnedMeshRenderer>().material = todosColores[randomCinturon];//Cinturon
                    break;

                case 4://Classy
                    instance.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = todosColores[colorDePelo];//Cejas
                    instance.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = ojos[ojosRandom];//Ojos
                    instance.transform.GetChild(2).GetComponent<SkinnedMeshRenderer>().material = todosColores[randomCamiseta];//Camiseta
                    instance.transform.GetChild(3).GetComponent<SkinnedMeshRenderer>().material = todosColores[randomPantalon];//Pantalon
                    instance.transform.GetChild(4).GetComponent<SkinnedMeshRenderer>().material = materialesPiel[randomPiel];//Piel
                    instance.transform.GetChild(5).GetComponent<SkinnedMeshRenderer>().material = todosColores[randomCinturon];//Cinturon
                    instance.transform.GetChild(6).GetComponent<SkinnedMeshRenderer>().material = todosColores[randomAdorno1];//Cinturon
                    
                    break;

                case 5://Soldier
                    instance.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = todosColores[colorDePelo];//Cejas
                    instance.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = ojos[ojosRandom];//Ojos
                    instance.transform.GetChild(2).GetComponent<SkinnedMeshRenderer>().material = todosColores[randomCamiseta];//Camiseta
                    instance.transform.GetChild(3).GetComponent<SkinnedMeshRenderer>().material = todosColores[randomPantalon];//Pantalon
                    instance.transform.GetChild(4).GetComponent<SkinnedMeshRenderer>().material = materialesPiel[randomPiel];//Piel
                    instance.transform.GetChild(5).GetComponent<SkinnedMeshRenderer>().material = todosColores[randomCinturon];//Cinturon
                    break;

                case 6://Suit
                    instance.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = todosColores[colorDePelo];//Cejas
                    instance.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = ojos[ojosRandom];//Ojos
                    instance.transform.GetChild(2).GetComponent<SkinnedMeshRenderer>().material = todosColores[randomCamiseta];//Camiseta
                    instance.transform.GetChild(3).GetComponent<SkinnedMeshRenderer>().material = todosColores[randomPantalon];//Pantalon
                    instance.transform.GetChild(4).GetComponent<SkinnedMeshRenderer>().material = materialesPiel[randomPiel];//Piel
                    instance.transform.GetChild(5).GetComponent<SkinnedMeshRenderer>().material = todosColores[randomCinturon];//Cinturon
                    instance.transform.GetChild(6).GetComponent<SkinnedMeshRenderer>().material = todosColores[randomAdorno1];//Cinturon
                    break;

                case 7://Worker
                    instance.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = todosColores[colorDePelo];//Cejas
                    instance.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = ojos[ojosRandom];//Ojos
                    instance.transform.GetChild(2).GetComponent<SkinnedMeshRenderer>().material = todosColores[randomCamiseta];//Camiseta
                    instance.transform.GetChild(3).GetComponent<SkinnedMeshRenderer>().material = todosColores[randomPantalon];//Pantalon
                    instance.transform.GetChild(4).GetComponent<SkinnedMeshRenderer>().material = materialesPiel[randomPiel];//Piel
                    instance.transform.GetChild(5).GetComponent<SkinnedMeshRenderer>().material = todosColores[randomCinturon];//Cinturon
                    break;


            }

            if (genero == 0)
            {
                
                string name = Names.getNameMale();
                instance.GetComponent<Patient>().name = name;
                instance.GetComponent<Patient>().gender = "Male";
                instance.name = name;
                int randomPelo = Random.Range(0, HairStyleMen.Length);

                if (randomPelo != todosColores.Length)
                {
                    GameObject pelo = Instantiate(HairStyleMen[randomPelo], instance.transform, false);
                    pelo.GetComponent<MeshRenderer>().material = todosColores[colorDePelo];//Ojos
                    pelo.name = "Pelo";
                    pelo.transform.rotation = Quaternion.Euler(-90f, 0, 0);
                    pelo.transform.localScale = new Vector3(1f, 1f, 1f);
                    pelo.transform.localPosition = new Vector3(0f, 0f, 0f);
                }

                int randomBarba = Random.Range(0, 11);

                if (randomBarba == 0 || randomBarba == 1)
                {
                    GameObject barba = Instantiate(peloFacial[randomBarba], instance.transform, false);
                    barba.GetComponent<MeshRenderer>().material = todosColores[colorDePelo];//Ojos
                    barba.name = "PeloFacial";
                    barba.transform.rotation = Quaternion.Euler(-90f, 0, 0);
                    barba.transform.localScale = new Vector3(1f, 1f, 1f);
                    barba.transform.localPosition = new Vector3(0f, 0f, 0f);
                }
                instance.transform.position = g1.transform.position;
                instance.transform.localScale = new Vector3(10f, 10f, 10f);
                instance.GetComponent<Patient>().sprite = generator.TakePhoto();
                instance.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                instance.transform.position = new Vector3(-103f, 1f, -103f);
                instance.transform.rotation = Quaternion.identity;
            }

            else
            {
                string name = Names.getNameFemale();
                instance.GetComponent<Patient>().name = name;
                instance.GetComponent<Patient>().gender = "Female";
                instance.name = name;

                int randomPelo = Random.Range(0, HairStyleWoman.Length);
                GameObject pelo = Instantiate(HairStyleWoman[randomPelo], instance.transform, false);
                pelo.GetComponent<MeshRenderer>().material = todosColores[colorDePelo];//Ojos
                pelo.name = "Pelo";
                pelo.transform.rotation = Quaternion.Euler(-90f, 0, 0);
                pelo.transform.localScale = new Vector3(1f, 1f, 1f);
                pelo.transform.localPosition = new Vector3(0f, 0f, 0f);

                instance.transform.position = g1.transform.position;
                instance.transform.localScale = new Vector3(10f, 10f, 10f);
                instance.GetComponent<Patient>().sprite = generator.TakePhoto();
                instance.transform.position = positionSpawn;
                instance.transform.rotation = Quaternion.identity;
                instance.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                instance.transform.position = new Vector3(-103f, 1f, -103f);
                instance.transform.rotation = Quaternion.identity;
            }        
        }

        else
        {
            Debug.Log("Aparecio un especial");
            int especial = Random.Range(0, especialPrefabs.Length);
            GameObject instance = Instantiate(especialPrefabs[especial], positionSpawn, Quaternion.identity);
            createPatient(instance);

            string name = Names.getNameMale();
            instance.GetComponent<Patient>().name = name;
            instance.name = name;

            instance.transform.position = g1.transform.position;
            instance.transform.localScale = new Vector3(10f, 10f, 10f);
            instance.GetComponent<Patient>().sprite = generator.TakePhoto();
            instance.transform.position = positionSpawn;
            instance.transform.rotation = Quaternion.identity;
            instance.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        

    }

    private void createPatient(GameObject instance)
    {
        instance.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        instance.transform.position = new Vector3(-103f, 1f, -103f);
        instance.transform.rotation = Quaternion.identity;
        instance.transform.SetParent(parent.transform);

        NavMeshAgent agente = instance.GetComponent<NavMeshAgent>();
        agente.enabled = true;

        //agente.destination = entradaHospital.transform.position;

        
    }
}

