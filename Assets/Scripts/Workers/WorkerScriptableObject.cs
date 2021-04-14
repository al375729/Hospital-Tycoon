using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WorkerObjects", menuName = "ScriptableObjects/WorkerObjects")]
public class WorkerScriptableObject : ScriptableObject
{
#pragma warning disable CS0108 // El miembro oculta el miembro heredado. Falta una contraseña nueva
    public string name;
#pragma warning restore CS0108 // El miembro oculta el miembro heredado. Falta una contraseña nueva
    public Sprite image;
    public string description;
    public GameObject prefab;
}



