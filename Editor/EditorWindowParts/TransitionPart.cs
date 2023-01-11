using System;
using _.Ecs.AnimationSystem.Editor.Data;
using _.Ecs.AnimationSystem.Editor.Interfaces;
using CustomEditorTools;
using UnityEditor;
using UnityEngine;

namespace _.Ecs.AnimationSystem.Editor.EditorWindowParts
{
    public class TransitionPart : WindowPartBase, ICustomEditorPart
    {
        private readonly EcsAnimationProperties _properties;
        
        public TransitionPart(EcsAnimationProperties properties)
        {
            _properties = properties;
        }

        public void Render() => Part();
        
        protected override void Content()
        {
            PartTitle("Transition");
            EditorGUILayout.BeginVertical(GUIStyles.New(GUIStyle.none).Margin(16, 16, 0, 0));

            float duration = _properties.GetTransitionDuration();
            duration = EditorGUILayout.FloatField("Duration: ", duration);
            _properties.SetTransitionDuration(duration);

            EditorGUILayout.EndVertical();
        }
    }
}