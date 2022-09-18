using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    [SerializeField]
    string buildingName;
    [SerializeField]
    List<Revenue> revenues;
    [SerializeField]
    int cost;

    /// <summary>
    /// Calcule la production d'Aer selon les revenues associé
    /// </summary>
    /// <returns>le total des revenues</returns>
    public int Production()
    {
        int prod = 0;
        for (int i = 0; i < revenues.Count; i++)
        {
            prod += (int) revenues[i]; 
        }
        return prod;
    }

    
    public string BuildingName
    {
        get { return buildingName; }
        set { 
            buildingName = value;
            UpdateUITextName();
        }
    }

    public List<Revenue> Revenues
    {
        get { return revenues; }
        set
        {
            revenues = value;
            UpdateUITextRevenue();
        }
    }

    /// <summary>
    /// Permet de générer la liste des noms de revenu au format string
    /// </summary>
    /// <returns>la liste des noms de revenu</returns>
    public string GetNamesOfRevenue()
    {
        if (revenues.Count == 0)
            return "";

        string names = "";

        foreach (Revenue revenue in revenues)
        {
            names += Enum.GetName(typeof(Revenue), revenue) + ", ";
        }

        return names.Substring(0, names.Length-2); 
    }

    public int Cost
    {
        get { return cost; }
        set
        {
            cost = value;
            transform.Find("Cost").GetComponent<Text>().text = value.ToString();
        }
    }

    /// <summary>
    /// Met à jour le texte du Gameobject UI nommé Name
    /// </summary>
    public void UpdateUITextName()
    {
        transform.Find("Name").GetComponent<Text>().text = buildingName;
    }


    /// <summary>
    /// Met à jour le texte du Gameobject UI nommé Cost
    /// </summary>
    public void UpdateUITextCost()
    {
        transform.Find("Cost").GetComponent<Text>().text = cost.ToString();
    }

    /// <summary>
    /// Met à jour le texte du Gameobject UI nommé Product
    /// </summary>
    public void UpdateUITextRevenue()
    {
        transform.Find("Product").GetComponent<Text>().text = Production().ToString();
    }
}
