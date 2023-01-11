using System;
using System.Collections.Generic;
using Ecs.Animation.AnimatorParameters;
using Ecs.Animation.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Ecs.Animation.Systems
{
    public class EntityEcsAnimatorSetter
    {
        private readonly Dictionary<string, int> _animationHashes = new();
        private readonly Dictionary<string, int> _paramHashes = new();

        public AnimationSetter Animation { get; }
        
        public ParamSetter Param { get; }

        public EntityEcsAnimatorSetter()
        {
            Animation = new AnimationSetter(GetAnimationHash);
            Param = new ParamSetter(GetParamHash);
        }

        private int GetAnimationHash(string animationName)
        {
            if(_animationHashes.ContainsKey(animationName) == false)
                _animationHashes.Add(animationName, Animator.StringToHash(animationName));

            return _animationHashes[animationName];
        }

        private int GetParamHash(string paramName)
        {
            if(_paramHashes.ContainsKey(paramName) == false)
                _paramHashes.Add(paramName, Animator.StringToHash(paramName));

            return _paramHashes[paramName];
        }

        //TODO Add comments
        public class AnimationSetter
        {
            private readonly Func<string, int> _getHashFunc;

            public AnimationSetter(Func<string, int> getHashFunc)
            {
                _getHashFunc = getHashFunc;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="animatableComponent"></param>
            /// <param name="ecsEntity"></param>
            /// <param name="animationShortName"></param>
            /// <returns></returns>
            public bool Set(ref AnimatableComponent animatableComponent, ref EcsEntity ecsEntity,
                string animationShortName)
            {
                if (ecsEntity.Has<HasLockedTimeAnimationComponent>() == true)
                    return false;

                int animationHash = _getHashFunc.Invoke(animationShortName);
                animatableComponent.EcsAnimator.SetAnimation(animationHash);

                return true;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="animatableComponent"></param>
            /// <param name="ecsEntity"></param>
            /// <param name="ecsAnimationsComponentName"></param>
            /// <returns></returns>
            public bool Set(ref AnimatableComponent animatableComponent, ref EcsEntity ecsEntity,
                EcsAnimationName ecsAnimationsComponentName)
            {
                if (ecsEntity.Has<HasLockedTimeAnimationComponent>() == true)
                    return false;

                int animationHash = _getHashFunc.Invoke(ecsAnimationsComponentName.ToString());
                animatableComponent.EcsAnimator.SetAnimation(animationHash);

                return true;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="animatableComponent"></param>
            /// <param name="ecsEntity"></param>
            /// <param name="animationShortName"></param>
            /// <returns></returns>
            public (bool, float) SetWithLock(ref AnimatableComponent animatableComponent, ref EcsEntity ecsEntity,
                string animationShortName)
            {
                if (ecsEntity.Has<HasLockedTimeAnimationComponent>() == true)
                    return (false, 0f);

                Set(ref animatableComponent, ref ecsEntity, animationShortName);
                var clip = animatableComponent.EcsAnimator.GetAnimationClip(_getHashFunc.Invoke(animationShortName));
                ecsEntity.Get<HasLockedTimeAnimationComponent>().Time = clip.length;

                return (true, clip.length);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="animatableComponent"></param>
            /// <param name="ecsEntity"></param>
            /// <param name="animationShortName"></param>
            /// <param name="lockeTime"></param>
            /// <returns></returns>
            public bool SetWithLock(ref AnimatableComponent animatableComponent, ref EcsEntity ecsEntity,
                string animationShortName, float lockeTime)
            {
                if (ecsEntity.Has<HasLockedTimeAnimationComponent>() == true)
                    return false;

                Set(ref animatableComponent, ref ecsEntity, animationShortName);
                ecsEntity.Get<HasLockedTimeAnimationComponent>().Time = lockeTime;

                return true;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="animatableComponent"></param>
            /// <param name="ecsEntity"></param>
            /// <param name="animationShortName"></param>
            /// <param name="lockeTime"></param>
            /// <returns></returns>
            public bool SetWithLock(ref AnimatableComponent animatableComponent, ref EcsEntity ecsEntity,
                EcsAnimationName animationShortName, float lockeTime)
            {
                if (ecsEntity.Has<HasLockedTimeAnimationComponent>() == true)
                    return false;

                Set(ref animatableComponent, ref ecsEntity, animationShortName);
                ecsEntity.Get<HasLockedTimeAnimationComponent>().Time = lockeTime;

                return true;
            }
        }

        //TODO Add Comments
        public class ParamSetter
        {
            private readonly Func<string, int> _getHashFunc;

            public ParamSetter(Func<string, int> getHashFunc)
            {
                _getHashFunc = getHashFunc;
            }
            
            /// <summary>
            /// 
            /// </summary>
            /// <param name="animatableComponent"></param>
            /// <param name="paramName"></param>
            /// <param name="value"></param>
            public void SetFloat(ref AnimatableComponent animatableComponent, string paramName, float value)
            {
                EcsAnimatorParameters parameters = animatableComponent.EcsAnimator.Parameters;

                int paramHash = _getHashFunc.Invoke(paramName);
                parameters.Add(new SetFloat(paramHash, value));
            }
        
            /// <summary>
            /// 
            /// </summary>
            /// <param name="animatableComponent"></param>
            /// <param name="paramName"></param>
            /// <param name="value"></param>
            /// <param name="smoothTime"></param>
            /// <param name="deltaTime"></param>
            public void SetFloat(ref AnimatableComponent animatableComponent, string paramName, float value, float smoothTime, float deltaTime)
            {
                EcsAnimatorParameters parameters = animatableComponent.EcsAnimator.Parameters;
            
                int paramHash = _getHashFunc.Invoke(paramName);
                parameters.Add(new SetSmoothFloat(paramHash, value, smoothTime, deltaTime));
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="animatableComponent"></param>
            /// <param name="paramName"></param>
            /// <param name="value"></param>
            public void SetInteger(ref AnimatableComponent animatableComponent, string paramName, int value)
            {
                EcsAnimatorParameters parameters = animatableComponent.EcsAnimator.Parameters;
            
                int paramHash = _getHashFunc.Invoke(paramName);
                parameters.Add(new SetInteger(paramHash, value));
            }
        
            /// <summary>
            /// 
            /// </summary>
            /// <param name="animatableComponent"></param>
            /// <param name="paramName"></param>
            /// <param name="value"></param>
            public void SetBool(ref AnimatableComponent animatableComponent, string paramName, bool value)
            {
                EcsAnimatorParameters parameters = animatableComponent.EcsAnimator.Parameters;
            
                int paramHash = _getHashFunc.Invoke(paramName);
                parameters.Add(new SetBool(paramHash, value));
            }
        }
    }
}