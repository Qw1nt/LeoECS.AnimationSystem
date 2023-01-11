using System;
using Ecs.Animation.Components;
using UnityEngine;

namespace Ecs.Animation.Models
{
    [Serializable]
    public class EcsAnimationData
    {
        [SerializeField] private EcsAnimationType animationType;
        
        [SerializeField] private EcsAnimationName animationName;

        [Space(15f)] [SerializeField] private AnimationClip animationClip;

        [Space] [SerializeField] private EcsAnimationLayerSettings layerSetting;

        public string ShortName { get; private set; }

        public int AnimationHash { get; private set; }

        public EcsAnimationLayerSettings LayerSetting => layerSetting;

        public void Initialize(string shortName = null)
        {
            if (animationType == EcsAnimationType.BlendTree)
                InitBlendTree(shortName);
            else
                InitClipAnimation(shortName);
        }

        private void InitBlendTree(string shortName)
        {
            if (string.IsNullOrEmpty(shortName) == true)
                throw new NullReferenceException("Animation type blend tree does not have a short name specified");
            
            ShortName = shortName;
            AnimationHash = Animator.StringToHash(ShortName);
        }

        private void InitClipAnimation(string shortName)
        {
            if (animationClip == null)
                throw new NullReferenceException($"There is no AnimationClip for animation {ShortName}");

            if (animationName == EcsAnimationName.None && string.IsNullOrEmpty(shortName) == true)
                throw new ArgumentException("Cannot set clip animation short name");
            
            ShortName = animationName == EcsAnimationName.None ? shortName : animationName.ToString();
            AnimationHash = Animator.StringToHash(animationClip.name);
        }
        
        public AnimationClip GetAnimationClip() => animationClip;
    }

    public enum EcsAnimationType
    {
        Clip,
        BlendTree
    }
}