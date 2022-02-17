using Asteroids.Scripts.Components;
using Unity.Entities;

namespace Asteroids.Scripts.Systems
{
    public class PrepareForDestructionIfLifeTimeIsOverSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            float deltaTime = Time.DeltaTime;
            Entities.ForEach((Entity _entity, ref LifeTimeComponentData _lifeTime, ref DestroyableComponentData _destroyableComponent) =>
            {
                _lifeTime.currentLifeTime += deltaTime;
            
                if (_lifeTime.currentLifeTime >= _lifeTime.maxLifeTime && _destroyableComponent.mustBeDestroyed == false)
                {
                    _destroyableComponent.mustBeDestroyed = true;
                }
            }).ScheduleParallel();
        }
    }
}
