using ECS.Components;
using Leopotam.EcsLite;
using Unity.VisualScripting;
using UnityEngine;
using EcsFilter = Leopotam.EcsLite.EcsFilter;
using EcsWorld = Leopotam.EcsLite.EcsWorld;
using IEcsInitSystem = Leopotam.EcsLite.IEcsInitSystem;
using IEcsRunSystem = Leopotam.EcsLite.IEcsRunSystem;
using IEcsSystem = Leopotam.EcsLite.IEcsSystem;
using NotImplementedException = System.NotImplementedException;

namespace ECS.System
{
    public class MovementSystem : IEcsInitSystem, IEcsRunSystem, IEcsSystem
    {
        private EcsWorld _world;
        private EcsFilter _playerFilter;
        private EcsPool<MovementComponent> _movementPool;
        private EcsPool<RigidBodyComponent> _rbPool;
        private EcsPool<AnimationComponent> _animPool;
        private EcsPool<JumpComponent> _jumpPool;
        private EcsPool<TransformComponent> _transformPool;
        private EcsPool<GroundCheckComponent> _groundCheckPool;
        private EcsPool<PlayerComponent> _playerPool;

        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            _playerFilter = _world.Filter<PlayerComponent>().End();
            _playerPool = _world.GetPool<PlayerComponent>();
            _movementPool = _world.GetPool<MovementComponent>();
            _rbPool = _world.GetPool<RigidBodyComponent>();
            _animPool = _world.GetPool<AnimationComponent>();
            _transformPool = _world.GetPool<TransformComponent>();
            _groundCheckPool = _world.GetPool<GroundCheckComponent>();
            _jumpPool = _world.GetPool<JumpComponent>();
        }
        
        public void Run(EcsSystems systems) //это типа update
        {
            foreach (var playerEntity in _playerFilter)
            {
                CheckGround(playerEntity); 
                if (Input.GetAxis("Horizontal") == 0 && _groundCheckPool.Get(playerEntity).isGrounded
                && _playerPool.Get(playerEntity).isClimb == false)
                {
                    _animPool.Get(playerEntity).animator.SetInteger("State",
                        _animPool.Get(playerEntity).idleAnimValue);
                }
                else
                {
                    Flip(playerEntity);
                    if (_groundCheckPool.Get(playerEntity).isGrounded && _playerPool.Get(playerEntity).isClimb == false)
                    {
                        _animPool.Get(playerEntity).animator.SetInteger("State",
                            _animPool.Get(playerEntity).walkAnimValue);
                    }
                }
                Move(playerEntity);
                if (Input.GetKeyDown(KeyCode.Space) && _groundCheckPool.Get(playerEntity).isGrounded)
                {
        
                    Jump(playerEntity);
                }
            }
        }
        void Move(int entity)
        {
            var playerSpeed = _movementPool.Get(entity).moveSpeed;
            _rbPool.Get(entity).value.velocity = new Vector2(Input.GetAxis("Horizontal") *
                                                                   playerSpeed,
                _rbPool.Get(entity).value.velocity.y);
        }

        void Jump(int entity)
        {
            var height = _jumpPool.Get(entity).jumpHeight;
            _rbPool.Get(entity).value.AddForce(_transformPool.Get(entity).value.up * height, ForceMode2D.Impulse);
        }
        
        void CheckGround(int entity)
        {
            var groundCheck = _groundCheckPool.Get(entity).groundDetect;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, 0.2f); //добавляем все коллайдеры, которые попали в круг радиуса .2 и в центре groundcheck
            _groundCheckPool.Get(entity).isGrounded = colliders.Length > 1;
            if (! _groundCheckPool.Get(entity).isGrounded && _playerPool.Get(entity).isClimb == false)
            {
                _animPool.Get(entity).animator.SetInteger("State", 
                    _animPool.Get(entity).jumpAnimValue);
            }
        }
        void Flip(int entity)
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                _transformPool.Get(entity).value.localRotation = Quaternion.Euler(0,0,0); //если нажата клавиша вправо, то есть инпут = 1, то персонаж смотрит впрово, поворот на 0
            }
            if (Input.GetAxis("Horizontal") < 0)
            {
                _transformPool.Get(entity).value.localRotation = Quaternion.Euler(0,180,0); 
            }
        }
    }
}