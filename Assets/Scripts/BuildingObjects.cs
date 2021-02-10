using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingObjects", menuName = "ScriptableObjects/BuildingObjects")]
public class BuildingObjects : ScriptableObject
{
    public bool symetric;
    public string buildingName;
    public GameObject prefab;
    public int width;
    public int height;

    public int[] xValues;
    public int[] yValues;

}
