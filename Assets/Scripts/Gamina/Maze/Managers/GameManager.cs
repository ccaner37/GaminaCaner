using Gamina.Maze.Controllers.Container;
using Gamina.Maze.ScriptableObjects;
using Gamina.Maze.Utility.Vibration;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gamina.Maze.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public static Action OnLevelCompleted;

        public static Action OnAdClosed;

        public LevelSettings LevelSettings;

        public bool IsLevelCompleted;

        public int CollectedBalls;

        private void OnEnable()
        {
            Instance = this;

            bool vibrator = Vibrator.Android();

            ContainerTriggerController.OnBallCollected += CheckLevelCompleted;
        }

        private void OnDisable() => ContainerTriggerController.OnBallCollected -= CheckLevelCompleted;

        public void CheckLevelCompleted()
        {
            bool isLevelComplete = !(CollectedBalls >= LevelSettings.BallCount) || IsLevelCompleted;
            if (isLevelComplete) return;

            IsLevelCompleted = true;
            OnLevelCompleted();
        }

        public void LoadNextScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void OnAdClose(object sender, EventArgs args)
        {
            OnAdClosed();
        }
    }
}
