using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameEventInt))]
public class GameEventIntEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUI.enabled = Application.isPlaying;

        GameEventInt e = target as GameEventInt;
        int value = e.value;

        if (GUILayout.Button("Raise"))
            e.Raise(value);
    }
}