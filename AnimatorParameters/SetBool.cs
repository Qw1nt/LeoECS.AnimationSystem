using Ecs.Animation.Interfaces;
using UnityEngine;

namespace Ecs.Animation.AnimatorParameters
{
    public readonly struct SetBool : IEcsAnimatorParameter<bool>
    {
        public SetBool(int paramHash, bool value)
        {
            ParamHash = paramHash;
            Value = value;
        }

        public int ParamHash { get; }
            
        public bool Value { get; }

        public void Apply(Animator animator)
        {
            animator.SetBool(ParamHash, Value);
        }
    }
}