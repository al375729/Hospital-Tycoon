using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{

    private static GameAssets asset;

    public static GameAssets Asset
    {
        get
        {
            if (asset == null) asset = Instantiate(Resources.Load<GameAssets>("GameAssets"));
            return asset;
        }
    }

    public Transform textPrefab;
}
