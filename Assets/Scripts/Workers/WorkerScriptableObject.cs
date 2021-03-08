using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WorkerObjects", menuName = "ScriptableObjects/WorkerObjects")]
public class WorkerScriptableObject : ScriptableObject
{
    public string name;
    public Sprite image;
    public string description;
    public GameObject prefab;
}



