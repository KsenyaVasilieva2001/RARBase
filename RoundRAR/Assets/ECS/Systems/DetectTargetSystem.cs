using ECS.Components;
using Leopotam.EcsLite;
using Unity.VisualScripting;
using EcsFilter = Leopotam.EcsLite.EcsFilter;
using EcsWorld = Leopotam.EcsLite.EcsWorld;
using IEcsInitSystem = Leopotam.EcsLite.IEcsInitSystem;
using IEcsRunSystem = Leopotam.EcsLite.IEcsRunSystem;
using IEcsSystem = Leopotam.EcsLite.IEcsSystem;
using NotImplementedException = System.NotImplementedException;

namespace ECS.System
{
    public class DetectTargetSystem : IEcsInitSystem, IEcsRunSystem, IEcsSystem
    {
        private EcsWorld _world;
        private EcsFilter _playerFilter;
        private EcsFilter _enemyFilter;
        private EcsPool<TargetComponent> _targetPool;
        private EcsPool<TransformComponent> _transformPool;

        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            _enemyFilter = _world.Filter<EnemyComponent>().End();
            _playerFilter = _world.Filter<PlayerComponent>().Inc<TransformComponent>().End();
            _targetPool = _world.GetPool<TargetComponent>();
            _transformPool = _world.GetPool<TransformComponent>();

        }
//по фильтру может только проходится, а из пула что-то вытаскивать
        public void Run(EcsSystems systems) //это типа update
        {
            foreach (var playerEntity in _playerFilter)
            {
                var playerTransform = _transformPool.Get(playerEntity).value.transform;
                foreach (var enemyEntity in _enemyFilter)
                {
                    if (_targetPool.Has(enemyEntity) == false)
                    {
                        _targetPool.Add(enemyEntity).value = playerTransform;
                    }
                }
            }
        }
    }
}