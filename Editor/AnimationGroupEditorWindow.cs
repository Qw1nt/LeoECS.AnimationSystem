using Ecs.Animation.Editor.Data;
using Ecs.Animation.Editor.EditorWindowParts;
using Ecs.Animation.Editor.Interfaces;
using Ecs.Animation.EditorWindowParts;
using Ecs.Animation.Models;
using UnityEditor;
using UnityEngine;

namespace Ecs.Animation.Editor
{
    public class AnimationGroupEditorWindow : EditorWindow
    {
        private ICustomEditorPart[] _uiParts;

        private static EcsAnimationGroupData AnimationGroupData { get; set; }

        private static SerializedObject _serializedObject;

        private static SerializedProperty _editingAnimation = null;

        private void OnGUI()
        {
            if (AnimationGroupData == null || _serializedObject == null)
            {
                Close();
                return;
            }

            _serializedObject.Update();

            foreach (var uiPart in _uiParts)
            {
                uiPart.Render();
            }

            _serializedObject.ApplyModifiedProperties();
        }

        public static void ShowWindow(ref EcsAnimationGroupData animationGroupData)
        {
            AnimationGroupData = animationGroupData;
            _serializedObject = new SerializedObject(AnimationGroupData);
            _editingAnimation = null;

            var window = GetWindow<AnimationGroupEditorWindow>("Animation Group Data Editor");
            window.BuildUIParts();
        }

        private void BuildUIParts()
        {
            _uiParts = new ICustomEditorPart[]
            {
                new Header(),
                new Body()
            };
        }

        private class CustomEditorPartRenderer
        {
            public void Render(ICustomEditorPart part)
            {
                part.Render();
            }

            public void Render(IInitCustomEditorPart part)
            {
                part.Init();
                part.Render();
            }
        }

        private class Header : ICustomEditorPart
        {
            private readonly CustomEditorPartRenderer _partRenderer = new();

            public void Render()
            {
                if (_editingAnimation != null)
                    return;

                _partRenderer.Render(new OverviewAnimationList(_serializedObject, AnimationGroupData));
            }
        }

        private class Body : ICustomEditorPart
        {
            private readonly CustomEditorPartRenderer _partRenderer = new();

            private EcsAnimationProperties _properties;
            private Vector2 _scrollPosition;

            public void Render()
            {
                if (_editingAnimation == null)
                    AnimationsList();
                else
                    EditingRecord();
            }

            private void AnimationsList()
            {
                EditorGUILayout.Space(20f);

                _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);

                _partRenderer.Render(new DefaultAnimationKeyPart(_serializedObject));
                _partRenderer.Render(new AnimationsListPart(_serializedObject, (animationProp) =>
                {
                    _editingAnimation = animationProp;
                    _properties = new EcsAnimationProperties(animationProp);
                }));

                EditorGUILayout.EndScrollView();
            }

            private void EditingRecord()
            {
                _partRenderer.Render(new EditingRecordHeaderPart(_editingAnimation, () => _editingAnimation = null));
                _partRenderer.Render(new AnimationNamePart(_properties));
                _partRenderer.Render(new AnimationClipPart(_properties, _serializedObject,
                    () => _editingAnimation = null));
                _partRenderer.Render(new TransitionPart(_properties));
                _partRenderer.Render(new LayerSettingsPart(_properties));
            }
        }
    }
}
