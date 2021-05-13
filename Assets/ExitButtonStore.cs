using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ExitButtonStore : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Color textColorIdle;
    public Color textColorHover;
    public Color textColorSelected;

    public Color imageColorIdle;
    public Color imageColorHover;
    public Color imageColorSelected;

    public TextMeshProUGUI text;
    public Image image;

    public TabGroup buttons;

    public GameObject antiClick;
    public GameObject shop;

    public GameObject button;

    public void OnPointerClick(PointerEventData eventData)
    {


        shop.SetActive(false);
        GlobalVariables.UI_OPEN = false;
        antiClick.SetActive(false);

        buttons.ResetAll();

        text.color = textColorIdle;
        image.color = imageColorIdle;

        button.GetComponent<Image>().color = Color.white;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.color = textColorHover;
        image.color = imageColorHover;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.color = textColorIdle;
        image.color = imageColorIdle;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
