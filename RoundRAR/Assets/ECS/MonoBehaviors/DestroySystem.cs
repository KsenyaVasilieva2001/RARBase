using ECS.Components;
using Leopotam.EcsLite;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using NotImplementedException = System.NotImplementedException;

namespace ECS.Systems
{
    public class DestroySystem: MonoBehaviour
    {
        public void DestroySnailInvoke()
        {
            Invoke(nameof(DestroySnail), 4f);
        }
       
        void DestroySnail()
        {
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
            Destroy(gameObject);
        }
        
    }
}
