using UnityEngine;

namespace Gameplay.Scripts.Level
{
    [CreateAssetMenu(menuName = "MyAssets/BaseLevelData")]
    public class LevelData : ScriptableObject
    {
        public float timeToFinish;
        public float lineLength;
        public int attemptToWin;
        public int incorrectTimes;
    }
}