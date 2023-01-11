using CustomEditorTools;
using Ecs.Animation.Editor.Data;
using Ecs.Animation.Editor.EditorWindowParts;
using Ecs.Animation.Editor.Interfaces;
using UnityEditor;
using UnityEngine;

namespace Ecs.Animation.EditorWindowParts
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