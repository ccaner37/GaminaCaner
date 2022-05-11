using UnityEngine;

namespace Gamina.Maze.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Level Settings", menuName = "ScriptableObjects/LevelSettings")]
    public class LevelSettings : ScriptableObject
    {
        [Range(10, 30)]
        public int BallCount;

        public Color BallColor;

        public GameObject BallPrefab;
    }
}
