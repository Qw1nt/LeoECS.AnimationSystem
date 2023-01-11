using System;
using Ecs.Animation.Models;
using UnityEngine;
using UnityEngine.Serialization;

namespace Ecs.Animation.Components
{
    [Serializable]
    public class SingleAnimatableComponentData
    {
        [SerializeField, FormerlySerializedAs("_animationGroupData")] private EcsAnimationGroupData animationGroupData;
        private readonly EcsAnimationGroup _ecsAnimationGroup = new();

        public void Init(EcsAnimationGroupData data = null)
        {
            if (data != null)
                animationGroupData = data;
            
            _ecsAnimationGroup.Init(animationGroupData.Animations);
        }

        public string GetDefaultAnimationKey() => animationGroupData.DefaultAnimationKey;
        
        public EcsAnimation Get(int animationHash)
        {
            return animationHash == -1 ? null : _ecsAnimationGroup.Get(animationHash);
        }
    }
}
