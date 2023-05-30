using System;
using UnityEngine.AI;

namespace ECS.Components
{
    [Serializable]
    public struct NavigationComponent
    {
        public NavMeshAgent navMeshAgent;
    }
}