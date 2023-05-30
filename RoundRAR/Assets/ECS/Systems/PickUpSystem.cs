using ECS.Components;
using LeoEcsPhysics;
using Leopotam.EcsLite;
using Unity.VisualScripting;
using UnityEngine;

namespace ECS.Systems
{
   public class PickUpSystem : MonoBehaviour, IEcsInitSystem, IEcsRunSystem

    {
        private EcsWorld _world;
        private EcsFilter _playerFilter;
        private EcsPool<RigidBodyComponent> _rbPool;
        private EcsPool<TransformComponent> _transformPool;
        private EcsPool<OnTriggerEnter2DEvent> _onCollisionPool;
        private EcsPool<EnemyComponent> _enemyPool;
        private EcsPool<PlayerComponent> _playerPool;

        private EcsPool<ColliderComponent> _colliderPool;

        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            _playerFilter = _world.Filter<OnTriggerEnter2DEvent>().End();
            _colliderPool = _world.GetPool<ColliderComponent>();
            _rbPool = _world.GetPool<RigidBodyComponent>();
            _transformPool = _world.GetPool<TransformComponent>();
            _onCollisionPool = _world.GetPool<OnTriggerEnter2DEvent>();
            _playerPool = _world.GetPool<PlayerComponent>();
        }

        public void Run(EcsSystems systems)
        {
            foreach (var player in _playerFilter)
            {
                //выносить теги в константу
                if (_onCollisionPool.Get(player).collider2D.gameObject.CompareTag("Coin"))
                {
                    _onCollisionPool.Get(player).collider2D.gameObject.GetComponent<CircleCollider2D>().enabled = false;
                    _onCollisionPool.Get(player).collider2D.gameObject.SetActive(false);
                    _playerPool.Add(player);
                    _playerPool.Get(player).coins++;
                    Debug.Log( _playerPool.Get(player).coins);
                    _playerPool.Del(player);
                    //Destroy(_onCollisionPool.Get(player).collider2D.gameObject);
                }
            }
        }
        
    }
}