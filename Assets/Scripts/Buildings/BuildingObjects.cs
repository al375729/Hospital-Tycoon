using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "BuildingObjects", menuName = "ScriptableObjects/BuildingObjects")]
public class BuildingObjects : ScriptableObject
{
    public string buildingName;
    public Sprite image;
    public string description;
    public int price;
    public GameObject prefab;

}
