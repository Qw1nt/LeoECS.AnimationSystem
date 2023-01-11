using Ecs.Animation.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Ecs.Animation.Systems
{
    public class LockedAnimationTimeSystem : IEcsRunSystem
    {
        private readonly EcsFilter<AnimatableComponent, HasLockedTimeAnimationComponent> _filter = null;

        public void Run()
        {
            float deltaTime = Time.deltaTime;
            
            foreach (int entity in _filter)
            {
                ref HasLockedTimeAnimationComponent lockedComponent = ref _filter.Get2(entity);
                lockedComponent.Time -= deltaTime;

                if (lockedComponent.Time <= 0f)
                {
                    ref EcsEntity ecsEntity = ref _filter.GetEntity(entity);
                    ecsEntity.Del<HasLockedTimeAnimationComponent>();
                }
            }   
        }
    }
}
