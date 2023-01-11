using CustomEditorTools;
using Ecs.Animation.Components;
using Ecs.Animation.Editor.Data;
using Ecs.Animation.Editor.Interfaces;
using Ecs.Animation.Models;
using UnityEditor;
using UnityEngine;

namespace Ecs.Animation.Editor.EditorWindowParts
{
    public class OverviewAnimationList : ICustomEditorPart
    {
        private readonly SerializedObject _serializedObject;
        private readonly EcsAnimationGroupData _animationGroupData;

        public OverviewAnimationList(SerializedObject serializedObject, EcsAnimationGroupData animationGroupData)
        {
            _serializedObject = serializedObject;
            _animationGroupData = animationGroupData;
        }
        
        public void Render()
        {
            EditorGUILayout.BeginVertical(GUIStyles.New(GUIStyle.none).Margin(0, 4, 0, 0));

            EditorGUILayout.PropertyField(_serializedObject.FindProperty("targetAnimator"));

            EditorGUILayout.Separator();

            string name = EditorGUILayout.TextField("Group Name:", _animationGroupData.GroupName);
            _serializedObject.FindProperty("groupName").stringValue = name;

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Separator();
            bool addAnimation =
                GUILayout.Button("Добавить", GUIStyles.New(GUI.skin.button), GUILayout.MaxWidth(128f));
            
            if (addAnimation == true)
                AddAnimation();
            
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndVertical();
        }
        
        private void AddAnimation()
        {
            SerializedProperty animations = _serializedObject.FindProperty("animations");
            animations.arraySize += 1;
            SerializedProperty newAnimation = animations.GetArrayElementAtIndex(animations.arraySize - 1);
            EcsAnimationProperties properties = new EcsAnimationProperties(newAnimation);

            properties.SetShortName("Unknown animation");
            properties.SetTransitionDuration(0f);
            properties.AnimationData.SetEnumAnimationName(EcsAnimationName.None);
            properties.AnimationData.SetAnimationClip(null);
        }
    }
}