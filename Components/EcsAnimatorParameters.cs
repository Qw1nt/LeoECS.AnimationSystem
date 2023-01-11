using System;
using System.Collections.Generic;
using Ecs.Animation.Interfaces;
using UnityEngine;

namespace Ecs.Animation.Components
{
    [Serializable]
    public class EcsAnimatorParameters
    {
        private readonly int _maxParametersCount;

        private Animator _animator;
        private List<IEcsAnimatorParameter> _playbackParams;

        public EcsAnimatorParameters(int maxParametersCount, Animator animator)
        {
            _maxParametersCount = maxParametersCount;
            _animator = animator;
            _playbackParams = new List<IEcsAnimatorParameter>(_maxParametersCount);
        }
        
        public void Play()
        {
            foreach (IEcsAnimatorParameter parameter in _playbackParams)
            {
                parameter.Apply(_animator);
            }
            
            _playbackParams.Clear();
        }

        public void Add(IEcsAnimatorParameter parameter)
        {
            if (_playbackParams.Count + 1 > _maxParametersCount)
                throw new InvalidOperationException("An error occurred while adding an animation parameter. Quantity exceeds the maximum");
            
            _playbackParams.Add(parameter);
        }
    }
}