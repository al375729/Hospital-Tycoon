using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Console : MonoBehaviour
{
    private static Text console;
    private static float time = 2.0f;
    private static GameObject go;
    public static GameObject img;

    private static LTDescr delay;

    private void Start()
    {
        go = this.gameObject;
        console = GetComponent<Text>();
        img = this.gameObject.transform.parent.GetChild(0).gameObject;
    }
    
    public static void setText(string texto)
    {
        Color colorTrigger = img.GetComponent<Image>().color;
        colorTrigger.a = 0f;
        img.GetComponent<Image>().color = colorTrigger;


        LeanTween.cancel(console.gameObject);
        LeanTween.cancel(img);
        RemoveMe();

        Color color = console.color;
        color.a = 1f;
        console.color = color;
        console.text = texto;

        console.color = new Color(255f,0f,0f);

        colorTrigger = img.GetComponent<Image>().color;
        colorTrigger.a = 1f;
        img.GetComponent<Image>().color = colorTrigger;

        LeanTween.alpha(img.GetComponent<RectTransform>(), 0f, 0.1f).setDelay(0f).setOnComplete(delete); ;


        delay = LeanTween.alphaText(console.rectTransform, 0, 1f).setDelay(2f).setOnComplete(RemoveMe);
        

    }

    public static void RemoveMe() 
    {
        Color color = console.color;
        color.a = 0f;
        console.color = color;
        console.text = "";
    }
    
     public static void delete() 
    {
        Color colorTrigger = img.GetComponent<Image>().color;
        colorTrigger.a = 0f;
        img.GetComponent<Image>().color = colorTrigger;
    }

}
