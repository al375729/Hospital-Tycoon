using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    private Text text;
    void Start()
    {
        text = this.GetComponent<Text>();
        text.text = GlobalVariables.MONEY.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = GlobalVariables.MONEY.ToString();
    }
}
