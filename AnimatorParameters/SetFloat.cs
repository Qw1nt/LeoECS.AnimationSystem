using Ecs.Animation.Interfaces;
using UnityEngine;

namespace Ecs.Animation.AnimatorParameters
{
    public readonly struct SetFloat : IEcsAnimatorParameter<float>
    {
        public SetFloat(int paramHash, float value)
        {
            ParamHash = paramHash;
            Value = value;
        }

        public int ParamHash { get; }
            
        public float Value { get; }
            
        public void Apply(Animator animator)
        {
            animator.SetFloat(ParamHash, Value);
        }
    }   
}