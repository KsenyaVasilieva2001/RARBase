using UnityEngine;
using System;
namespace ECS.Components
{
    [Serializable]
    public struct HealthComponent
    {
        public float curHp;
        public float maxHp;
        public bool isHit;
        public int deltaHp;
    }
}