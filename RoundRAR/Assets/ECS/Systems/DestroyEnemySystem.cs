using ECS.Components;
using LeoEcsPhysics;
using Leopotam.EcsLite;
using Unity.VisualScripting;
using UnityEngine;

namespace ECS.Systems
{
    public class DestroyEnemySystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _playerFilter;
        private EcsFilter _enemyFilter;
        private EcsFilter _hitFilter;
        private EcsPool<JumpComponent> _jumpPool;
        private EcsPool<TransformComponent> _transformPool;
        private EcsPool<OnCollisionEnter2DEvent> _onCollisionPool;
        private EcsPool<AnimationComponent> _animPool;
        private EcsPool<RigidBodyComponent> _rbPool;
        private EcsPool<ColliderComponent> _colliderPool;
        private EcsPool<EnemyComponent> _enemyPool;


        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            _playerFilter = _world.Filter<PlayerComponent>().Inc<TransformComponent>().End();
            _transformPool = _world.GetPool<TransformComponent>();
             _enemyFilter = _world.Filter<EnemyComponent>().End(); 
             _onCollisionPool = _world.GetPool<OnCollisionEnter2DEvent>();
             _animPool = _world.GetPool<AnimationComponent>();
             _rbPool = _world.GetPool<RigidBodyComponent>();
             _colliderPool = _world.GetPool<ColliderComponent>();
             _enemyPool = _world.GetPool<EnemyComponent>();
            _hitFilter = _world.Filter<OnCollisionEnter2DEvent>().Exc<EnemyComponent>().End();
         // _hitFilter = _world.Filter<HitPlaceComponent>().Inc<OnCollisionEnter2DEvent>().End();

        }
        //Здесь проверка на hit place!!!!
        public void Run(EcsSystems systems)
        {
            foreach (var hit in _hitFilter)
            {
                foreach (var enemy in _enemyFilter)
                {
                    if (_onCollisionPool.Get(hit).collider2D.gameObject.CompareTag("Player"))
                    {
                        _transformPool.Add(hit);
                        if (_enemyPool.Get(enemy).hitPlace != null)
                        {
                            Debug.Log(_transformPool.Get(hit).value.position);
                            if (_enemyPool.Get(enemy).canBeKilled &&
                                _enemyPool.Get(enemy).hitPlace.gameObject.transform.position ==
                                _transformPool.Get(hit).value.position)
                            {
                                Debug.Log("Is Hit");
                                _enemyPool.Get(enemy).isHit = true;
                                _rbPool.Get(enemy).value.bodyType = RigidbodyType2D.Dynamic;
                                _colliderPool.Get(enemy).value.enabled = false;
                                foreach (var player in _playerFilter)
                                {
                                    Jump(player);
                                }
                            }
                        }
                        _transformPool.Del(hit);

                    }
                }
            }
        }
        void Jump(int entity)
        {
            var height = _jumpPool.Get(entity).jumpHeight/4;
            _rbPool.Get(entity).value.AddForce(_transformPool.Get(entity).value.up * height, ForceMode2D.Impulse);
        }
    }
}