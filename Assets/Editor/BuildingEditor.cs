using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Building))]
public class BuildingEditor : Editor
{
    Building building;

    SerializedProperty buildingNameProperty;
    SerializedProperty revenuesProperty;
    SerializedProperty costProperty;

    private void OnEnable()
    {
        building = (Building)target;

        buildingNameProperty = serializedObject.FindProperty("buildingName");
        revenuesProperty = serializedObject.FindProperty("revenues");
        costProperty = serializedObject.FindProperty("cost");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // modifie la valeur du champ buildingName
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(buildingNameProperty, true);
        serializedObject.ApplyModifiedProperties();
        if (EditorGUI.EndChangeCheck())
        {
            building.UpdateUITextName();
        }

        // modifie le contenu de la liste du champ revenues
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(revenuesProperty, true);
        serializedObject.ApplyModifiedProperties();
        if (EditorGUI.EndChangeCheck())
        {
            building.UpdateUITextRevenue();
        }

        // modifie la valeur du champ cost
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(costProperty);
        serializedObject.ApplyModifiedProperties();
        if (EditorGUI.EndChangeCheck())
        {
            building.UpdateUITextCost();
        }

    }
}
