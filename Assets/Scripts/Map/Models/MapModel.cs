using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class MapModel
    {
        public bool IsTilesGenerated;
        public bool IsMazeGenerated;
        public readonly Dictionary<Vector2, TileModel> Tiles = new Dictionary<Vector2, TileModel>();
        
        public static bool IsInTileBoundary(TileModel indicatedTileModel, Vector3 indicatedPosition)
        {
            var isLesserThanMinX = indicatedPosition.x < indicatedTileModel.Boundary.minPoint.x;
            var isLesserThanMinY = indicatedPosition.y < indicatedTileModel.Boundary.minPoint.y;
            if (isLesserThanMinX || isLesserThanMinY)
            {
                return false;
            }
            var isMoreThanMaxX = indicatedPosition.x > indicatedTileModel.Boundary.maxPoint.x;
            var isMoreThanMaxY = indicatedPosition.y > indicatedTileModel.Boundary.maxPoint.y;
            if (isMoreThanMaxX || isMoreThanMaxY)
            {
                return false;
            }
            return true;
        }
        
    }
}