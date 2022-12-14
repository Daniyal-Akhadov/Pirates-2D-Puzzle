using System;
using PixelCrew.Utilities.Editor;
using UnityEditor;

namespace PixelCrew.Components.Dialogs.Editor
{
    [CustomEditor(typeof(ShowDialogComponent))]
    public class ShowDialogComponentEditor : UnityEditor.Editor
    {
        private SerializedProperty _modeProperty;
        
        private void OnEnable()
        {
            _modeProperty = serializedObject.FindProperty("_mode");
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(_modeProperty);

            if (_modeProperty.GetEnum(out ShowDialogComponent.DialogMode mode))
            {
                switch (mode)
                {
                    case ShowDialogComponent.DialogMode.Bound:
                        EditorGUILayout.PropertyField(serializedObject.FindProperty("_bound"));
                        break;
                    case ShowDialogComponent.DialogMode.External:
                        EditorGUILayout.PropertyField(serializedObject.FindProperty("_external"));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_onStart"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_onFinished"));
            serializedObject.ApplyModifiedProperties();
        }
    }
}