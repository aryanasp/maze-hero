using System;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class MapModel
    {
        private bool _isTilesGenerated;
        public Action OnTilesGenerated = () => { };
        private bool _isMazeGenerated;
        public Action OnMazeGenerated = () => { };
        public readonly Dictionary<Vector2, TileModel> Tiles = new Dictionary<Vector2, TileModel>();
        
        public bool IsTilesGenerated
        {
            get => _isTilesGenerated;
            set
            {
                if (_isTilesGenerated == value)
                {
                    return;
                }
                _isTilesGenerated = value;
                OnTilesGenerated.Invoke();
            }
        }

        public bool IsMazeGenerated
        {
            get => _isMazeGenerated;
            set
            {
                if (_isMazeGenerated == value)
                {
                    return;
                }

                _isMazeGenerated = value;
                OnMazeGenerated.Invoke();
            }
        }

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

        public void Reset()
        {
            foreach (var pair in Tiles)
            {
                var tile = pair.Value;
                tile.HasBlock = false;
                tile.BlockType = BlockType.None;
                UnityEngine.Object.Destroy(tile.BlockTypeGameObject);
            }
        }
    }
}