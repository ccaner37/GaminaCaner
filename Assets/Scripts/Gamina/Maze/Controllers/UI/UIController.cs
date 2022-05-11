using Gamina.Maze.Managers;
using Gamina.Maze.Utility.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Gamina.Maze.Controllers.UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField]
        private GameObject _levelCompletedPanel;

        [SerializeField]
        private float _completedPanelDelay = 0.6f, _completedPanelScaleSpeed = 4f;

        private void OnEnable() => GameManager.OnAdClosed += EnableLevelCompletedPanel;
        private void OnDisable() => GameManager.OnAdClosed -= EnableLevelCompletedPanel;

        private async void EnableLevelCompletedPanel()
        {
            int delay = (int)(_completedPanelDelay * 1000);
            await Task.Delay(delay);

            _levelCompletedPanel.transform.localScale = Vector3.zero;
            _levelCompletedPanel.SetActive(true);
            CustomTween.Instance.Scale(_levelCompletedPanel.transform, Vector3.one, _completedPanelScaleSpeed);
        }

        public void NextLevelButton() => GameManager.Instance.LoadNextScene();
    }
}
