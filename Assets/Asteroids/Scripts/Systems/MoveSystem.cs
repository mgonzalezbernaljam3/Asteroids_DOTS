using Asteroids.Scripts.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Extensions;
using Unity.Transforms;
using UnityEngine;

namespace Asteroids.Scripts.Systems
{
    public class MoveSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            Entities.ForEach((
                ref MovementCommandsComponentData _movementCommandsComponentData, ref PhysicsVelocity _velocity,
                in MovementParametersComponentData _movementParametersComponentData,  
                in PhysicsMass _physicsMass, in Translation _translation ) =>
            {
                _movementCommandsComponentData.previousPosition = _translation.Value;
                
                PhysicsComponentExtensions.ApplyLinearImpulse(
                    ref _velocity, 
                    _physicsMass, 
                    _movementCommandsComponentData.currentDirectionOfMove * 
                    _movementCommandsComponentData.currentlinearCommand * 
                    _movementParametersComponentData.linearVelocity
                );
            
                if (math.length(_velocity.Linear) > _movementParametersComponentData.maxLinearVelocity) 
                    _velocity.Linear = math.normalize(_velocity.Linear) * _movementParametersComponentData.maxLinearVelocity;

            }).ScheduleParallel();

        }
    }
}