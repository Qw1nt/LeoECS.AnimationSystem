using Ecs.Animation.Components;
using Ecs.Animation.Events;
using Ecs.Animation.Models;
using Leopotam.Ecs;

namespace Ecs.Animation.Systems
{
    public class EcsAnimationSystem : IEcsRunSystem
    {
        private readonly EcsFilter<AnimatableComponent> _filter = null;
        
        public void Run()
        {
            foreach (int entity in _filter)
            {
                ref AnimatableComponent animatableComponent = ref _filter.Get1(entity);
                ref EcsAnimator ecsAnimator = ref animatableComponent.EcsAnimator;

                ecsAnimator.Parameters.Play();

                if (ecsAnimator.AnimationBuffersEquals() == true)
                    continue;
                
                PlayAnimation(ref ecsAnimator, entity);
            }
        }

        private void PlayAnimation(ref EcsAnimator ecsAnimator, int entity)
        {
            ecsAnimator.ResetLayers();
            ecsAnimator.PlayBufferedAnimations();
            ecsAnimator.Cache();
            
            ref EcsEntity ecsEntity = ref _filter.GetEntity(entity);
            ecsEntity.Get<SetAnimationEvent>();
        }
    }
}
