using Ecs.Animation.Models;
using UnityEditor;
using UnityEngine;

namespace Ecs.Animation.Editor
{
    [CustomEditor(typeof(EcsAnimationGroupData))]
    public class AnimationGroupEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            bool openingWindow = GUILayout.Button("Open Editor Window");
            
            if (openingWindow == true)
            {
                EcsAnimationGroupData targetData = (EcsAnimationGroupData) serializedObject.targetObject;
                AnimationGroupEditorWindow.ShowWindow(ref targetData);
            }
            
            // base.OnInspectorGUI();
            serializedObject.ApplyModifiedProperties();
        }
    }
}
