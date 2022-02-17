using Asteroids.Scripts.Components;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class ScreenInfoComponentData_Authoring : MonoBehaviour, IConvertGameObjectToEntity
{
    public Camera screen;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        Vector3 bottomLeftCornerPosition = screen.ViewportToWorldPoint(new Vector3(0, 0));
        Vector3 topRightCornerPosition = screen.ViewportToWorldPoint(new Vector3(1, 1));

        float heightOfScreen = topRightCornerPosition.y - bottomLeftCornerPosition.y;
        float widthOfScreen = topRightCornerPosition.x - bottomLeftCornerPosition.x;
        float2 size = new float2(widthOfScreen, heightOfScreen);

        dstManager.AddComponentData(entity, new ScreenInfoComponentData()
        {
            height = size.y,
            width = size.x
        });
    }
}
