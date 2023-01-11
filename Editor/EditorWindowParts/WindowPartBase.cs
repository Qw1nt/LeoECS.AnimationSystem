using CustomEditorTools;
using UnityEditor;
using UnityEngine;

namespace Ecs.Animation.Editor.EditorWindowParts
{
    public abstract class WindowPartBase
    {
        protected void Part()
        {
            EditorGUILayout.BeginVertical("Box");
            EditorGUILayout.Space(10f);
            Content();
            EditorGUILayout.Space(10f);
            EditorGUILayout.EndVertical();
        }

        protected  void PartTitle(string label)
        {
            GUILayout.Label(label,
                GUIStyles.New()
                    .FontSize(16)
                    .FontWeight(FontStyle.Normal)
                    .ContentAlignment(TextAnchor.MiddleLeft)
                    .ForegroundColor(Color.white)
                    .Margin(8, 20, 0, 10));
        }

        protected abstract void Content();
    }
}