using Asteroids.Scripts.Components;
using Unity.Entities;

namespace Asteroids.Scripts.Systems
{
    public class LaserDestructionSystem : SystemBase
    {
        private EntityManager entityManager;

        protected override void OnCreate()
        {
            base.OnCreate();
            entityManager = World.EntityManager;
        }

        protected override void OnUpdate()
        {
            Entities.WithoutBurst().WithStructuralChanges().WithAll<ProjectileTagComponent>().ForEach((Entity _entity, in DestroyableComponentData _destroyable) =>
            {
                if(_destroyable.mustBeDestroyed) entityManager.DestroyEntity(_entity);

            }).Run();
        }
    }
}
