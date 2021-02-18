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
    void Start()
    {
        button = this.gameObject.GetComponent<Button>();
        button.onClick.AddListener(onButtonPressed);
    }

    private void onButtonPressed()
    {
        editModeButton.setEditMode(false);
        deleteModeButton.setDeleteMode(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
