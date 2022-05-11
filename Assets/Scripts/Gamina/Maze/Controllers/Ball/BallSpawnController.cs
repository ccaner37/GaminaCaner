using Gamina.Maze.Managers;
using Gamina.Maze.ScriptableObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gamina.Maze.Controllers.Ball
{
    public class BallSpawnController : MonoBehaviour
    {
        private LevelSettings _settings;

        [SerializeField]
        private Transform _spawnPoint;

        private void Start()
        {
            _settings = GameManager.Instance.LevelSettings;
            SpawnBalls();
        }

        private void SpawnBalls()
        {
            float ballCount = _settings.BallCount;
            GameObject ball = _settings.BallPrefab;

            for (int i = 0; i < ballCount; i++)
            {
                Vector2 randomPos = Random.insideUnitCircle;
                randomPos += new Vector2(_spawnPoint.position.x, _spawnPoint.position.y);
                Vector3 spawnPos = new Vector3(randomPos.x, randomPos.y, _spawnPoint.position.z);

                GameObject spawnedBall = Instantiate(ball, spawnPos, ball.transform.rotation, transform);
                GiveColor(spawnedBall);
            }
        }

        private void GiveColor(GameObject ball)
        {
            Color color = _settings.BallColor;
            Material material = ball.GetComponent<Renderer>().material;
            material.color = color;
        }
    }
}
