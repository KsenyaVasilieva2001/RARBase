using ECS.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace ECS.Systems
{
     public class GroundPatrolSystem : IEcsInitSystem, IEcsRunSystem
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
        }
        
        public void Run(EcsSystems systems) 
        {
            foreach (var enemy in _enemyFilter)
            {
                if (_enemyPool.Get(enemy).name.Equals("groundPatrol"))
                {
                    MoveGroundPatrol(enemy);
                }
                
            }
           
        }
        void MoveGroundPatrol(int entity)
        {
            var speed = _movementPool.Get(entity).moveSpeed;
            var groundDetect = _groundCheckPool.Get(entity).groundDetect;
            _transformPool.Get(entity).value.Translate(Vector2.left * speed * Time.deltaTime);
            RaycastHit2D groundInfo = Physics2D.Raycast(groundDetect.position, Vector2.down, 1f);
            if (!groundInfo.collider)
            {
                RotateGroundPatrol(entity);
            }
        }
        void RotateGroundPatrol(int entity)
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