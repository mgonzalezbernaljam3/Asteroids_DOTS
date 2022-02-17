using Unity.Entities;

namespace Asteroids.Scripts.Components
{
    [GenerateAuthoringComponent]
    public struct DestroyableComponentData : IComponentData
    {
        public bool mustBeDestroyed;
        public int points;
    }
}