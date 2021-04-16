using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PatientInfo : MonoBehaviour
{
    public static Text name;
    public static Text gender;
    public static GameObject panel;

    void Start()
    {
        panel = this.gameObject;
        name = transform.GetChild(0).GetComponent<Text>();
        gender = transform.GetChild(1).GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void showPanel()
    {
        Color tempColor = panel.GetComponent<Image>().color;
        tempColor.a = 1f;
        panel.gameObject.GetComponent<Image>().color = tempColor;

        name.gameObject.SetActive(true);
        gender.gameObject.SetActive(true);

    }

    public static void disablePanel()
    {

        Color tempColor = panel.GetComponent<Image>().color;
        tempColor.a = 0f;
        panel.gameObject.GetComponent<Image>().color = tempColor;

        name.gameObject.SetActive(false);
        gender.gameObject.SetActive(false);
    }
}
