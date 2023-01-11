using Ecs.Animation.Components;
using Leopotam.Ecs;

namespace Ecs.Animation.Systems
{
    public class SetBaseAnimationSystem : IEcsRunSystem
    {
        private readonly EcsFilter<AnimatableComponent> _filter = null;
        private readonly EntityEcsAnimatorSetter _ecsAnimatorSetter = null;

        public void Run()
        {
            foreach (int entity in _filter)
            {
                ref EcsEntity ecsEntity = ref _filter.GetEntity(entity);
                ref AnimatableComponent animatableComponent = ref _filter.Get1(entity);
                
                _ecsAnimatorSetter.Animation.Set(ref animatableComponent, ref ecsEntity, animatableComponent.Data.GetDefaultAnimationKey());
            } 
        }
    }
}
