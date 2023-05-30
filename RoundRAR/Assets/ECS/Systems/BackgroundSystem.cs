using ECS.Components;
using Leopotam.EcsLite;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace ECS.Systems
{
    public class BackgroundSystem : IEcsInitSystem, IEcsRunSystem, IEcsSystem
    {
        private EcsWorld _world;
        private EcsFilter _bgFilter;
        private EcsPool<TransformComponent> _transformPool;
        private EcsPool<BackgroundComponent> _bgPool;
        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            _bgFilter = _world.Filter<BackgroundComponent>().End();
            _bgPool = _world.GetPool<BackgroundComponent>();
            _transformPool = _world.GetPool<TransformComponent>();
            foreach (var bg in _bgFilter)
            { 
                _bgPool.Get(bg).startPos = _transformPool.Get(bg).value.position.x;
             _bgPool.Get(bg).length =
                    GameObject.FindWithTag("Background").GetComponent<SpriteRenderer>().bounds.size.x;
            }
        }

        public void Run(EcsSystems systems)
        {
            foreach (var bg in _bgFilter)
            {
                float temp = _bgPool.Get(bg).camera.transform.position.x * (1 - _bgPool.Get(bg).parallaxEffect);
                float dist = _bgPool.Get(bg).camera.transform.position.x * _bgPool.Get(bg).parallaxEffect;
                _transformPool.Get(bg).value.position = new Vector3(_bgPool.Get(bg).startPos + dist,
                    _transformPool.Get(bg).value.position.y,
                    _transformPool.Get(bg).value.position.z);
                if (temp > _bgPool.Get(bg).startPos + _bgPool.Get(bg).length/3)
                {
                    _bgPool.Get(bg).startPos += _bgPool.Get(bg).length;
                }
                else if (temp < _bgPool.Get(bg).startPos - _bgPool.Get(bg).length/3)
                {
                    _bgPool.Get(bg).startPos -= _bgPool.Get(bg).length;
                }
            }
        }
    }
}