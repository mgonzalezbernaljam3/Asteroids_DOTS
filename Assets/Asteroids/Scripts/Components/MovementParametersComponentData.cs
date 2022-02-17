using Unity.Entities;

namespace Asteroids.Scripts.Components
{
    [GenerateAuthoringComponent]
    public struct MovementParametersComponentData : IComponentData
    {
        public float linearVelocity;
        public float maxLinearVelocity;
        public float angularVelocity;
        public float maxAngularVelocity;
    }
}