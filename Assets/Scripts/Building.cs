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
    [SerializeField]
    Color color;

    /// <summary>
    /// Permet d'initialisé un Building
    /// </summary>
    /// <param name="buildingName">Le nom du batiment</param>
    /// <param name="revenues">La liste des revenus</param>
    /// <param name="cost">Le cout du batiment</param>
    public void Init(string buildingName, List<Revenue> revenues, int cost, Color color)
    {
        this.buildingName = buildingName;
        this.revenues = revenues;
        this.cost = cost;
        this.color = color;

        UpdateUITextName();
        UpdateUITextRevenue();
        UpdateUITextCost();
        UpdateUITextNamesRevenue();
        UpdateUIImgColor();
    }

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

    public int Cost
    {
        get { return cost; }
        set
        {
            cost = value;
            UpdateUITextCost();
        }
    }

    public Color Color
    {
        get { return color; }
        set
        {
            color = value;
            UpdateUIImgColor();
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

        return names.Substring(0, names.Length - 2);
    }

    /// <summary>
    /// Met à jour le texte du Gameobject UI nommé Name si il existe
    /// </summary>
    public void UpdateUITextName()
    {
        Transform tfName = transform.Find("Name");
        if (tfName != null)
            tfName.GetComponent<Text>().text = buildingName;
    }


    /// <summary>
    /// Met à jour le texte du Gameobject UI nommé Cost si il existe
    /// </summary>
    public void UpdateUITextCost()
    {
        Transform tfCost = transform.Find("Cost");
        if (tfCost != null)
            tfCost.GetComponent<Text>().text = cost.ToString();
    }

    /// <summary>
    /// Met à jour le texte du Gameobject UI nommé Product si il existe
    /// </summary>
    public void UpdateUITextRevenue()
    {
        Transform tfProduct = transform.Find("Product");
        if (tfProduct != null)
            tfProduct.GetComponent<Text>().text = Production().ToString();
    }

    /// <summary>
    /// Met à jour le texte du Gameobject UI nommé NamesRevenue si il existe
    /// </summary>
    public void UpdateUITextNamesRevenue()
    {
        Transform tfNamesRevenue = transform.Find("NamesRevenue");
        if (tfNamesRevenue != null)
            tfNamesRevenue.GetComponent<Text>().text = GetNamesOfRevenue();
    }

    public void UpdateUIImgColor()
    {
        Image img = GetComponent<Image>();
        if (img != null)
            img.color = color;
    }

    public void UpdateUIAll()
    {

    }
}
