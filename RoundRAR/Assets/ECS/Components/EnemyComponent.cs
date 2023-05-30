using System;
using UnityEngine;

namespace ECS.Components
{
    [Serializable]
    public struct EnemyComponent
    {
        public int id;
        public string name;
        public GameObject hitPlace;
        public bool isHit;
        public bool canBeKilled;
    }
}