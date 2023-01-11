using System;
using System.Collections.Generic;
using Ecs.Animation.Models;
using UnityEngine;

namespace Ecs.Animation.Components
{
    [Serializable]
    public class MultipleAnimatableComponentData
    {
        [SerializeField] private List<EcsAnimationGroupData> _animationGroupDates;
        private Dictionary<string, EcsAnimationGroup> _groupsDictionary;

        public void Init()
        {
            _groupsDictionary = new Dictionary<string, EcsAnimationGroup>();

            foreach (EcsAnimationGroupData groupData in _animationGroupDates)
            {
                EcsAnimationGroup animationGroup = new EcsAnimationGroup();
                
                animationGroup.Init(groupData.Animations);
                
                _groupsDictionary.Add(groupData.GroupName, animationGroup);
            }
        }
        
        public EcsAnimation Get(string groupName, int animationHash)
        {
            if (string.IsNullOrEmpty(groupName) == true)
                return null;
            
            if(_groupsDictionary is null == true)
                Init();
            
            if (_groupsDictionary!.TryGetValue(groupName, out EcsAnimationGroup animationGroup) == true)
                return animationGroup?.Get(animationHash);

            return null;
        }
    }

    public enum EcsAnimationName
    {
        None,
        Idle,
        Walk,
        Running,
        Attack,
        TakeDamage,
        TakeDamageClone,
        PlayerActivateWave,
        Die
    }
}