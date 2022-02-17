using Asteroids.Scripts.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Extensions;
using Unity.Transforms;

namespace Asteroids.Scripts.Systems
{
    public class RotateSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            Entities.ForEach((
                ref PhysicsVelocity _velocity, ref MovementCommandsComponentData _commandsComponentData,
                ref Rotation _rotation, in MovementParametersComponentData _parametersComponentData, in PhysicsMass _physicsMass) =>
            {
                PhysicsComponentExtensions.ApplyAngularImpulse(
                    ref _velocity, _physicsMass,
                    _commandsComponentData.currentAngularCommand * _parametersComponentData.angularVelocity);

                float3 currentAngularSpeed = PhysicsComponentExtensions.GetAngularVelocityWorldSpace(in _velocity, in _physicsMass, in _rotation);
            
                if(math.length(currentAngularSpeed)>_parametersComponentData.maxAngularVelocity)
                {
                    PhysicsComponentExtensions.SetAngularVelocityWorldSpace(ref _velocity, _physicsMass, _rotation,
                        math.normalize(currentAngularSpeed)* _parametersComponentData.maxAngularVelocity );
                }
            
            }).Schedule();
        }
    }
}
