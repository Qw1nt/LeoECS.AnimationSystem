using System;
using CustomEditorTools;
using Ecs.Animation.Components;
using Ecs.Animation.Editor.Data;
using Ecs.Animation.Editor.Interfaces;
using Ecs.Animation.Models;
using UnityEditor;
using UnityEngine;

namespace Ecs.Animation.Editor.EditorWindowParts
{
    public class AnimationClipPart : WindowPartBase, IInitCustomEditorPart
    {
        private readonly EcsAnimationProperties _properties;
        private readonly SerializedObject _serializedObject;
        private readonly Action _onFailureInit;
        
        private string[] _animationNames;
        private AnimationClip[] _clips;

        private EcsAnimationType _animationType;
        private EcsAnimationName _editingAnimationName;
        
        private int _selectedClipIndex = 0;

        public AnimationClipPart(EcsAnimationProperties properties, SerializedObject serializedObject, Action onFailureInit)
        {
            _properties = properties;
            _serializedObject = serializedObject;
            _onFailureInit = onFailureInit;
        }

        public void Init()
        {
            CreateAnimationNames();
            _animationType = _properties.AnimationData.GetEnumAnimationType();
        }
        
        private void CreateAnimationNames()
        {
            SerializedProperty animatorControllerProperty = _serializedObject.FindProperty("targetAnimator");

            if (animatorControllerProperty.objectReferenceValue == null)
            {
                _onFailureInit?.Invoke();
                EditorUtility.DisplayDialog("Error", "Target animator controller not set", "OK");
                return;    
            }
                
            SerializedObject animatorController = new SerializedObject(animatorControllerProperty.objectReferenceValue);

            RuntimeAnimatorController target = (RuntimeAnimatorController) animatorController.targetObject;
                
            _animationNames = new string[target.animationClips.Length + 1];
            _clips = new AnimationClip[target.animationClips.Length + 1];
                
            _animationNames[0] = "None";
            _clips[0] = null;
                
            for (int i = 1; i < _animationNames.Length; i++)
            {
                AnimationClip clip = target.animationClips[i - 1];

                _animationNames[i] = clip.name;
                _clips[i] = clip;
            }

            AnimationClip currentClip = _properties.AnimationData.GetAnimationClip();

            for (int i = 0; i < _clips.Length; i++)
            {
                if (_clips[i] != currentClip)
                    continue;

                _selectedClipIndex = i;
            }
        }

        public void Render() => Part();

        protected override void Content()
        {
            PartTitle("Clip");

            _animationType = (EcsAnimationType) EditorGUILayout.EnumPopup("Animation type:", _animationType, GUIStyles.New(EditorStyles.popup).Margin(16, 16, 0, 0));
            _properties.AnimationData.SetEnumAnimationType(_animationType);
                
            if (_properties.AnimationData.GetEnumAnimationType() == EcsAnimationType.BlendTree)
            {
                _editingAnimationName = EcsAnimationName.None;
                return;
            }
                
            EditorGUILayout.BeginVertical(GUIStyles.New(GUIStyle.none).Margin(16, 16, 0, 0));
            EditorGUILayout.PropertyField(_properties.AnimationData.AnimationClip);
            EditorGUILayout.EndVertical();

            _selectedClipIndex = EditorGUILayout.Popup(_selectedClipIndex, _animationNames, GUIStyles.New(EditorStyles.popup).Margin(16, 16, 0, 0));
            EditorGUILayout.BeginHorizontal(GUIStyles.New(GUIStyle.none).Margin(8, 0, 8, 0));

            if (GUILayout.Button("<-", GUILayout.MaxWidth(48f)) == true)
            {
                if (_selectedClipIndex - 1 < 0)
                    _selectedClipIndex = _animationNames.Length - 1;
                else
                    _selectedClipIndex--;
            }

            if (GUILayout.Button("->", GUILayout.MaxWidth(48f)) == true)
            {
                if (_selectedClipIndex + 1 >= _animationNames.Length)
                    _selectedClipIndex = 0;
                else
                    _selectedClipIndex++;
            }

            GUILayout.Label($"{_selectedClipIndex + 1} / {_animationNames.Length}",
                GUIStyles.New(EditorStyles.label).Margin(16, 0, 2, 0));

            _properties.AnimationData.SetAnimationClip(_clips[_selectedClipIndex]);

            EditorGUILayout.EndHorizontal();
        }
    }
}