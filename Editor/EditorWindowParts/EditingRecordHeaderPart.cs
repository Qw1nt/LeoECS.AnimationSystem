using System;
using CustomEditorTools;
using Ecs.Animation.Editor.Interfaces;
using UnityEditor;
using UnityEngine;

namespace Ecs.Animation.Editor.EditorWindowParts
{
    public class EditingRecordHeaderPart : ICustomEditorPart
    {
        private readonly SerializedProperty _editingAnimation;
        private readonly Action _onBackToList;

        public EditingRecordHeaderPart(SerializedProperty editingAnimation, Action onBackToList)
        {
            _editingAnimation = editingAnimation;
            _onBackToList = onBackToList;
        }
        
        public void Render()
        {
            EditorGUILayout.BeginVertical("Box");
            EditorGUILayout.Space(10f);
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Назад", GUI.skin.button.Margin(8, 0, 0, 0), 
                    GUILayout.MaxWidth(64f), GUILayout.MinHeight(24f)) == true)
            {
                _onBackToList?.Invoke();
            }

            EditorGUILayout.Separator();

            GUILayout.Label(_editingAnimation?.FindPropertyRelative("shortName").stringValue,
                GUIStyles.New().FontSize(24).FontWeight(FontStyle.Normal).ContentAlignment(TextAnchor.MiddleRight)
                    .ForegroundColor(Color.white).Margin(0, 20, 0, 0));

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space(10f);
            EditorGUILayout.EndVertical();
        }
    }
}