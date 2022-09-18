using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlUICycle : MonoBehaviour
{
    public float timeCycle = 60f;
    [SerializeField]
    int aerCurrent;
    int aerLastCycle;


    void Start()
    {
        aerCurrent = 1000;
        aerLastCycle = 0;

        UpdateUIAer();
        UpdateUILastCycle();

        InvokeRepeating("Cycle", timeCycle, timeCycle);
    }

    /// <summary>
    /// Cycle du jeu qui ajout la recette produite lors du dernier cycle
    /// </summary>
    void Cycle()
    {
        aerLastCycle = Takings();
        aerCurrent += aerLastCycle;

        UpdateUIAer();
        UpdateUILastCycle();
    }

    /// <summary>
    /// Met à jour le texte du Gameobject UI nommé Aer si il existe
    /// </summary>
    void UpdateUIAer()
    {
        Transform tfAer = transform.Find("Aer");
        if (tfAer != null)
            tfAer.GetComponent<Text>().text = aerCurrent.ToString() + " Aer";
    }

    /// <summary>
    /// Met à jour le texte du Gameobject UI nommé LastCycle si il existe
    /// </summary>
    void UpdateUILastCycle()
    {
        Transform tfLastCycle = transform.Find("LastCycle");
        if (tfLastCycle != null)
            tfLastCycle.GetComponent<Text>().text = aerLastCycle.ToString() + " Aer";
    }

    public int AerCurrent
    {
        get { return aerCurrent; }
    }

    /// <summary>
    /// Calcule la depense sur le nombre d'aer courrant
    /// </summary>
    /// <param name="cost">la dépense d'aer à effectuer</param>
    public void Outlay(int cost)
    {
        aerCurrent -= cost;
        UpdateUIAer();
    }


    /// <summary>
    /// Calcule la recette géneré de tout les batiment construit
    /// </summary>
    /// <returns>la recette généré par les batiments construit</returns>
    public int Takings()
    {
        ControlUIConstructor economy = GameObject.Find("PanelBuilder").GetComponent<ControlUIConstructor>();

        int takingsThisCycle = 0;
        foreach (GameObject buildingConstructed in economy.Constructions)
        {
            takingsThisCycle += buildingConstructed.GetComponent<Building>().Production();
        }
        return takingsThisCycle;
    }

}
