using System;
using Asteroids.Scripts.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Random = UnityEngine.Random;
using Asteroids.Managers;

namespace Asteroids.Scripts.Generic
{
    public class AsteroidsBootstrapper : MonoBehaviour
    {

        public Entity asteroidLibrary;
        public Entity player;
        public Transform[] asteroidsSpawnPositions;
        public float asteroidSpawnFrequency = 3f;

        private EntityManager entityManager;
        private Vector3[] spawnPositionsVectors;
        private GameManager gm;
        private float currentTimer;

        private void Awake()
        {
            entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            gm = GameManager.Instance;


            spawnPositionsVectors = new Vector3[asteroidsSpawnPositions.Length];
            for (int i = 0; i < spawnPositionsVectors.Length; i++)
            {
                spawnPositionsVectors[i] = asteroidsSpawnPositions[i].position;
            }
        }


        void Start()
        {
            entityManager.CreateEntity(typeof(InputComponentData));
            SpawnPlayerAtPosition(Vector3.zero);
            gm.StartGame();
        }

        public void StartGame()
        {
            SpawnPlayerAtPosition(Vector3.zero);

        }

        private void SpawnAsteroid()
        {
            DynamicBuffer<EntityBufferElement> buffer = entityManager.GetBuffer<EntityBufferElement>(asteroidLibrary);
            int lengthOfBuffer = buffer.Length;
            int randomAsteroidIndex = Random.Range(0, lengthOfBuffer);
            Entity newAsteroid = entityManager.Instantiate(buffer[randomAsteroidIndex].entity);

            int randomSpawnPositionIndex = Random.Range(0, spawnPositionsVectors.Length);
            Vector3 spawnPosition = spawnPositionsVectors[randomSpawnPositionIndex];

            entityManager.SetComponentData(newAsteroid, new Translation()
            {
                Value = spawnPosition
            });

            float3 randomMoveDirection = math.normalize(new float3(Random.Range(-.8f, .8f), Random.Range(-.8f, .8f), 0));
            float3 randomRotation = math.normalize(new float3(Random.value, Random.value, 0));

            entityManager.SetComponentData(newAsteroid, new MovementCommandsComponentData()
            {
                previousPosition = spawnPosition,
                currentDirectionOfMove = randomMoveDirection,
                currentlinearCommand = 1,
                currentAngularCommand = randomRotation
            });
        }

        private void Update()
        {
            currentTimer += Time.deltaTime;

            if (currentTimer > asteroidSpawnFrequency)
            {
                currentTimer = 0;
                SpawnAsteroid();
            }

        }

        private void SpawnPlayerAtPosition(Vector3 _spawnPosition)
        {
            var playerRef = entityManager.GetComponentData<PlayerElementComponentData>(player);
            var playerShip = entityManager.Instantiate(playerRef.player);
            entityManager.SetComponentData(player, new Translation
            {
                Value = _spawnPosition
            });

        }
    }
}
