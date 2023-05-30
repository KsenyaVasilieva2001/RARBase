using ECS.Components;
using LeoEcsPhysics;
using Leopotam.EcsLite;
using Unity.VisualScripting;
using UnityEngine;

namespace ECS.Systems
{
    public class ClimbSystem : IEcsInitSystem, IEcsRunSystem

    {
        private EcsWorld _world;
        private EcsFilter _playerFilter;
        private EcsFilter _playerFilterExit;
        private EcsFilter _playerFilterStay;
        private EcsPool<OnTriggerExit2DEvent> _onTrigerExitPool;
        private EcsPool<OnTriggerStay2DEvent> _onTrigerStayPool;
        private EcsPool<AnimationComponent> _animPool;
        private EcsPool<MovementComponent> _movePool;
        private EcsPool<TransformComponent> _transformPool;
        private EcsPool<RigidBodyComponent> _rbPool;
        private EcsPool<PlayerComponent> _playerPool;
        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            _playerFilterStay = _world.Filter<OnTriggerStay2DEvent>().End();
            _playerFilterExit = _world.Filter<OnTriggerExit2DEvent>().End();
            _playerFilter = _world.Filter<PlayerComponent>().End();
           _playerPool = _world.GetPool<PlayerComponent>();
            _rbPool = _world.GetPool<RigidBodyComponent>();
            _transformPool = _world.GetPool<TransformComponent>();
            _movePool = _world.GetPool<MovementComponent>();
            _onTrigerExitPool = _world.GetPool<OnTriggerExit2DEvent>();
            _onTrigerStayPool = _world.GetPool<OnTriggerStay2DEvent>();
            _animPool = _world.GetPool<AnimationComponent>();
        }

        public void Run(EcsSystems systems)
        {
            foreach (var player in _playerFilterStay)
            {
                if (_onTrigerStayPool.Get(player).collider2D.gameObject.CompareTag("Ladder"))
                {
                    foreach (var p in _playerFilter)
                    {
                        Debug.Log("Climb!");
                        _playerPool.Get(p).rbValue.bodyType = RigidbodyType2D.Kinematic;
                        _playerPool.Get(p).isClimb = true;
                        if (Input.GetAxis("Vertical") == 0)
                        {
                            _animPool.Get(p).animator.SetInteger("State",
                                _animPool.Get(p).climbIdle);
                        }
                        else
                        {
                            _animPool.Get(p).animator.SetInteger("State",
                                _animPool.Get(p).climb);
                            _playerPool.Get(p).transformValue.Translate(Vector3.up * Input.GetAxis("Vertical") *
                                                                        _movePool.Get(p).moveSpeed * 0.03f *
                                                                        Time.deltaTime);
                        }
                    }
                }


            }
            foreach (var player in _playerFilterExit)
            {
                if (_onTrigerExitPool.Get(player).collider2D.gameObject.CompareTag("Ladder"))
                {
                    foreach (var p in _playerFilter)
                    {
                        _playerPool.Get(p).isClimb = false;
                        _playerPool.Get(p).rbValue.bodyType = RigidbodyType2D.Dynamic;
                    }
                }
            }
        }
        
    }
}