using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Diagnostics.Contracts;

public class ControlUIBuilding : MonoBehaviour
{
    public GameObject prefabBuilding;
    public List<GameObject> buildings;

    private void Start()
    {
        UpdateUIBuilding();
    }

    /// <summary>
    /// Mise à jour de l'affichage des buildings dans la scène
    /// </summary>
    public void UpdateUIBuilding()
    {
        float adaptedPosX = 0;
        Transform parentBuildings = transform.Find("Viewport/Content");
        RectTransform prefabRect = prefabBuilding.GetComponent<RectTransform>();

        // on parcour l'ensemble des buildings créé dans la scene
        int nbChilds = parentBuildings.childCount;
        Building[] savePresetBulding = new Building[nbChilds];
        for (int i = nbChilds - 1; i >= 0; i--)
        {
            // On sauvegarder les buildings déjà créé
            savePresetBulding[i] = parentBuildings.GetChild(i).GetComponent<Building>();
            // Puis on les supprimers
            DestroyImmediate(parentBuildings.GetChild(i).gameObject);
        }

        for (int i = 0; i < buildings.Count; i++)
        {
            // On créer les nouveaux buildings
            GameObject build = Instantiate(prefabBuilding, parentBuildings);
            buildings[i] = build;

            
            // On ajoute les anciens preset 
            if (i < nbChilds)
            {
                Building actualBuilding = build.GetComponent<Building>();
                actualBuilding.BuildingName = savePresetBulding[i].BuildingName;
                actualBuilding.Cost = savePresetBulding[i].Cost;
                actualBuilding.Revenues = savePresetBulding[i].Revenues;
            }

            // On reposition le building en fonction de sa position dans la liste
            RectTransform rect = build.GetComponent<RectTransform>();
            adaptedPosX = (rect.offsetMin.x * (i+1) + rect.offsetMax.x * i) ;

            rect.offsetMin = new Vector2(adaptedPosX, rect.offsetMin.y);

            rect.sizeDelta = new Vector2(prefabRect.sizeDelta.x, rect.sizeDelta.y);

            // On ajoute un listener au bouton pour créer une construction
            build
                .GetComponent<Button>()
                .onClick
                .AddListener(delegate {
                    GameObject
                    .Find("PanelBuilder")
                    .GetComponent<ControlUIConstructor>()
                    .MakeConstruction(build.GetComponent<Building>());
                });
        }

        // On adapte la position du gameobject Content afin d'adapter le scrolling horizontal
        RectTransform parentBuildingsRect = parentBuildings.GetComponent<RectTransform>();
        parentBuildingsRect.sizeDelta = new Vector2 (adaptedPosX + prefabRect.offsetMax.x, parentBuildingsRect.sizeDelta.y);
    }
}
