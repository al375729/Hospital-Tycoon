using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diseases : MonoBehaviour
{
    public static Diseases Instance { get; private set; } // static singleton

    public static List<Disease> diseasesLevel1;
    public static List<Disease> diseasesLevel2;
    public static List<Disease> diseasesLevel3;
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
        diseasesLevel3 = new List<Disease>(1);


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
        Disease BrazoRoto = new Disease("Brazo roto", steps, 2);
        diseasesLevel2.Add(BrazoRoto);

        Disease PiernaRota = new Disease("Pierna rota", steps, 2);
        diseasesLevel2.Add(PiernaRota);

        Disease ManoRota = new Disease("Mano rota", steps, 2);
        diseasesLevel2.Add(ManoRota);

        Disease PieRoto = new Disease("Pie roto", steps, 2);
        diseasesLevel2.Add(PieRoto);

        Disease Esguince = new Disease("Esguince", steps, 2);
        diseasesLevel2.Add(Esguince);

        steps.Enqueue("Analisis");
        Disease InfeccionOrina = new Disease("Infeccion de orina", steps, 3);
        diseasesLevel3.Add(InfeccionOrina);
    }

    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static Disease GetDiseaseLevel3(int random)
    {
        Disease disease = diseasesLevel3[random];
        return disease;

    }
    public static Disease GetDiseaseLevel2(int random)
    {
       Disease disease = diseasesLevel2[random];
            return disease;
        
    }

    public static Disease GetDiseaseLevel1(int random)
    {
        Disease disease = diseasesLevel1[random];
        return disease;

    }
}
