using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MenuFateReason))]
[CanEditMultipleObjects]
public class Editor_FateReasonMenu : Editor
{
    public override void OnInspectorGUI()
    {
        MenuFateReason myTarget = (MenuFateReason)target;

        DrawDefaultInspector();
        //EditorGUILayout.LabelField("Repopulate Fate Reaons");
        if(GUILayout.Button("Repopulate Fate Reaons"))
        {
            myTarget.ReadFateReasonsFile();
        }

        if (GUILayout.Button("Switch Fate Popup"))
        {
            myTarget.SwitchFatePopup();
        }
    }
}
