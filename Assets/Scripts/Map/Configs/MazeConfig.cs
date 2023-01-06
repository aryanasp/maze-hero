using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    [CreateAssetMenu(fileName = "MazeConfig", menuName = "Game/Map/Maze Config", order = 0)]
    public class MazeConfig : ScriptableObject
    {
        public List<Vector2> positions;
    }
}