using ECS.System;
using ECS.Systems;
using LeoEcsPhysics;
using Leopotam.EcsLite;
using UnityEngine;
using Voody.UniLeo.Lite;

namespace ECS {
    sealed class EcsStartup : MonoBehaviour
    {
        EcsWorld _world;
        EcsSystems _systems;
        void Start() 
        {
            _world = new  EcsWorld ();    
            _systems = new  EcsSystems (_world);
            EcsPhysicsEvents.ecsWorld = _world;
            _systems
                .ConvertScene()
                .Add(new BackgroundSystem())
                //.Add(new SpawnSystem())
                .Add(new ClimbSystem())
                .Add(new PickUpSystem())
                .Add(new MovementSystem())
                .Add(new GroundPatrolSystem())
                .Add(new AirPatrolSystem())
                .Add(new DetectTargetSystem())
                .Add(new MoveToTargetSystem())
                .Add(new TakeHealthSystem())
              //  .Add(new DestroyEnemySystem())
                .Init(); 
        }
       

        void FixedUpdate () {
            // process systems here.
            _systems?.Run ();
        }

        void OnDestroy () {
            if (_systems != null) {
                EcsPhysicsEvents.ecsWorld = null;
                // list of custom worlds will be cleared
                // during IEcsSystems.Destroy(). so, you
                // need to save it here if you need.
                _systems.Destroy ();
                _systems = null;
               _world.Destroy();
                _world = null;
            }
            
            // cleanup custom worlds here.
            
            // cleanup default world.
            if (_world == null) return;
            _world.Destroy ();
            _world = null;
        }
    }
}