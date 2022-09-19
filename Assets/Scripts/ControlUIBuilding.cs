using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Diagnostics.Contracts;

public class ControlUIBuilding : MonoBehaviour
{
    public GameObject prefabBuilding;
    public List<GameObject> buildings;
    public Color errorColor;
    public float errorTime;

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
                actualBuilding.Init(
                    savePresetBulding[i].BuildingName, 
                    savePresetBulding[i].Revenues, 
                    savePresetBulding[i].Cost, 
                    savePresetBulding[i].Color);
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
                    Building building = build.GetComponent<Building>();

                    if (!Purchase(building.Cost))
                    {
                        StartCoroutine(ErrorSignal(build.GetComponent<Image>()));
                        return;
                    }

                    GameObject
                    .Find("PanelBuilder")
                    .GetComponent<ControlUIConstructor>()
                    .MakeConstruction(building);
                });
        }

        // On adapte la position du gameobject Content afin d'adapter le scrolling horizontal
        RectTransform parentBuildingsRect = parentBuildings.GetComponent<RectTransform>();
        parentBuildingsRect.sizeDelta = new Vector2 (adaptedPosX + prefabRect.offsetMax.x, parentBuildingsRect.sizeDelta.y);
    }

    /// <summary>
    /// Permet d'afficher un signal d'erreur sur une image au joueur
    /// </summary>
    /// <param name="obj">L'image a signaler</param>
    /// <returns></returns>
    IEnumerator ErrorSignal(Image obj)
    {
        Color colorObj = obj.color;
        obj.color = errorColor;
        yield return new WaitForSeconds(errorTime);
        obj.color = colorObj;
    }

    /// <summary>
    /// Permet d'acheter un batiment si les fonds sont suffisants
    /// </summary>
    /// <param name="cost">Le prix du batiment</param>
    /// <returns>true si le batiment a été acheté</returns>
    public bool Purchase(int cost)
    {
        ControlUICycle economy = GameObject.Find("GameInfo").GetComponent<ControlUICycle>();

        if(economy.AerCurrent < cost)
            return false;

        economy.Outlay(cost);

        return true;
    }
}
