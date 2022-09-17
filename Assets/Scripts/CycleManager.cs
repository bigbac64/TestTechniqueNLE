using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycleManager : MonoBehaviour
{
    public GameObject prefabBuilding;
    public GameObject prefabBuildConstruction;

    float timeCycle = 60f;
    int aerCurrent;
    int aerLastCycle;

    List<Building> buildingList;

    // Start is called before the first frame update
    void Start()
    {
        buildingList = new List<Building>();
        aerCurrent = 1000;
        aerLastCycle = 0;

        InvokeRepeating("Cycle", timeCycle, timeCycle);
    }

    void Cycle()
    {
        CountProductCycle();
    }

    public void BuyBuilding(Building bd)
    {
        if (aerCurrent < bd.Cost)
            return;

        aerCurrent -= bd.Cost;
        buildingList.Add(bd);
    }

    public void RemoveBuilding(Building bd)
    {
        buildingList.Remove(bd);
    }

    void CountProductCycle()
    {
        aerLastCycle = 0;

        foreach (Building bd in buildingList)
        {
            aerLastCycle += bd.Production();
        }
    }
}
