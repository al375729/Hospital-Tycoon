using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenShopButton : MonoBehaviour
{
    private Button button;
    public EditModeButton editModeButton;
    public DeleteModeButton deleteModeButton;
    public GameObject antiClick;
    public GameObject shop;
    void Start()
    {
        button = this.gameObject.GetComponent<Button>();
        button.onClick.AddListener(onButtonPressed);
    }

    private void onButtonPressed()
    {
        editModeButton.setEditMode(false);
        deleteModeButton.setDeleteMode(false);
        shop.SetActive(true);
        antiClick.SetActive(true);
        GlobalVariables.UI_OPEN = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
