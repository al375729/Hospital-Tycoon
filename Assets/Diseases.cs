using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diseases : MonoBehaviour
{
    public static Diseases Instance { get; private set; } // static singleton

    public static List<Disease> diseasesLevel1;
    public static List<Disease> diseasesLevel2;
    public List<Disease> diseasesLevel3;
    public List<Disease> diseasesLevel4;
    public List<Disease> diseasesLevel5;
    public class Disease
    {
        public string name;

        public Queue<string> tasks;

        public int stars;


        public Disease(string name,Queue<string> tasks, int stars)
        {
            this.name = name;
            this.tasks = tasks;
            this.stars = stars;
        }
    }

    void Awake()
    {
        diseasesLevel1 = new List<Disease>(5);
        diseasesLevel2 = new List<Disease>(3);


        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }

        Queue<string> steps = new Queue<string>(0);

        Disease fiebre = new Disease("Fiebre", steps, 1);
        diseasesLevel1.Add(fiebre);

        Disease dolorArticular = new Disease("Dolor articular", steps, 1);
        diseasesLevel1.Add(dolorArticular);

        Disease mareo = new Disease("Mareo", steps, 1);
        diseasesLevel1.Add(mareo);

        Disease gripe = new Disease("Gripe", steps, 1);
        diseasesLevel1.Add(gripe);

        Disease GastroInteritis = new Disease("Gastrointeritis", steps, 1);
        diseasesLevel1.Add(GastroInteritis);

        steps.Enqueue("Radiologia");
        Disease HuesoRoto = new Disease("Hueso roto", steps, 2);
        diseasesLevel2.Add(HuesoRoto);
        diseasesLevel2.Add(HuesoRoto);
        diseasesLevel2.Add(HuesoRoto);
    }

    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Disease GetDisease()
    {
        int ran = Random.Range(0, 101);

        if(ran<80)
        {
            Disease disease = diseasesLevel1[Random.Range(0, diseasesLevel1.Count)];
            return disease;
        }
        else 
        {
            Disease disease = diseasesLevel2[Random.Range(0, diseasesLevel2.Count)];
            return disease;
        }
    }
}
