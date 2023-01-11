using System.Collections.Generic;
using UnityEngine;

namespace Ecs.Animation.Models
{
    [CreateAssetMenu(fileName = "NewEcsAnimationGroup", menuName = "Ecs/Animation System/Create Animation Group")]
    public class EcsAnimationGroupData : ScriptableObject
    {
        [SerializeField] private string groupName;
        [SerializeField] private RuntimeAnimatorController targetAnimator;

        [Space] [SerializeField] private string defaultAnimationKey;
        [Space(5f)] [SerializeField] private List<EcsAnimation> animations;

        public string GroupName => groupName;

        public string DefaultAnimationKey => defaultAnimationKey;
        
        public IReadOnlyList<EcsAnimation> Animations => animations;
    }
}
