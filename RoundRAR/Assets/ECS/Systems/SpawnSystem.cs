using ECS.Components;
using LeoEcsPhysics;
using Leopotam.EcsLite;
using Unity.VisualScripting;
using UnityEngine;
using Voody.UniLeo.Lite;
using NotImplementedException = System.NotImplementedException;

namespace ECS.Systems
{
    public class SpawnSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _spawn;
        private EcsPool<SpawnComponent> _spawnPool;
        private EcsPool<TransformComponent> _transformPool;


        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            _spawn = _world.Filter<SpawnComponent>().End();
            _spawnPool = _world.GetPool<SpawnComponent>();
            _transformPool = _world.GetPool<TransformComponent>();
        }

        public void Run(EcsSystems systems)
        {
            foreach (var spawn in _spawn)
            {
                if (Time.time > _spawnPool.Get(spawn).nextSpawn)
                {
                    _spawnPool.Get(spawn).nextSpawn = Time.time + _spawnPool.Get(spawn).spawnRate;
                    _spawnPool.Get(spawn).randX = Random.Range(_transformPool.Get(spawn).value.position.x - 8 * 2,
                        _transformPool.Get(spawn).value.position.x + 8 * 2);
                    _spawnPool.Get(spawn).whereToSpawn = new Vector2(_spawnPool.Get(spawn).randX,
                        _transformPool.Get(spawn).value.position.y);
                    GameObject.Instantiate(_spawnPool.Get(spawn).objectToSpawn,
                        _spawnPool.Get(spawn).whereToSpawn,
                        Quaternion.identity);
                }
            }
            GameObject.FindWithTag("Snail").GetComponent<DestroySystem>().DestroySnailInvoke();

        }
    }
}