using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ToolTip : MonoBehaviour
{
    public TextMeshProUGUI header;
    public TextMeshProUGUI description;

    public LayoutElement layoutElement;

    public int characterWrapLimit;

    RectTransform rectTransform;
    // Start is called before the first frame update

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    void Start()
    {
        
    }

    public void setText(string header, string description)
    {
        if (string.IsNullOrEmpty(header))
        {
            this.header.gameObject.SetActive(false);
        }
        else
        {
            this.description.gameObject.SetActive(true);
            this.header.text = header;
        }

        this.description.text = description;
    }
    // Update is called once per frame
    void Update()
    {
        int headerLength = header.text.Length;
        int descriptionLength = description.text.Length;

        if (headerLength > characterWrapLimit || descriptionLength > characterWrapLimit)
        {
            layoutElement.enabled = true;
        }
        else layoutElement.enabled = false;

        Vector2 mousePosition = Input.mousePosition;

        

        float pivotX = mousePosition.x / Screen.width;
        float pivotY = mousePosition.y / Screen.height;

        rectTransform.pivot = new Vector2(pivotX, pivotY);
        transform.position = mousePosition;
        
    }
}
