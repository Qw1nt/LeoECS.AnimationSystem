using System;
using Ecs.Animation.Components;
using Ecs.Animation.Models;

namespace Ecs.Animation.Components
{
    [Serializable]
    public struct AnimatableComponent
    {
        public EcsAnimator EcsAnimator;
        
        public SingleAnimatableComponentData Data;
    }
}