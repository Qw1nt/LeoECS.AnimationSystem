using Ecs.Animation.Interfaces;
using UnityEngine;

namespace Ecs.Animation.AnimatorParameters
{
    public readonly struct SetSmoothFloat : IEcsAnimatorParameter<float>
    {
        private readonly float _time;
        private readonly float _deltaTime;
            
        public SetSmoothFloat(int paramHash, float value, float time, float deltaTime)
        {
            ParamHash = paramHash;
            Value = value;
            _time = time;
            _deltaTime = deltaTime;
        }

        public int ParamHash { get; }
            
        public float Value { get; }
            
        public void Apply(Animator animator)
        {
            animator.SetFloat(ParamHash, Value, _time, _deltaTime);
        }
    } 
}