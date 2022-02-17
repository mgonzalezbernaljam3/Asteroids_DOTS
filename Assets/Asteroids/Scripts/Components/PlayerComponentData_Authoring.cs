using System.Collections.Generic;
using Asteroids.Scripts.Generic;
using Unity.Entities;
using UnityEngine;

namespace Asteroids.Scripts.Components
{
    public class PlayerComponentData_Authoring : MonoBehaviour, IConvertGameObjectToEntity, IDeclareReferencedPrefabs
    {
        public AsteroidsBootstrapper gameBootstrapper;
        public GameObject playerPref;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData(entity, new PlayerElementComponentData { player = conversionSystem.GetPrimaryEntity(playerPref) });
            gameBootstrapper.player = entity;
        }
        public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
        {
            referencedPrefabs.Add(playerPref);
        }
    }

    public struct PlayerElementComponentData : IComponentData
    {
        public Entity player { get; set; }
    }

}

