using Ecs.Animation.Components;
using Ecs.Animation.Events;
using Leopotam.Ecs;

namespace Ecs.Animation.Systems
{
    public class InitAnimatableComponentsSystem : IEcsPreInitSystem, IEcsRunSystem
    {
        private readonly EcsFilter<AnimatableComponent> _preInitFilter = null;
        private readonly EcsFilter<InitAnimatableComponentEvent> _runFilter = null;

        public void PreInit()
        {
            Execute(_preInitFilter);
        }

        public void Run()
        {
            Execute(_runFilter);
        }

        private void Execute(EcsFilter ecsFilter)
        {
            foreach (int entity in ecsFilter)
            {
                ref EcsEntity ecsEntity = ref ecsFilter.GetEntity(entity);
                ref AnimatableComponent animatableComponent = ref ecsEntity.Get<AnimatableComponent>();
                ref SingleAnimatableComponentData singleAnimatableComponentData = ref animatableComponent.Data;
                
                animatableComponent.Data.Init();
                animatableComponent.EcsAnimator.Init(singleAnimatableComponentData);
                
                animatableComponent.EcsAnimator.Cache();
            }   
        }
    }
}
