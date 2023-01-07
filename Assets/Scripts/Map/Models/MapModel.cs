using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class MapModel
    {
        public bool IsTilesGenerated;
        public bool IsMazeGenerated;
        public bool IsGemGenerated;
        public readonly Dictionary<Vector2, TileModel> Tiles = new Dictionary<Vector2, TileModel>();
    }
}