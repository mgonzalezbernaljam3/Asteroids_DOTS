using Asteroids.Scripts.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using Unity.Physics;
using Unity.Physics.Systems;

namespace Asteroids.Scripts.Systems
{
    public class OnPhysicsTriggerJobSystem : JobComponentSystem
    {
        private BuildPhysicsWorld physicsWorld;
        private StepPhysicsWorld stepPhysicsWorld;

        protected override void OnCreate()
        {
            base.OnCreate();
            physicsWorld = World.DefaultGameObjectInjectionWorld.GetOrCreateSystem<BuildPhysicsWorld>();
            stepPhysicsWorld = World.DefaultGameObjectInjectionWorld.GetOrCreateSystem<StepPhysicsWorld>();
        }
        
        [BurstCompile]
        private struct TriggerJob : ITriggerEventsJob
        {
            public ComponentDataFromEntity<DestroyableComponentData> destroyables;
        
            public void Execute(TriggerEvent triggerEvent)
            {
                if (destroyables.HasComponent(triggerEvent.EntityA))
                {
                    var destroyable = destroyables[triggerEvent.EntityA];
                    destroyable.mustBeDestroyed = true;
                    destroyables[triggerEvent.EntityA] = destroyable;
                }
                if (destroyables.HasComponent(triggerEvent.EntityB))
                {
                    var destroyable = destroyables[triggerEvent.EntityB];
                    destroyable.mustBeDestroyed = true;
                    destroyables[triggerEvent.EntityB] = destroyable;
                }
            }
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            ComponentDataFromEntity<DestroyableComponentData> destroyablesJob = GetComponentDataFromEntity<DestroyableComponentData>();

            TriggerJob job = new TriggerJob
            {
                destroyables = destroyablesJob
            };

            JobHandle jobHandle = job.Schedule(stepPhysicsWorld.Simulation, ref physicsWorld.PhysicsWorld, inputDeps);
            jobHandle.Complete();
            return jobHandle;
        }

    }
}