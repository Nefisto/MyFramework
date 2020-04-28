using UnityEngine.UI;
using UnityEditor;

using static FlexibleButton; // ButtonType, Settings
using static UnityEngine.UI.Selectable; // Transition
using UnityEngine;

[CustomEditor(typeof(FlexibleButton))]
[CanEditMultipleObjects]
public class FlexibleButtonEditor : Editor
{
    SerializedProperty sprite;
    SerializedProperty buttonType;

    // Default
    SerializedProperty defaultButtonTransition;
    SerializedProperty defaultSpriteState;
    SerializedProperty defaultColorBlock;

    // Confirm
    SerializedProperty confirmButtonTransition;
    SerializedProperty confirmSpriteState;
    SerializedProperty confirmColorBlock;

    // Decline
    SerializedProperty declineButtonTransition;
    SerializedProperty declineSpriteState;
    SerializedProperty declineColorBlock;

    private void OnEnable()
    {
        sprite = serializedObject.FindProperty("sprite");
        buttonType = serializedObject.FindProperty("buttonType");

        defaultButtonTransition = serializedObject.FindProperty("defaultButtonTransition");
        defaultSpriteState = serializedObject.FindProperty("defaultSpriteState");
        defaultColorBlock = serializedObject.FindProperty("defaultColorBlock");

        confirmButtonTransition = serializedObject.FindProperty("confirmButtonTransition");
        confirmSpriteState = serializedObject.FindProperty("confirmSpriteState");
        confirmColorBlock = serializedObject.FindProperty("confirmColorBlock");

        declineButtonTransition = serializedObject.FindProperty("declineButtonTransition");
        declineSpriteState = serializedObject.FindProperty("declineSpriteState");
        declineColorBlock = serializedObject.FindProperty("declineColorBlock");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(sprite);
        EditorGUILayout.PropertyField(buttonType);

        switch ((ButtonType)buttonType.intValue)
        {
            case ButtonType.Default:
                EditorGUILayout.PropertyField(defaultButtonTransition,
                        new GUIContent("Button transition"));
                switch ((Transition)defaultButtonTransition.intValue)
                {
                    case Selectable.Transition.SpriteSwap:
                        EditorGUILayout.PropertyField(defaultSpriteState);
                        break;

                    case Selectable.Transition.ColorTint:
                        EditorGUILayout.PropertyField(defaultColorBlock);
                        break;

                    case Selectable.Transition.Animation:
                        EditorGUILayout.HelpBox("Not implemented", MessageType.Warning);
                        break;
                }
                break;

            case ButtonType.Confirm:
                EditorGUILayout.PropertyField(confirmButtonTransition,
                        new GUIContent("Button transition"));

                switch ((Transition)confirmButtonTransition.intValue)
                {
                    case Selectable.Transition.SpriteSwap:
                        EditorGUILayout.PropertyField(confirmSpriteState);
                        break;

                    case Selectable.Transition.ColorTint:
                        EditorGUILayout.PropertyField(confirmColorBlock);
                        break;

                    case Selectable.Transition.Animation:
                        EditorGUILayout.HelpBox("Not implemented", MessageType.Warning);
                        break;
                }
                break;

            case ButtonType.Decline:
                EditorGUILayout.PropertyField(declineButtonTransition,
                        new GUIContent("Button transition"));

                switch ((Transition)declineButtonTransition.intValue)
                {
                    case Selectable.Transition.SpriteSwap:
                        EditorGUILayout.PropertyField(declineSpriteState);
                        break;

                    case Selectable.Transition.ColorTint:
                        EditorGUILayout.PropertyField(declineColorBlock);
                        break;

                    case Selectable.Transition.Animation:
                        EditorGUILayout.HelpBox("Not implemented", MessageType.Warning);
                        break;
                }
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }
}