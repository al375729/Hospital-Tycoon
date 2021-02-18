using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUIManagement : MonoBehaviour
{
    public Button buyBuilding1;
    public GameObject prefabBuilding1;

    public Button buyBuilding2;
    public GameObject prefabBuilding2;

    public Image antiClick;


    void Start()
    {
        buyBuilding1.onClick.AddListener(InstantiateBuilding);
    }

    private void InstantiateBuilding()
    {
        Vector3 mouse = Input.mousePosition;
        Instantiate(prefabBuilding1, mouse, Quaternion.identity);
        closeUIShop();
    }

    private void closeUIShop()
    {
        this.gameObject.SetActive(false);
        antiClick.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
