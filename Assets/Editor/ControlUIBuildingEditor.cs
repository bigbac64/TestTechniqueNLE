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
    SerializedProperty errorColorProperty;
    SerializedProperty errorTimeProperty;

    private void OnEnable()
    {
        uiBuilding = (ControlUIBuilding)target;

        buildingsProperty = serializedObject.FindProperty("buildings");
        prefabBuildingProperty = serializedObject.FindProperty("prefabBuilding");
        errorColorProperty = serializedObject.FindProperty("errorColor");
        errorTimeProperty = serializedObject.FindProperty("errorTime");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();


        // modifie le contenu de la liste du champ revenues
        EditorGUI.BeginChangeCheck();

        EditorGUILayout.PropertyField(prefabBuildingProperty);
        serializedObject.ApplyModifiedProperties();

        EditorGUILayout.PropertyField(buildingsProperty, true);
        serializedObject.ApplyModifiedProperties();

        GUI.skin.label.fontStyle = FontStyle.Bold;
        GUILayout.Label("Failure parameter");
        GUILayout.Space(10f);

        EditorGUILayout.PropertyField(errorColorProperty);
        serializedObject.ApplyModifiedProperties();

        EditorGUILayout.PropertyField(errorTimeProperty);
        serializedObject.ApplyModifiedProperties();
        if (EditorGUI.EndChangeCheck())
        {
            uiBuilding.UpdateUIBuilding();
        }


    }
}
