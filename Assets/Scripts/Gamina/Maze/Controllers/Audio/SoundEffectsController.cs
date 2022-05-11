using Gamina.Maze.Controllers.Container;
using Gamina.Maze.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamina.Maze.Audio
{
    public class SoundEffectsController : MonoBehaviour
    {
        [SerializeField]
        private AudioClip _ballCollectAudio, _levelCompleteAudio;

        [SerializeField]
        private AudioSource _audioSource;

        private void OnEnable()
        {
            ContainerTriggerController.OnBallCollected += PlayBallCollectSound;
            GameManager.OnAdClosed += PlayLevelCompleteSound;
        }

        private void OnDisable()
        {
            ContainerTriggerController.OnBallCollected -= PlayBallCollectSound;
            GameManager.OnAdClosed -= PlayLevelCompleteSound;
        }

        private void PlayBallCollectSound() => PlayAudio(_ballCollectAudio);

        private void PlayLevelCompleteSound() => PlayAudio(_levelCompleteAudio);

        private void PlayAudio(AudioClip clip)
        {
            _audioSource.clip = clip;
            _audioSource.Play();
        }
    }
}
