using Unity.Entities;

namespace Asteroids.Scripts.Components
{
    public struct OffScreenWrapperComponentData : IComponentData
    {
        public float bounds;
        public bool isOffScreenLeft;
        public bool isOffScreenRight;
        public bool isOffScreenDown;
        public bool isOffScreenUp;
    }
}