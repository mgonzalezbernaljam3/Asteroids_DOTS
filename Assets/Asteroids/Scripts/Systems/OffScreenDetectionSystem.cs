
using Asteroids.Scripts.Components;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public class OffScreenDetectionSystem : SystemBase
{
    protected override void OnUpdate()
    {
        var screenData = GetSingleton<ScreenInfoComponentData>();

        Entities.ForEach(
            (Entity _entity, ref OffScreenWrapperComponentData _offScreen,
                in MovementCommandsComponentData _moveComponent, in Translation _translation) =>
            {
                var previousPosition = _moveComponent.previousPosition;
                var currentPosition = _translation.Value;

                var isMovingLeft = currentPosition.x - previousPosition.x < 0;
                var isMovingRight = currentPosition.x - previousPosition.x > 0;
                var isMovingUp = currentPosition.y - previousPosition.y > 0;
                var isMovingDown = currentPosition.y - previousPosition.y < 0;

                _offScreen.isOffScreenLeft = _translation.Value.x < -(screenData.width + _offScreen.bounds) * .5f && isMovingLeft;
                _offScreen.isOffScreenRight = _translation.Value.x > (screenData.width + _offScreen.bounds) * .5f && isMovingRight;

                _offScreen.isOffScreenUp = _translation.Value.y > (screenData.height + _offScreen.bounds) * .5f && isMovingUp;
                _offScreen.isOffScreenDown = _translation.Value.y < -(screenData.height + _offScreen.bounds) * .5f && isMovingDown;
                
            }).ScheduleParallel();
    }
}
