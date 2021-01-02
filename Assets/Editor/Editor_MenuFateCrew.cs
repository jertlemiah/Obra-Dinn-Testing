using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MenuFateCrew))]
[CanEditMultipleObjects]
public class Editor_MenuFateCrew : Editor
{
    public override void OnInspectorGUI()
    {
        MenuFateCrew myTarget = (MenuFateCrew)target;

        DrawDefaultInspector();
        //EditorGUILayout.LabelField("Repopulate Fate Reaons");
        if (GUILayout.Button("Repopulate Fate Reaons"))
        {
            myTarget.ReadFateCrewFile();
        }
    }
}
