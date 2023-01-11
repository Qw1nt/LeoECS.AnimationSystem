using System;
using UnityEngine;

namespace Ecs.Animation.Models
{
    [Serializable]
    public class EcsAnimation
    {
        [Space]
        [SerializeField] private string shortName;
        
        [Space(5f)] 
        [SerializeField] private float transitionDuration;
        
        [Space(15f)]
        [SerializeField] private EcsAnimationData animationData;

        public string ShortName => shortName;

        public float TransitionDuration => transitionDuration;

        public EcsAnimationData Data => animationData;

        public void Play(Animator animator)
        {
            EcsAnimationLayerSettings layerSetting = Data.LayerSetting;
            int animationHash = Data.AnimationHash;

            layerSetting.Apply(animator);
            animator.CrossFade(animationHash, TransitionDuration, layerSetting.GetIndex());
        }
    }
}