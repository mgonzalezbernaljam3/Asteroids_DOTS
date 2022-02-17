using System.Collections.Generic;
using Asteroids.Scripts.Generic;
using Unity.Entities;
using UnityEngine;

namespace Asteroids.Scripts.Components
{
    public struct EntityBufferElement : IBufferElementData
    {
        public Entity entity;
    }
    
    public class AsteroidLibrarySpawn : MonoBehaviour, IConvertGameObjectToEntity,IDeclareReferencedPrefabs
    {
        public AsteroidsBootstrapper asteroidSpawn;
        public GameObject[] asteroidsPrefabs;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            DynamicBuffer<EntityBufferElement> buffer = dstManager.AddBuffer<EntityBufferElement>(entity);
            foreach (GameObject asteroid in asteroidsPrefabs)
            {
                buffer.Add(new EntityBufferElement()
                {
                    entity = conversionSystem.GetPrimaryEntity(asteroid)
                });
            }

            asteroidSpawn.asteroidLibrary = entity;
        }

        public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
        {
            referencedPrefabs.AddRange(asteroidsPrefabs);
        }
    }

    
}