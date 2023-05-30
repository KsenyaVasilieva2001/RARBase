using System;
using UnityEngine;

namespace ECS.Components
{
    [Serializable]
    public struct BackgroundComponent
    {
        public float length;
        public float startPos;
        public GameObject camera;
        public float parallaxEffect;
    }
}