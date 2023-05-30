using ECS.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace ECS.Systems
{
    public class AirPatrolSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _enemyFilter;
        private EcsPool<EnemyComponent> _enemyPool;
        private EcsPool<MovementComponent> _movementPool;
        private EcsPool<TransformComponent> _transformPool;
        private EcsPool<GroundCheckComponent> _groundCheckPool;

        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            _enemyFilter = _world.Filter<EnemyComponent>().End();
            _enemyPool = _world.GetPool<EnemyComponent>();
            _movementPool = _world.GetPool<MovementComponent>();
            _transformPool = _world.GetPool<TransformComponent>();
            _groundCheckPool = _world.GetPool<GroundCheckComponent>();
            foreach (var enemy in _enemyFilter)
            {
                if (_enemyPool.Get(enemy).name.Equals("fly"))
                {
                    _transformPool.Get(enemy).value.position = 
                        new Vector3(_movementPool.Get(enemy).point1.transform.position.x, 
                            _movementPool.Get(enemy).point1.transform.position.y,
                            _transformPool.Get(enemy).value.position.z);
                }
            }
        }
        
        public void Run(EcsSystems systems) 
        {
            foreach (var enemy in _enemyFilter)
            {
                if (_enemyPool.Get(enemy).name.Equals("fly"))
                {
                    if (_movementPool.Get(enemy).canFly)
                    {
                        MoveAirPatrol(enemy);
                    }
                }

            }
           
        }
        
        void MoveAirPatrol(int entity)
        {
            var speed = _movementPool.Get(entity).moveSpeed;
            _transformPool.Get(entity).value.position =
                Vector3.MoveTowards(_transformPool.Get(entity).value.position,
                    _movementPool.Get(entity).point1.position,
                    speed * Time.deltaTime);

            if (_transformPool.Get(entity).value.position == _movementPool.Get(entity).point1.position)
            {
                (_movementPool.Get(entity).point1, _movementPool.Get(entity).point2) =
                    (_movementPool.Get(entity).point2, _movementPool.Get(entity).point1);
                RotateAirPatrol(entity);
                //_movementPool.Get(entity).canFly = false;
              //  StartCoroutine(Waiting(entity));
            }
        }
        
        void RotateAirPatrol(int entity)
        {
            if (_movementPool.Get(entity).moveLeft)
            {
                _transformPool.Get(entity).value.eulerAngles = new Vector3(0, 180, 0);
                _movementPool.Get(entity).moveLeft = false;
            }
            else
            {
                _transformPool.Get(entity).value.eulerAngles = new Vector3(0, 0, 0);
                _movementPool.Get(entity).moveLeft = true;
            }
        }
    }
}