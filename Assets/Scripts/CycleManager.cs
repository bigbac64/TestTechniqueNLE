using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycleManager : MonoBehaviour
{
    float timeCycle = 60f;
    int aerCurrent;
    int aerLastCycle;


    // Start is called before the first frame update
    void Start()
    {
        aerCurrent = 1000;
        aerLastCycle = 0;

        InvokeRepeating("Cycle", timeCycle, timeCycle);
    }

    void Cycle()
    {
    }

    public void BuyBuilding(Building bd)
    {
        if (aerCurrent < bd.Cost)
            return;

        aerCurrent -= bd.Cost;
    }


}
