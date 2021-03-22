using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setUI(GameObject[] genertaedCharacters)
    {
        characters = genertaedCharacters;
        for (int i = 0; i < genertaedCharacters.Length; i++)
        {
            genertaedCharacters[i].transform.position = g1.transform.position;

            Button instance;

            instance = Instantiate(buttonTemplate, panel.transform);
            instance.gameObject.transform.GetChild(0).GetComponent<Text>().text = genertaedCharacters[i].name;
            instance.gameObject.transform.GetChild(1).GetComponent<Text>().text = "algo";
            instance.gameObject.transform.GetChild(2).GetComponent<Image>().sprite = generator.TakePhoto();

            genertaedCharacters[i].transform.position = g2.transform.position;

            instance.GetComponent<Button>().AddEventListener(genertaedCharacters[i], 2, SpawnBuilding);
        }
    }

    void SpawnBuilding(GameObject prefab, int price)
    {
        if (GlobalVariables.MONEY > price)
        {
            shop.SetActive(false);
            GlobalVariables.UI_OPEN = false;
            antiClick.SetActive(false);

            GlobalVariables.MONEY -= price;

            shopButton.image.color = Color.white;

            prefab.gameObject.transform.position = new Vector3(10,1,10);
            prefab.gameObject.transform.rotation = Quaternion.identity;
            prefab.gameObject.transform.localScale = new Vector3(1.5f, 1.5f,1.5f);

            buttons.ResetAll();
        }

    }

}
