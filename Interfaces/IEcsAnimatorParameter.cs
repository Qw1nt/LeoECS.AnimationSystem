using UnityEngine;

namespace Ecs.Animation.Interfaces
{
    public interface IEcsAnimatorParameter
    {
        public void Apply(Animator animator);
    }
    
    public interface IEcsAnimatorParameter<out T> : IEcsAnimatorParameter
    {
        public int ParamHash { get; }
        
        public T Value { get; }
    }
}