using Unity.Entities;

namespace Asteroids.Scripts.Components
{
    [GenerateAuthoringComponent]
    public struct LifeTimeComponentData : IComponentData
    {
        public float maxLifeTime;
        public float currentLifeTime;
    }
}
