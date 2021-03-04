using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class ButtonExtension
{
    public static void AddEventListener<T,W>(this Button button, T param,W param2, Action<T,W> OnClick)
    {
        button.onClick.AddListener(delegate()
       {
           OnClick(param,param2);
       });
    }
}


public class ShopUIManagement : MonoBehaviour
{
    public BuildingObjects[] arrayOfBuildings;
    public Button exit;
    public GameObject antiClick;
    public GameObject shop;

    public Transform parent;

    public Button buttonTemplate;

    public Button shopButton;

    
    
    void Start()
    {
        Button instance;

        for (int i = 0; i < arrayOfBuildings.Length; i++)
        {
            instance = Instantiate(buttonTemplate, transform);
            instance.gameObject.transform.GetChild(0).GetComponent<Text>().text = arrayOfBuildings[i].buildingName;
            instance.gameObject.transform.GetChild(1).GetComponent<Text>().text = arrayOfBuildings[i].description;
            instance.gameObject.transform.GetChild(2).GetComponent<Image>().sprite = arrayOfBuildings[i].image;

            instance.GetComponent<Button>().AddEventListener(arrayOfBuildings[i].prefab, arrayOfBuildings[i].price, SpawnBuilding);


        }
    }

    void SpawnBuilding(GameObject prefab, int price)
    {
        if(GlobalVariables.MONEY > price)
        {
            shop.SetActive(false);
            GlobalVariables.UI_OPEN = false;
            antiClick.SetActive(false);

            GlobalVariables.MONEY -= price;

            shopButton.image.color = Color.white;

            Debug.Log(Input.mousePosition);
            Instantiate(prefab, Input.mousePosition, Quaternion.identity);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
