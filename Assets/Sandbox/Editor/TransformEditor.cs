using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;

// [CustomEditor(typeof(Transform), true)]
// [CanEditMultipleObjects]
// public class TransformEditor : Editor
// {
//     Editor defaultEditor;
//     Transform Target
//     {
//         get => target as Transform;
//     }

//     public bool originalState;

//     private void OnEnable()
//     {
//         defaultEditor = Editor.CreateEditor(targets, Type.GetType("UnityEditor.TransformInspector, UnityEditor"));
//     }

//     // private void OnDisable()
//     // {
//     //     MethodInfo disableMethod = defaultEditor.
//     //                                 GetType().
//     //                                 GetMethod("OnDisable", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
//     //     if (disableMethod != null)
//     //         disableMethod.Invoke(defaultEditor,null);
//     //     DestroyImmediate(defaultEditor);
//     // }

//     public override void OnInspectorGUI()
//     {
//         originalState = EditorGUILayout.Toggle("Default state", originalState);
//         defaultEditor.OnInspectorGUI();
//     }
// }