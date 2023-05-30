using UnityEngine;
using System;
namespace ECS.Components
{
    [Serializable]
    public struct MovementComponent
    {
        public float moveSpeed;
        public bool moveLeft;
        public float waitTime;
        public Transform point1;
        public Transform point2;
        public bool canFly;
    }
}
