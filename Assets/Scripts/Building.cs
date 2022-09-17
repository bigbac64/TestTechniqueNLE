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

    // renvoie la somme des sources de revenue
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
            transform.Find("Cost").GetComponent<Text>().text = value.ToString();
        }
    }

    // Met à jour le texte du Gameobject UI nommé Name
    public void UpdateUITextName()
    {
        transform.Find("Name").GetComponent<Text>().text = buildingName;
    }


    // Met à jour le texte du Gameobject UI nommé Cost
    public void UpdateUITextCost()
    {
        transform.Find("Cost").GetComponent<Text>().text = cost.ToString();
    }

    // ajoute une source de revenue au batiment si elle n'est pas déjà présente dans la liste
    public void AddRevenue(Revenue rev)
    {
        if (revenues.Contains(rev))
            return;

        revenues.Add(rev);
        UpdateUITextRevenue();
    }

    // retire une source de revenue au batiment 
    public void RemoveRevenue(Revenue rev)
    {
        revenues.Remove(rev);
        UpdateUITextRevenue();
    }

    // Met à jour le texte du Gameobject UI nommé Product
    public void UpdateUITextRevenue()
    {
        transform.Find("Product").GetComponent<Text>().text = Production().ToString();
    }
}
