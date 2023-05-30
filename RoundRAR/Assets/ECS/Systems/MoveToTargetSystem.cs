using ECS.Components;
using Leopotam.EcsLite;
using IEcsSystem = Leopotam.EcsLite.IEcsSystem;
using NotImplementedException = System.NotImplementedException;

namespace ECS.System
{
    public class MoveToTargetSystem : IEcsInitSystem, IEcsRunSystem, IEcsSystem
    {
        private EcsWorld _world;
        private EcsFilter _playerFilter;
        private EcsFilter _enemyFilter;
        private EcsPool<TargetComponent> _targetPool;
        private EcsPool<NavigationComponent> _navigationPool;
        
        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            _navigationPool = _world.GetPool<NavigationComponent>();
            _targetPool = _world.GetPool<TargetComponent>();
            _enemyFilter = _world.Filter<EnemyComponent>().End();
        }

        public void Run(EcsSystems systems)
        {
            foreach (var enemyEntity in _enemyFilter)
            {
                if (_targetPool.Has(enemyEntity) && _navigationPool.Has(enemyEntity))
                {
                    var target = _targetPool.Get(enemyEntity).value.position;
                    if (_navigationPool.Get(enemyEntity).navMeshAgent != null)
                    {
                        _navigationPool.Get(enemyEntity).navMeshAgent.SetDestination(target);
                    }
                }
            }
        }
    }
}