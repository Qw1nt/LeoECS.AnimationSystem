using System;
using _.Ecs.AnimationSystem.Editor.Interfaces;
using UnityEditor;
using UnityEngine;

namespace _.Ecs.AnimationSystem.Editor.EditorWindowParts
{
    public class AnimationsListPart : ICustomEditorPart
    {
        private readonly SerializedObject _serializedObject;
        private readonly SerializedProperty _animations;
        private readonly Action<SerializedProperty> _onStartEditing;

        public AnimationsListPart(SerializedObject serializedObject, Action<SerializedProperty> onStartEditing)
        {
            _serializedObject = serializedObject;
            _animations = _serializedObject.FindProperty("animations");
            _onStartEditing = onStartEditing;
        }

        public void Render()
        {
            for (int i = 0; i < _animations.arraySize; i++)
            {
                SerializedProperty animationProp = _animations.GetArrayElementAtIndex(i);
                EditorGUILayout.BeginHorizontal("Box", GUILayout.MinHeight(28f));
                EditorGUILayout.LabelField(animationProp.FindPropertyRelative("shortName").stringValue);
                
                if (GUILayout.Button("Редактировать") == true)
                    _onStartEditing?.Invoke(animationProp);

                if (GUILayout.Button("Удалить") == true)
                    RemoveAnimation(i);

                EditorGUILayout.EndHorizontal();
            }
        }
        
        private void RemoveAnimation(int index)
        {
            SerializedProperty animations = _serializedObject.FindProperty("animations");
            animations.DeleteArrayElementAtIndex(index);
        }
    }
}