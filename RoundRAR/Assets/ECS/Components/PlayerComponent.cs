using System;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

namespace ECS.Components
{
    [Serializable]
    public struct PlayerComponent
    {
        public int tag;
        public int coins;
        public Transform transformValue;
        public Rigidbody2D rbValue;
        public bool isClimb;
    }
}