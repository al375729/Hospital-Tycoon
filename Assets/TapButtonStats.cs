using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TapButtonStats : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public TabGroupStats buttonGroup;

    public TextMeshProUGUI text;

    public Image image;

    public void OnPointerClick(PointerEventData eventData)
    {
        buttonGroup.OnSelected(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonGroup.OnEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonGroup.OnExit(this);
    }

    private void Start()
    {
        text = text.GetComponent<TextMeshProUGUI>();
        buttonGroup.Suscribe(this);
        image = this.GetComponent<Image>();
    }


}
