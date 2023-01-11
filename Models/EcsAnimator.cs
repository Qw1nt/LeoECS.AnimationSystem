using System;
using Ecs.Animation.Components;
using UnityEngine;

namespace Ecs.Animation.Models
{
    [Serializable]
    public class EcsAnimator
    {
        [SerializeField] private Animator unityAnimator;
        [SerializeField] private int maxParameterCount;
        
        private SingleAnimatableComponentData _data;
   
        public Animator UnityAnimator => unityAnimator;
        
        private EcsBuffer<EcsAnimation> CurrentAnimationsBuffer { get; set; }
        
        private EcsBuffer<EcsAnimation> AnimationPlaybackBuffer { get; set; }
        
        public EcsAnimatorParameters Parameters { get; private set; }

        public void Init(SingleAnimatableComponentData data)
        {
            Parameters = new EcsAnimatorParameters(maxParameterCount, unityAnimator);
            
            CreateBuffers();
            SetData(data);
        }
        
        private void CreateBuffers()
        {
            CurrentAnimationsBuffer = new EcsBuffer<EcsAnimation>(2);
            AnimationPlaybackBuffer = new EcsBuffer<EcsAnimation>(2);

            if (unityAnimator is null)
                throw new NullReferenceException("Unity animator not set");
            
            for (int i = 0; i < unityAnimator.layerCount; i++)
            {
                CurrentAnimationsBuffer.Fill(i, new EcsAnimation());
                AnimationPlaybackBuffer.Fill(i, new EcsAnimation());
            }
        }

        private void SetData(SingleAnimatableComponentData data)
        {
            _data = data;
        }

        public void SetAnimation(int animationHash)
        {
            EcsAnimation ecsAnimation = _data.Get(animationHash);
            EcsAnimationLayerSettings layerSettings = ecsAnimation?.Data.LayerSetting;

            if (layerSettings is null == false)
                CurrentAnimationsBuffer.Fill(layerSettings.GetIndex(), ecsAnimation);
        }

        public bool AnimationBuffersEquals()
        {
            for (int i = 0; i < CurrentAnimationsBuffer.GetLength(); i++)
            {
                string firstName = CurrentAnimationsBuffer.Get(i)?.Data?.ShortName;
                string secondName = AnimationPlaybackBuffer.Get(i)?.Data?.ShortName;

                if (firstName != secondName)
                    return false;
            }
            
            return true;
        }

        public void PlayBufferedAnimations()
        {
            foreach (EcsAnimation ecsAnimation in CurrentAnimationsBuffer.GetCollection())
            {
                ecsAnimation?.Play(UnityAnimator);
            }
        }
        
        public void ResetLayers()
        {
            for(int i = 0; i < unityAnimator.layerCount; i++)
                unityAnimator.SetLayerWeight(i, 0f);
        }

        public void Cache()
        {
            ClearAnimationBuffer(AnimationPlaybackBuffer);

            for (int i = 0; i < CurrentAnimationsBuffer.GetLength(); i++)
            {
                EcsAnimationData ecsAnimationData = CurrentAnimationsBuffer.Get(i)?.Data;
                EcsAnimationLayerSettings layerSetting = ecsAnimationData?.LayerSetting;
                
                if(layerSetting == null)
                    continue;
                
                int layerIndex = layerSetting.GetIndex();
                AnimationPlaybackBuffer.Fill(layerIndex, CurrentAnimationsBuffer.Get(i));
            }
            
            ClearAnimationBuffer(CurrentAnimationsBuffer);
        }
        
        public void ClearAnimationBuffer(EcsBuffer<EcsAnimation> buffer)
        {
            for (int i = 0; i < buffer.GetLength(); i++)
            {
                buffer.Remove(i);
            }
        }

        public EcsBuffer<EcsAnimation> GetPlaybackBuffer() => AnimationPlaybackBuffer;

        public int GetLayerCount() => unityAnimator.layerCount;
        
        public AnimationClip GetAnimationClip(int animationHash)
        {
            EcsAnimation ecsAnimation = _data.Get(animationHash);
            return ecsAnimation?.Data?.GetAnimationClip();
        }
        
        /*
        public AnimationClip GetAnimationClip(EcsAnimationName name)
        {
            EcsAnimation ecsAnimation = _data.Get(name.ToString());

            foreach (AnimationClip animationClip in UnityAnimator.runtimeAnimatorController.animationClips)
            {
                if (animationClip.name == ecsAnimation.Data.GetAnimationClipName())
                    return animationClip;
            }

            return null;
        }*/
    }
}
