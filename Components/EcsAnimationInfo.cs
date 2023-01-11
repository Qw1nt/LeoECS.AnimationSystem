namespace Ecs.Animation.Components
{
    public struct EcsAnimationInfo
    {
        public string AnimationName;

        public int LayerIndex;
        public float LayerWeight;

        public float TransitionDuration;
        
        public EcsAnimationInfo(string animationName, int layerIndex, float layerWeight, float transitionDuration)
        {
            AnimationName = animationName;
            
            LayerIndex = layerIndex;
            LayerWeight = layerWeight;
            
            TransitionDuration = transitionDuration;
        }
        
        /*
        public EcsAnimationInfo(FixedString32Bytes animationName, int layerIndex, float layerWeight, float transitionDuration)
        {
            AnimationName = animationName;
            
            LayerIndex = layerIndex;
            LayerWeight = layerWeight;

            TransitionDuration = transitionDuration;
        }*/
    }
}