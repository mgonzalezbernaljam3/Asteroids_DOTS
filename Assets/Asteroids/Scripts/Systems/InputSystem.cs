using Asteroids.Scripts.Components;
using Unity.Entities;
using UnityEngine;

namespace Asteroids.Scripts.Systems
{
    public class InputSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            Entities.ForEach((ref InputComponentData _input) =>
            {
                _input.inputLeft = Input.GetKey(KeyCode.A);
                _input.inputRight = Input.GetKey(KeyCode.D);
                _input.inputGas = Input.GetKey(KeyCode.W);
                _input.inputShoot = Input.GetKey(KeyCode.K);
                _input.inputBomb = Input.GetKey(KeyCode.L);
                
            }).Run();
        }
    }
}
