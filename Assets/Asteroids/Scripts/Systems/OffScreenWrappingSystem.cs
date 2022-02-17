using Asteroids.Scripts.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
public class OffScreenWrappingSystem : SystemBase
{
    protected override void OnUpdate()
    {
        ScreenInfoComponentData screenDataComponent = GetSingleton<ScreenInfoComponentData>();
        
        Entities.WithAll<OffScreenWrapperComponentData>().ForEach((
            Entity _entity, ref OffScreenWrapperComponentData _offScreenWrapperComponent, ref Translation _translation) => 
        {
            if (_offScreenWrapperComponent.isOffScreenLeft)
            {
                _translation.Value = SpawnOnRightSide(_translation.Value, _offScreenWrapperComponent.bounds, screenDataComponent);
            }
            else if (_offScreenWrapperComponent.isOffScreenRight)
            {
                _translation.Value = SpawnOnLeftSide(_translation.Value, _offScreenWrapperComponent.bounds, screenDataComponent);
            }
            else if (_offScreenWrapperComponent.isOffScreenUp)
            {
                _translation.Value = SpawnOnBottomSide(_translation.Value,_offScreenWrapperComponent.bounds, screenDataComponent);
            }
            else if (_offScreenWrapperComponent.isOffScreenDown)
            {
                _translation.Value = SpawnOnTopSide(_translation.Value,_offScreenWrapperComponent.bounds, screenDataComponent);
            }
            
            _offScreenWrapperComponent.isOffScreenDown = false;
            _offScreenWrapperComponent.isOffScreenRight = false;
            _offScreenWrapperComponent.isOffScreenUp = false;
            _offScreenWrapperComponent.isOffScreenLeft = false;
            
        }).ScheduleParallel();
    }

    private static float3 SpawnOnRightSide(float3 _position, float _bounds, ScreenInfoComponentData _screenDataComponent)
    {
        return new float3((_bounds + _screenDataComponent.width)*.5f, _position.y, 0);;
    }
    private static float3 SpawnOnLeftSide(float3 _position,float _bounds, ScreenInfoComponentData _screenDataComponent)
    {
        return new float3(-(_bounds + _screenDataComponent.width)*.5f,_position.y, 0);;
    }
    private static float3 SpawnOnTopSide(float3 _position,float _bounds, ScreenInfoComponentData _screenDataComponent)
    {
        return  new float3(_position.x,(_bounds + _screenDataComponent.height)*.5f,0);
    }
    private static float3 SpawnOnBottomSide(float3 _position,float _bounds, ScreenInfoComponentData _screenDataComponent)
    {
        return new float3(_position.x, - (_bounds + _screenDataComponent.height)*.5f,0);
    }
}
