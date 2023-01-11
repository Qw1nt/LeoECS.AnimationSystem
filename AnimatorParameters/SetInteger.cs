using Ecs.Animation.Interfaces;
using UnityEngine;

namespace Ecs.Animation.AnimatorParameters
{
    public readonly struct SetInteger : IEcsAnimatorParameter<int>
    {
        public SetInteger(int paramHash, int value)
        {
            ParamHash = paramHash;
            Value = value;
        }

        public int ParamHash { get; }
            
        public int Value { get; }

        public void Apply(Animator animator)
        {
            animator.SetInteger(ParamHash, Value);
        }
    }  
}