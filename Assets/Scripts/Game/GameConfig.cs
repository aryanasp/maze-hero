using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Game/Game/Game Config", order = 0)]
    public class GameConfig : ScriptableObject
    {
        public int roundDuration;
        public int roundCount;
    }
}