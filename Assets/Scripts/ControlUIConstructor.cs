using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlUIConstructor : MonoBehaviour
{
    public GameObject prefabBuildConstruction;
    public float animationSpeed;
    List<GameObject> constructions;

    void Start()
    {
        constructions = new List<GameObject>();
    }

    /// <summary>
    /// Permet de construire un batiment dans la scène et de l'ajouter dans la liste des batiments construits
    /// </summary>
    /// <param name="building">Le batiment qui a été sélectionné pour la construction</param>
    public void MakeConstruction(Building building)
    {
        Transform parentBuildings = transform.Find("Viewport/Content");
        GameObject construct = Instantiate(prefabBuildConstruction, parentBuildings);

        // modifie les informations du building construit
        construct.GetComponent<Building>().Init(building.BuildingName, building.Revenues, building.Cost);

        // défini l'action du bouton qui retire le gameobject de la scène et de la liste
        construct.transform
            .Find("Remove")
            .GetComponent<Button>()
            .onClick
            .AddListener(delegate {
                DestroyConstruction(construct);
            });

        // positionne le Gameobject crée sur la scène
        construct.transform.position = GetNextConstructionPosition(construct, constructions.Count);

        constructions.Add(construct);
        ResizeContent();
    }

    

    /// <summary>
    /// Permet de supprimer un batiment construit et de le retirer de la liste des batiment construit
    /// </summary>
    /// <param name="go">Le Gameobject à supprimer</param>
    public void DestroyConstruction(GameObject go)
    {
        if (!constructions.Contains(go))
            return;

        int index = constructions.IndexOf(go);
        constructions.RemoveAt(index);
        Destroy(go);

        ConstructionsRepositioning(index);
    }

    /// <summary>
    /// Permet de déplacer un batiment sur la scène
    /// </summary>
    /// <param name="go">Le Gameobject a déplacer</param>
    /// <param name="toIndex">la position indexé de destination</param>
    public void MoveConstruction(GameObject go, int toIndex)
    {
        Vector3 from = go.transform.position;
        Vector3 to = GetNextConstructionPosition(go, toIndex);
        StartCoroutine(MoveLerp(from, to, animationSpeed, go));
    }

    /// <summary>
    /// Permet de repositionner tout les batiments construit selon leurs position indexé
    /// </summary>
    /// <param name="indexAt"></param>
    void ConstructionsRepositioning(int indexAt)
    {
        for(int i = indexAt; i < constructions.Count; i++)
        {
            MoveConstruction(constructions[i], i);
        }
        ResizeContent();
    }


    /// <summary>
    /// Permet de redimentionner le Gameobject de la scène (Content) pour avoir un scrolling cohérent
    /// </summary>
    void ResizeContent()
    {
        RectTransform content = transform.Find("Viewport/Content").GetComponent<RectTransform>();
        RectTransform prefabRect = prefabBuildConstruction.GetComponent<RectTransform>();

        content.sizeDelta = new Vector2(content.sizeDelta.x, constructions.Count * prefabRect.sizeDelta.y);
    }

    /// <summary>
    /// Permet de retourner la nouvelle position du gameobject en fonction de la position indexé
    /// 
    /// S'adapte en fonction du scale du canvas
    /// </summary>
    /// <param name="go">Le Gameobject à repositionner</param>
    /// <param name="index">La prochaine position indexé</param>
    /// <returns>La position du Gameobject selon l'index</returns>
    Vector3 GetNextConstructionPosition(GameObject go, int index)
    {
        Vector3 rectPos = go.transform.position;
        RectTransform prefabRect = prefabBuildConstruction.GetComponent<RectTransform>();
        RectTransform parent = go.transform.parent.GetComponent<RectTransform>();

        Canvas canvas = GameObject.Find("Canvas").GetComponent<Canvas>();


        return new Vector3(rectPos.x, parent.position.y - prefabRect.sizeDelta.y * canvas.scaleFactor * index, rectPos.z);
    }

    /// <summary>
    /// Permet un movement Lerp sur un Gameobject depuis une Coroutine
    /// </summary>
    /// <param name="from">La position d'origine</param>
    /// <param name="to">La position d'arrivé</param>
    /// <param name="speed">La vitesse de déplacement</param>
    /// <param name="go">Le Gameobject a déplacer</param>
    /// <returns>Yield: Execution à chaque frame</returns>
    IEnumerator MoveLerp(Vector3 from, Vector3 to, float speed, GameObject go)
    {
        var tick = 0f;

        while (tick < 1f)
        {
            tick += speed * Time.deltaTime;
            go.transform.position = Vector3.Lerp(from, to, tick);
            yield return null;
        }
        go.transform.position = to;
    }

    public List<GameObject> Constructions
    {
        get { return constructions; }
    } 
}
