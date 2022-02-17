using Asteroids.Scripts.Components;
using Unity.Entities;
using Unity.Transforms;


namespace Asteroids.Scripts.Systems
{
    public class WeaponSystem : SystemBase
    {
        private EndSimulationEntityCommandBufferSystem endSimulationEntityCommandBufferSystem;
        
        protected override void OnCreate()
        {
            base.OnCreate();
            endSimulationEntityCommandBufferSystem = World.DefaultGameObjectInjectionWorld.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        }

        protected override void OnUpdate()
        {
            float deltaTime = Time.DeltaTime;
            EntityCommandBuffer.ParallelWriter ecb = endSimulationEntityCommandBufferSystem.CreateCommandBuffer().AsParallelWriter();

            Entities.ForEach((Entity _entity, int entityInQueryIndex , ref WeaponComponentData _weaponComponent,
                in Translation _translation,
                in Rotation _rotation) =>
            {
                _weaponComponent.timer += deltaTime;
                
                if (!_weaponComponent.isFiring) return;
                if (!(_weaponComponent.timer > _weaponComponent.fireRate)) return;
                
                _weaponComponent.timer = 0;
                Entity newProjectile = ecb.Instantiate(entityInQueryIndex, _weaponComponent.projectilePrefab);

                ecb.SetComponent(entityInQueryIndex , newProjectile, new Translation
                {
                    Value = _translation.Value
                });

                ecb.SetComponent(entityInQueryIndex , newProjectile, new Rotation()
                {
                    Value = _rotation.Value
                });

                ecb.SetComponent(entityInQueryIndex , newProjectile, new MovementCommandsComponentData()
                {
                    currentlinearCommand = 1,
                });

                ecb.AddComponent(entityInQueryIndex, newProjectile, new ParticleEffectLink()
                {
                    value = 1
                });

            }).ScheduleParallel();
            
            endSimulationEntityCommandBufferSystem.AddJobHandleForProducer(Dependency);
        }
    }
}