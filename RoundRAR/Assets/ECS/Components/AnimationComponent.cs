using System;
using UnityEngine;

namespace ECS.Components
{
    [Serializable]
    public struct AnimationComponent
    {
        public Animator animator;
        public int idleAnimValue;
        public int walkAnimValue;
        public int jumpAnimValue;
        public int climbIdle;
        public int climb;
    }
}