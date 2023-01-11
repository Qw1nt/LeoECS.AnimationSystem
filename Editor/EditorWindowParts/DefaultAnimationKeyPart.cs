using _.Ecs.AnimationSystem.Editor.Interfaces;
using CustomEditorTools;
using UnityEditor;

namespace _.Ecs.AnimationSystem.Editor.EditorWindowParts
{
    public class DefaultAnimationKeyPart : IInitCustomEditorPart
    {
        private readonly SerializedProperty _defaultAnimationKey;
        
        private string[] _ecsAnimationsNames;
        private int _selectedIndex;
        
        public DefaultAnimationKeyPart(SerializedObject serializedObject)
        {
            _defaultAnimationKey = serializedObject.FindProperty("defaultAnimationKey");
            FillAnimationsNames(serializedObject);
        }

        private void FillAnimationsNames(SerializedObject serializedObject)
        {
            SerializedProperty serializedAnimations = serializedObject.FindProperty("animations");
            int arraySize = serializedAnimations.arraySize;
            
            _ecsAnimationsNames = new string[arraySize];
            
            for (int i = 0; i < arraySize; i++)
            {
                SerializedProperty element = serializedAnimations.GetArrayElementAtIndex(i);
                _ecsAnimationsNames[i] = element.FindPropertyRelative("shortName").stringValue;
            }
        }

        public void Init()
        {
            if (string.IsNullOrEmpty(_defaultAnimationKey.stringValue) == true)
                _defaultAnimationKey.stringValue = _ecsAnimationsNames[0];
            
            DeterminateSelectedIndex();
        }

        private void DeterminateSelectedIndex()
        {
            string currentAnimationName = _defaultAnimationKey.stringValue;
            
            for(int i = 0; i < _ecsAnimationsNames.Length; i++)
            {
                if (currentAnimationName != _ecsAnimationsNames[i])
                    continue;

                _selectedIndex = i;
                break;
            }
        }

        public void Render()
        {
            _selectedIndex = EditorGUILayout.Popup("Default animation name:", _selectedIndex, _ecsAnimationsNames,
                GUIStyles.New(EditorStyles.popup).Margin(4, 8, 0, 8));
            _defaultAnimationKey.stringValue = _ecsAnimationsNames[_selectedIndex];
        }
    }
}