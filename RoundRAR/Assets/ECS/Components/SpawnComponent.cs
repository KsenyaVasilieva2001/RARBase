using System;
using UnityEngine;

namespace ECS.Components
{
    [Serializable]
    public struct SpawnComponent
    {
        public Vector2 whereToSpawn;
        public GameObject objectToSpawn;
        public float randX;
        public float spawnRate;
        public float nextSpawn;
    }
}