using Ecs.Animation.Events;
using Ecs.Animation.Systems;
using Leopotam.Ecs;
using Unity.VisualScripting;

namespace Ecs.Animation.Extensions
{
    public static class EcsAnimationExtensions
    {
        public static EcsSystems AddDefaultAnimationSystemComponents(this EcsSystems systems)
        {
            systems.UseInitializingAnimatableComponents();
            systems.UseBaseAnimationSetterSystem();
            return systems;
        }
    
        public static EcsSystems UseInitializingAnimatableComponents(this EcsSystems systems)
        {
            systems.Add(new InitAnimatableComponentsSystem());
            systems.OneFrame<InitAnimatableComponentEvent>();
            return systems;
        }

        public static EcsSystems UseBaseAnimationSetterSystem(this EcsSystems systems)
        {
            systems.Add(new SetBaseAnimationSystem());
            return systems;
        }

        public static EcsSystems AddAnimationSystem(this EcsSystems systems)
        {
            systems.Add(new EcsAnimationSystem());
            systems.Add(new LockedAnimationTimeSystem());
            return systems;
        }

        public static EcsSystems InjectAnimationSetter(this EcsSystems systems)
        {
            systems.Inject(new EntityEcsAnimatorSetter());
            return systems;
        }
    }
}
