using Asteroids.Scripts.Components;
using Asteroids.Managers;
using Unity.Entities;

namespace Asteroids.Scripts.Systems
{
    public class PlayerDestructionSystem : SystemBase
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
            Entities
            .WithStructuralChanges()
            .WithoutBurst()
            .WithAll<PlayerTagComponent>()
            .ForEach((Entity _entity, ref DestroyableComponentData _destroyable) =>
            {
                if (_destroyable.mustBeDestroyed)
                {
                    entityManager.DestroyEntity(_entity);
                    gm.UpdateLives(1);
                }
            }).Run();
        }
    }
}

