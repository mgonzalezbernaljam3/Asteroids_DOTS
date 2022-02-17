using Unity.Entities;

namespace Asteroids.Scripts.Components
{
    [GenerateAuthoringComponent]  
    public struct ScreenInfoComponentData : IComponentData
    {
        public float height;
        public float width;
    }
}