using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTipSystem : MonoBehaviour
{
    private static ToolTipSystem current;

    private void Awake()
    {
        current = this;
    }

    public ToolTip toolTip;

    public static void Show(string header, string description)
    {
        current.toolTip.setText(header,description);
        current.toolTip.gameObject.SetActive(true);
    }

    public static void Hide()
    {
        current.toolTip.gameObject.SetActive(false);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
