using Asteroids.Scripts.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Asteroids.Scripts.Systems
{
    public class SetMoveDirectionToUpSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            Entities.WithAll<MovingInUpDirectionComponent>().ForEach((
                ref MovementCommandsComponentData _movementCommandsComponentData, 
                in Rotation _rotation) =>
            {
                float3 direction = math.mul(_rotation.Value, math.up());
                _movementCommandsComponentData.currentDirectionOfMove = direction;

            }).Schedule();
        }
    }
}