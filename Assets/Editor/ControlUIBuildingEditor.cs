using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ControlUIBuilding))]
public class ControlUIBuildingEditor : Editor
{
    ControlUIBuilding uiBuilding;

    SerializedProperty buildingsProperty;
    SerializedProperty prefabBuildingProperty;

    private void OnEnable()
    {
        uiBuilding = (ControlUIBuilding)target;

        buildingsProperty = serializedObject.FindProperty("buildings");
        prefabBuildingProperty = serializedObject.FindProperty("prefabBuilding");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

               
        EditorGUILayout.PropertyField(prefabBuildingProperty);
        serializedObject.ApplyModifiedProperties();

        // modifie le contenu de la liste du champ revenues
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(buildingsProperty, true);
        serializedObject.ApplyModifiedProperties();
        if (EditorGUI.EndChangeCheck())
        {
            uiBuilding.UpdateUIBuilding();
        }

    }
}
