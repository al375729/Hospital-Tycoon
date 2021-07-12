using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingButton : MonoBehaviour
{
    private Button button;
    public GameObject antiClick;
    public GameObject panel;
    void Start()
    {
        button = this.gameObject.GetComponent<Button>();
        button.onClick.AddListener(onButtonPressed);
    }

    private void onButtonPressed()
    {
        panel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
