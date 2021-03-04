using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextPopUp : MonoBehaviour
{
    private static GameObject prefab;
    private static TextMeshPro text;

    private void Awake()
    {
        
    }
    public static void DisplayTextPopUp(Vector3 position, string textToDisplay)
    {
        Transform copy = Instantiate(GameAssets.Asset.textPrefab, position,Quaternion.identity);
        
    }

    
}
