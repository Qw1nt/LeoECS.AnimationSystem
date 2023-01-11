using System;
using UnityEngine;

namespace Ecs.Animation.Models
{
    [Serializable]
    public class EcsAnimationLayerSettings
    {
        private const float NoneWeight = 0f;

        [SerializeField] private int layerIndex;
        [Range(0f, 1f)] [SerializeField] private float layerWeight = 0f;

        public void Apply(Animator animator) => animator.SetLayerWeight(layerIndex, layerWeight);

        public void Reset(Animator animator) => animator.SetLayerWeight(layerIndex, NoneWeight);

        public int GetIndex() => layerIndex;

        public float GetWeight() => layerWeight;
    }
}