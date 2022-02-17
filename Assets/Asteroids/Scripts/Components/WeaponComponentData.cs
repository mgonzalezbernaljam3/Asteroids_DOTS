using Unity.Entities;

namespace Asteroids.Scripts.Components
{
    [GenerateAuthoringComponent]
    public struct WeaponComponentData : IComponentData
    {
        public Entity projectilePrefab;
        public bool isFiring;
        public float fireRate;
        public float timer;
    }
}
