using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testing1 : MonoBehaviour
{


    public GameObject panel;

    public GameObject g1;
    public GameObject g2;


    public Button buttonTemplate;
    public ImageGenerator generator;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setUI(GameObject[] genertaedCharacters)
    {

        for (int i = 0; i < genertaedCharacters.Length; i++)
        {
            Debug.Log(genertaedCharacters[i]);
            genertaedCharacters[i].transform.position = g1.transform.position;

            Button instance;

            instance = Instantiate(buttonTemplate, panel.transform);
            instance.gameObject.transform.GetChild(0).GetComponent<Text>().text = genertaedCharacters[i].name;
            instance.gameObject.transform.GetChild(1).GetComponent<Text>().text = "algo";
            instance.gameObject.transform.GetChild(2).GetComponent<Image>().sprite = generator.TakePhoto();

            genertaedCharacters[i].transform.position = g2.transform.position;
        }
    }

}
