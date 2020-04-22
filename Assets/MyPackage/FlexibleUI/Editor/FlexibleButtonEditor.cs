using UnityEngine.UI;
using UnityEditor;

[CustomEditor(typeof(FlexibleButton))]
[CanEditMultipleObjects]
public class FlexibleButtonEditor : Editor
{
    FlexibleButton reference;

    SerializedProperty sprite;
    SerializedProperty buttonTransition;
    SerializedProperty spriteState;
    SerializedProperty colorBlock;

    private void OnEnable()
    {
        reference = (FlexibleButton)target;

        sprite = serializedObject.FindProperty("sprite");
        buttonTransition = serializedObject.FindProperty("buttonTransition");
        spriteState = serializedObject.FindProperty("spriteState");
        colorBlock = serializedObject.FindProperty("colorBlock");

    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(sprite);
        EditorGUILayout.PropertyField(buttonTransition);

        switch ((Selectable.Transition)buttonTransition.intValue)
        {
            case Selectable.Transition.SpriteSwap:
                EditorGUILayout.PropertyField(spriteState);
            break;

            case Selectable.Transition.ColorTint:
                EditorGUILayout.PropertyField(colorBlock);
            break;

            case Selectable.Transition.Animation:
                EditorGUILayout.HelpBox("Not implemented", MessageType.Warning);
            break;
        }

        serializedObject.ApplyModifiedProperties();
    }
}