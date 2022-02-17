using Asteroids.Scripts.Components;
using Asteroids.Managers;
using Unity.Entities;

namespace Asteroids.Scripts.Systems
{
    public class AsteroidDestructionSystem : SystemBase
    {
        private EntityManager entityManager;
        private GameManager gm;

        protected override void OnCreate()
        {
            base.OnCreate();
            entityManager = World.EntityManager;
            gm = GameManager.Instance;
        }

        protected override void OnUpdate()
        {
            Entities.WithoutBurst().WithStructuralChanges().WithAll<AsteroidTagComponent>().ForEach((
                Entity _entity,
                in DestroyableComponentData _destroyable) =>
            {
                if (_destroyable.mustBeDestroyed)
                {
                    gm.UpdatePoints(_destroyable.points);
                    entityManager.DestroyEntity(_entity);

                }

            }).Run();
        }
    }
}
