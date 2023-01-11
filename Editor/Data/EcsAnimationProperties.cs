using UnityEditor;

namespace _.Ecs.AnimationSystem.Editor.Data
{
    public class EcsAnimationProperties
    {
        private readonly SerializedProperty _shortName;

        private readonly SerializedProperty _transitionDuration;

        public EcsAnimationProperties(SerializedProperty animation)
        {
            _shortName = animation.FindPropertyRelative("shortName");
            _transitionDuration = animation.FindPropertyRelative("transitionDuration");
            AnimationData = new EcsAnimationDataProperties(animation.FindPropertyRelative("animationData"));
        }

        public EcsAnimationDataProperties AnimationData { get; private set; }

        public void SetShortName(string value) => _shortName.stringValue = value;

        public string GetShortName() => _shortName.stringValue;

        public float GetTransitionDuration() => _transitionDuration.floatValue;

        public void SetTransitionDuration(float value) => _transitionDuration.floatValue = value;
    }
}