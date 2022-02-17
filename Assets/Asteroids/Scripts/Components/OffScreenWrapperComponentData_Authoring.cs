using Unity.Entities;
using UnityEngine;

namespace Asteroids.Scripts.Components
{
    public class OffScreenWrapperComponentData_Authoring : MonoBehaviour, IConvertGameObjectToEntity
    {
        public MeshRenderer mesh;
        
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData(entity, new OffScreenWrapperComponentData
            {
                bounds = mesh.bounds.extents.magnitude
            });
        }
    }
}