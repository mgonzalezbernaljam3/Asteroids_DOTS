using Unity.Entities;

namespace Asteroids.Scripts.Components
{
    public struct InputComponentData : IComponentData
    {
        public bool inputLeft;
        public bool inputRight;
        public bool inputGas;
        public bool inputShoot;
        public bool inputBomb;
    }
    
}
