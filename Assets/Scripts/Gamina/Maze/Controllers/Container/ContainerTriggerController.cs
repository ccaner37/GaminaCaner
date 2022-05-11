using Gamina.Maze.Interactables.Interfaces;
using Gamina.Maze.Managers;
using System;
using UnityEngine;

namespace Gamina.Maze.Controllers.Container
{
    public class ContainerTriggerController : MonoBehaviour
    {
        public static Action OnBallCollected;

        private void OnTriggerEnter(Collider other)
        {
            Transform triggerer = other.transform;
            bool isParentMaze = triggerer.parent.TryGetComponent<IInteractable>(out IInteractable maze);
            if (isParentMaze)
            {
                triggerer.SetParent(null);
                GameManager.Instance.CollectedBalls++;
                OnBallCollected();
            }
        }
    }
}
