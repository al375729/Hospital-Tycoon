using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarcaMedico : MonoBehaviour
{
    ConsultController consultController;
    RadiologyController radiologyController;
    AnalisisController analisisController;

    void Start()
    {
        consultController = ConsultController.Instance;
        radiologyController = RadiologyController.Instance;
        analisisController = AnalisisController.Instance;
        //addReferences();
    }

    // Update is called once per frame
    void Update()
    {
    }


    public void addReferences()
    {
        
            if (this.GetComponent<ObjectsOnRoom>() != null)
            {
                ObjectsOnRoom obj = this.GetComponent<ObjectsOnRoom>();

                int index;

                switch (obj.objectType)
                {
                    case ObjectsOnRoom.type.ConsultDoctor:
                        index = consultController.addDoctor(this.transform);
                        obj.indexInList = index;
                        break;

                    case ObjectsOnRoom.type.ConsultPatient:
                        index = consultController.addPatient(this.transform);
                        obj.indexInList = index;
                        break;

                    case ObjectsOnRoom.type.None:
                        break;

                    case ObjectsOnRoom.type.RadiologyDoctor:
                        index = radiologyController.addDoctor(this.transform);
                        obj.indexInList = index;
                        break;

                    case ObjectsOnRoom.type.RadiologyPatient:
                        index = radiologyController.addPatient(this.transform);
                        obj.indexInList = index;
                        break;

                    case ObjectsOnRoom.type.AnalysisDoctor:
                        index = analisisController.addDoctor(this.transform);
                        obj.indexInList = index;
                        break;

                    case ObjectsOnRoom.type.AnalysisPatient:
                        index = analisisController.addPatient(this.transform);
                        obj.indexInList = index;
                        break;

                }
            }
        
    }
}
