using Asteroids.Scripts.Components;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace Asteroids.Scripts.Systems
{
    public class PlayerControlSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            EntityQuery query = GetEntityQuery(typeof(InputComponentData));
            NativeArray<InputComponentData> array = query.ToComponentDataArray<InputComponentData>(Allocator.TempJob);

            if (array.Length == 0)
            {
                array.Dispose();
                return;
            }
            InputComponentData inputData = array[0];
            
            Entities.WithAll<PlayerTagComponent>().
                ForEach((ref MovementCommandsComponentData _movementCommandsComponentData,
                ref WeaponComponentData _weaponComponent) =>
            {
                int turningLeft = inputData.inputLeft ? 1 : 0;
                int turningRight = inputData.inputRight ? 1 : 0;
                
                int rotationDirection = turningLeft - turningRight;

                _weaponComponent.isFiring = inputData.inputShoot;
                
                _movementCommandsComponentData.currentAngularCommand = new float3(0,0,rotationDirection);
                _movementCommandsComponentData.currentlinearCommand = inputData.inputGas ? 1 : 0;

            }).ScheduleParallel();
            
            array.Dispose();
        }
    }
}