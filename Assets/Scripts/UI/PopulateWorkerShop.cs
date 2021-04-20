using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;




public class PopulateWorkerShop : MonoBehaviour
{


    public GameObject panel;

    public GameObject g1;
    public GameObject g2;


    public Button buttonTemplate;
    public ImageGenerator generator;

    GameObject[] characters;

    public TabGroup buttons;

    public GameObject antiClick;
    public GameObject shop;
    public Button shopButton;
    private Button buttonPressed;

    public Sprite star;
    List<GameObject> genertaedCharacters;

    void Start()
    {

    }

    public static class ButtonExtension
{
}

    // Update is called once per frame
    void Update()
    {
        
    }

    public void deleteUI()
    {
        if (this.gameObject.transform.childCount != 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
    }

    public void setUI(List<GameObject> charactersList)
    {
        Debug.Log("popu");

        genertaedCharacters = charactersList;
        for (int i = 0; i < genertaedCharacters.Count; i++)
        {
            genertaedCharacters[i].transform.position = g1.transform.position;

            Button instance;

            instance = Instantiate(buttonTemplate, panel.transform);
            instance.gameObject.transform.GetChild(0).GetComponent<Text>().text = genertaedCharacters[i].name;
            instance.gameObject.transform.GetChild(1).GetComponent<Text>().text = genertaedCharacters[i].GetComponent<Worker>().getType();
            instance.gameObject.transform.GetChild(2).GetComponent<Image>().sprite = generator.TakePhoto();

            for (int j = 0; j < genertaedCharacters[i].GetComponent<Worker>().stars; j++)
            {
                Color tempColor = instance.gameObject.transform.GetChild(3 + j).GetComponent<Image>().color;
                tempColor.a = 1f;
                instance.gameObject.transform.GetChild(j+3).GetComponent<Image>().color = tempColor;

            }

            int ranBonuses = Random.Range(0, 10);
            Color tempColor2;

            switch (ranBonuses)
            {
               
                case 0:
                     tempColor2 = instance.gameObject.transform.GetChild(instance.gameObject.transform.childCount -3).GetComponent<Image>().color;
                    tempColor2.a = 1f;
                    instance.gameObject.transform.GetChild(instance.gameObject.transform.childCount -3).GetComponent<Image>().color = tempColor2;

                    genertaedCharacters[i].GetComponent<Worker>().walkingSpeedBonus = 3;
                    break;

                case 1:
                     tempColor2 = instance.gameObject.transform.GetChild(instance.gameObject.transform.childCount - 2).GetComponent<Image>().color;
                    tempColor2.a = 1f;
                    instance.gameObject.transform.GetChild(instance.gameObject.transform.childCount - 2).GetComponent<Image>().color = tempColor2;

                    genertaedCharacters[i].GetComponent<Worker>().treatingSpeedBonus = 9; 
                    break;

                case 2:
                     tempColor2 = instance.gameObject.transform.GetChild(instance.gameObject.transform.childCount - 1).GetComponent<Image>().color;
                    tempColor2.a = 1f;
                    instance.gameObject.transform.GetChild(instance.gameObject.transform.childCount - 1).GetComponent<Image>().color = tempColor2;

                    genertaedCharacters[i].GetComponent<Worker>().moneyBonus = 15;
                    break;
            }
            genertaedCharacters[i].transform.position = g2.transform.position;

            instance.GetComponent<Button>().AddEventListener(i, 2, SpawnBuilding);
        }
    }

    void SpawnBuilding(int i, int price)
    {
        Destroy(gameObject.transform.GetChild(i).gameObject);
        GameObject prefab = genertaedCharacters[i];

        genertaedCharacters.RemoveAt(i);
        deleteUI();
        setUI(genertaedCharacters);


        if (GlobalVariables.MONEY > price)
        {
            shop.SetActive(false);
            GlobalVariables.UI_OPEN = false;
            antiClick.SetActive(false);

            GlobalVariables.MONEY -= price;

            shopButton.image.color = Color.white;

            prefab.gameObject.transform.position = new Vector3(5,0.05f,-98f);
            prefab.gameObject.transform.rotation = Quaternion.identity;
            prefab.gameObject.transform.localScale = new Vector3(1.5f, 1.5f,1.5f);
            
            prefab.gameObject.AddComponent<NavMeshAgent>();
            NavMeshAgent navAgent = prefab.gameObject.GetComponent<NavMeshAgent>();
            navAgent.baseOffset = 0.5f;
            navAgent.speed = 7;
            navAgent.angularSpeed = 15;
            navAgent.stoppingDistance = 0.1f;
            navAgent.acceleration = 5f;
            navAgent.radius = 0.1f;
            navAgent.height = 3.5f;
            navAgent.autoBraking = true;
            //navAgent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;

            //prefab.gameObject.AddComponent<WorkerAI>();

            Worker worker = prefab.gameObject.GetComponent<Worker>();
            worker.setWorking(true);
    
            buttons.ResetAll();
        }

    }

}
