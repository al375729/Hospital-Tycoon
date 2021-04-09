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


    private void Start()
    {
        go = this.gameObject;
        console = GetComponent<Text>();
    }
    
    public static void setText(string texto)
    {
        Color color = console.color;
        color.a = 1f;
        console.color = color;
        console.text = texto;

        LeanTween.alphaText(console.rectTransform, 0, 1f).setDelay(3f);

    }

}
