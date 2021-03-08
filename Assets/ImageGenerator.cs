using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageGenerator : MonoBehaviour
{

    public Camera came;

    Camera cam;
    void Awake()
    {

        cam = came.GetComponent<Camera>();

        if(cam.targetTexture ==null)
        {
            cam.targetTexture = new RenderTexture(256, 256, 24);
        }
        else
        {
            cam.targetTexture.width = 256;
            cam.targetTexture.height = 256;
        }
        cam.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Sprite  TakePhoto()
    {
        cam.gameObject.SetActive(true);

        Texture2D snapshot = new Texture2D(256, 256, TextureFormat.RGB24, false);
        cam.Render();
        RenderTexture.active = cam.targetTexture;
        snapshot.ReadPixels(new Rect(0, 0, 256, 256), 0, 0);
        snapshot.Apply();
        byte[] bytes = snapshot.EncodeToPNG();
        string name = Name();
        //System.IO.File.WriteAllBytes(name, bytes);
        
        cam.gameObject.SetActive(false);

        Sprite mySprite = Sprite.Create(snapshot, new Rect(0.0f, 0.0f, 256, 256), new Vector2(0.5f, 0.5f), 100.0f);

        return mySprite;


    }


        
        
            
            
        
    

    private string Name()
    {
        return string.Format("{0}/Snapshots/snap_{1}x{2}_{3}.png",
            Application.dataPath,
            256,256,
            System.DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss--ff"));
    }
}
