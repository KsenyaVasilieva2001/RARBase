using System;
using UnityEngine;

namespace ECS.Components
{
    [Serializable]
    public struct GroundCheckComponent
    {
        public Transform groundDetect;
        public bool isGrounded;
    }
}