using _.Ecs.AnimationSystem.Editor.Data;
using _.Ecs.AnimationSystem.Editor.Interfaces;
using CustomEditorTools;
using Ecs.Animation.Components;
using Ecs.Animation.Models;
using UnityEditor;

namespace _.Ecs.AnimationSystem.Editor.EditorWindowParts
{
    public class AnimationNamePart : WindowPartBase, IInitCustomEditorPart
    {
        private readonly EcsAnimationProperties _properties;
        private EcsAnimationName _editingAnimationName;

        public AnimationNamePart(EcsAnimationProperties properties)
        {
            _properties = properties;
        }
        
        public void Init()
        {
            _editingAnimationName = _properties.AnimationData.GetEnumAnimationName();
        }

        public void Render() => Part();
        
        protected override void Content()
        {
            PartTitle("Name");

            if (_properties.AnimationData.GetEnumAnimationType() == EcsAnimationType.BlendTree)
            {
                _properties.AnimationData.SetEnumAnimationName(EcsAnimationName.None);
                SetShortNameField();
                return;
            }
            
            _editingAnimationName = (EcsAnimationName) EditorGUILayout.EnumPopup(_editingAnimationName,
                GUIStyles.New(EditorStyles.popup)
                    .Margin(16, 16, 0, 0));

            _properties.AnimationData.SetEnumAnimationName(_editingAnimationName);

            if (_editingAnimationName == EcsAnimationName.None)
            {
                SetShortNameField();
            }
            else
            {
                _properties.SetShortName(_editingAnimationName.ToString());

                EditorGUILayout.LabelField(_editingAnimationName.ToString(),
                    GUIStyles.New(EditorStyles.label)
                        .Padding(16, 0, 8, 0));
            }
        }

        private void SetShortNameField()
        {
            _properties.SetShortName(EditorGUILayout.TextField(_properties.GetShortName(),
                GUIStyles.New(EditorStyles.textField)
                    .Margin(16, 16, 8, 0)));
        }
    }
}