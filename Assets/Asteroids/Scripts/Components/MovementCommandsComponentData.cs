using Unity.Entities;
using Unity.Mathematics;

namespace Asteroids.Scripts.Components
{
    [GenerateAuthoringComponent]
    public struct MovementCommandsComponentData : IComponentData
    {
        public float3 currentDirectionOfMove;
        public float3 currentAngularCommand;
        public float currentlinearCommand;
        public float3 previousPosition;
    }
}