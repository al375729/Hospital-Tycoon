using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{

    private static List<GameObject> buildingsInGame;

    private void Start()
    {
        List<GameObject> buildingsInGame = new List<GameObject>(30);
    }
}
