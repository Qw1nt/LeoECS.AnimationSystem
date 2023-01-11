using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ecs.Animation.Models
{
    [Serializable]
    public class EcsAnimationGroup
    {
        [Space] 
        [SerializeField] private List<EcsAnimation> _containedAnimations;
        
        private Dictionary<int, EcsAnimation> _animations;
            
        public void Init(IReadOnlyList<EcsAnimation> ecsAnimations)
        {
            _containedAnimations = new List<EcsAnimation>(ecsAnimations);
            _animations = new Dictionary<int, EcsAnimation>();
            
            foreach (EcsAnimation ecsAnimation in _containedAnimations)
            {
                EcsAnimationData ecsAnimationData = ecsAnimation.Data;
                
                ecsAnimationData.Initialize(ecsAnimation.ShortName);
                _animations.Add(Animator.StringToHash(ecsAnimationData.ShortName), ecsAnimation);
            }
        }

        public EcsAnimation Get(int animationHash)
        {
            return _animations.TryGetValue(animationHash, out EcsAnimation ecsAnimation) == true ? ecsAnimation : null;
        }
    }
}