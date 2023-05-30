using System.Collections;
using ECS.Components;
using ECS.Providers;
using LeoEcsPhysics;
using Leopotam.EcsLite;
using Unity.VisualScripting;
using UnityEngine;


namespace ECS.Systems
{
    public class TakeHealthSystem : IEcsInitSystem, IEcsRunSystem

    {
        private EcsWorld _world;
        private EcsFilter _playerFilter;
        private EcsFilter _enemyFilter;
        private EcsPool<JumpComponent> _jumpPool;
        private EcsPool<RigidBodyComponent> _rbPool;
        private EcsPool<HealthComponent> _healthPool;
        private EcsPool<TransformComponent> _transformPool;
        private EcsPool<OnCollisionEnter2DEvent> _onCollisionPool;
        private EcsPool<EnemyComponent> _enemyPool;

        private EcsPool<ColliderComponent> _colliderPool;

        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            _playerFilter = _world.Filter<PlayerComponent>().Inc<HealthComponent>().End();
            _healthPool = _world.GetPool<HealthComponent>();
            _colliderPool = _world.GetPool<ColliderComponent>();
            _rbPool = _world.GetPool<RigidBodyComponent>();
            _jumpPool = _world.GetPool<JumpComponent>();
            _transformPool = _world.GetPool<TransformComponent>();
             _enemyFilter = _world.Filter<OnCollisionEnter2DEvent>().End(); 
             _onCollisionPool = _world.GetPool<OnCollisionEnter2DEvent>();
            foreach (var playerEntity in _playerFilter)
            {
                Debug.Log("Take Health Init");
                _healthPool.Get(playerEntity).curHp = _healthPool.Get(playerEntity).maxHp;
            }
        }

        public void Run(EcsSystems systems)
        {
            foreach (var enemyEntity in _enemyFilter)
            {
                if (_onCollisionPool.Get(enemyEntity).collider2D.gameObject.CompareTag("Player"))
                {
                    foreach (var player in _playerFilter)
                    {
                        RecountHp(player);
                        Jump(player);
                        if (_healthPool.Get(player).curHp <= 0)
                        {
                            GameObject.FindWithTag("MainCamera").GetComponent<SceneSystem>().RestartScene();
                        }
                    }
                }
            }
        }
        
/*
        void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log("Take Health is done");
            foreach (var enemyEntity in _enemyFilter)
            {
                if (collision.gameObject.GetComponent<PlayerComponentProvider>())
                {
                    foreach (var playerEntity in _playerFilter)
                    {
                        RecountHp(playerEntity, enemyEntity);
                        Jump(playerEntity);
                    }
                }
            }
        }
*/
        void RecountHp(int entity)
        {
            _healthPool.Get(entity).curHp += _healthPool.Get(entity).deltaHp;
            if (_healthPool.Get(entity).deltaHp < 0)
            {
            //    GameObject.FindWithTag("MainCamera").GetComponent<CoroutineSystem>().StopCoroutineOnHit();
                //StopCoroutine(OnHit(entity));
                
                _healthPool.Get(entity).isHit = true;
                GameObject.FindWithTag("MainCamera").GetComponent<CoroutineSystem>().StartCoroutineOnHit();
              //  StartCoroutine(OnHit(entity));
            }
        }
        
        void Jump(int entity)
        {
            var height = _jumpPool.Get(entity).jumpHeight/10;
            _rbPool.Get(entity).value.AddForce(_transformPool.Get(entity).value.up * height, ForceMode2D.Impulse);
        }

        /*
        IEnumerator OnHit(int entity)
        {
            if (_healthPool.Get(entity).isHit)
                    GetComponent<SpriteRenderer>().color = new Color(1f, GetComponent<SpriteRenderer>().color.g - 0.02f,
                        GetComponent<SpriteRenderer>().color.b - 0.02f); //вычислено как 1-0.4 / на кадров в секунду
                else
                    GetComponent<SpriteRenderer>().color = new Color(1f, GetComponent<SpriteRenderer>().color.g + 0.02f,
                        GetComponent<SpriteRenderer>().color.b + 0.02f);
                if (GetComponent<SpriteRenderer>().color.g == 1f) StopCoroutine(OnHit(entity));

                if (GetComponent<SpriteRenderer>().color.g <= 0.4) _healthPool.Get(entity).isHit = false;
                yield return new WaitForSeconds(0.01f); // Период корутины
                StartCoroutine(OnHit(entity));
        }
        */
    }
}