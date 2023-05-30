using ECS.Components;
using Leopotam.EcsLite;
/*
namespace ECS.Systems
{
    public class BarnacleAttackSystem : IEcsInitSystem, IEcsRunSystem
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
                if (_enemyPool.Get(enemy).name.Equals("groundPatrol"))
                {
                    MoveGroundPatrol(enemy);
                }

                if (_enemyPool.Get(enemy).name.Equals("fly"))
                {
                    if (_movementPool.Get(enemy).canFly)
                    {
                        MoveAirPatrol(enemy);
                    }
                }

                if (_enemyPool.Get(enemy).name.Equals("snail"))
                {
                }

            }

        }
    }
}
*/