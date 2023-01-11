using System;
using Ecs.Animation.Components;
using Ecs.Animation.Models;
using UnityEditor;
using UnityEngine;

namespace Ecs.Animation.Editor.Data
{
    public class EcsAnimationDataProperties
    {
        private readonly SerializedProperty _enumAnimationType;
    
        private readonly SerializedProperty _enumAnimationName;
        
        private readonly SerializedProperty _layerIndex;

        private readonly SerializedProperty _layerWeight;

        public EcsAnimationDataProperties(SerializedProperty animationDataProperty)
        {
            _enumAnimationType = animationDataProperty.FindPropertyRelative("animationType");
            _enumAnimationName = animationDataProperty.FindPropertyRelative("animationName");
            AnimationClip = animationDataProperty.FindPropertyRelative("animationClip");
            _layerIndex = animationDataProperty.FindPropertyRelative("layerSetting").FindPropertyRelative("layerIndex"); 
            _layerWeight = animationDataProperty.FindPropertyRelative("layerSetting").FindPropertyRelative("layerWeight");
        }

        public SerializedProperty AnimationClip { get; private set; }

        public EcsAnimationType GetEnumAnimationType()
        {
            var values = (EcsAnimationType[]) Enum.GetValues((typeof(EcsAnimationType)));
            return values[_enumAnimationType.enumValueIndex];
        }

        public void SetEnumAnimationType(EcsAnimationType value) => _enumAnimationType.enumValueIndex = value.GetHashCode();
        
        public EcsAnimationName GetEnumAnimationName()
        {
            var values = (EcsAnimationName[]) Enum.GetValues(typeof(EcsAnimationName));
            return values[_enumAnimationName.enumValueIndex];
        }

        public void SetEnumAnimationName(EcsAnimationName value) => _enumAnimationName.enumValueIndex = value.GetHashCode();

        public AnimationClip GetAnimationClip() => (AnimationClip) AnimationClip.objectReferenceValue;

        public void SetAnimationClip(AnimationClip clip) => AnimationClip.objectReferenceValue = clip;

        public int GetLayerIndex() => _layerIndex.intValue;

        public void SetLayerIndex(int value) => _layerIndex.intValue = value;

        public float GetLayerWeight() => _layerWeight.floatValue;

        public void SetLayerWeight(float value) => _layerWeight.floatValue = value;
    }
}