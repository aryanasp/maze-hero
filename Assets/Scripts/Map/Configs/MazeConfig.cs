using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Map
{
    public enum BlockType
    {
        None = 0,
        BrownBlock = 1,
        Apple = 2
    }
    
    [System.Serializable]
    public class MapObject
    {
        public BlockType blockType;
        public Vector2 position;
    }

    [System.Serializable]
    public class BlockTypePath
    {
        public BlockType blockType;
        [FilePath(ParentFolder = "Assets/Resources/")]
        public string prefabPathToLoad;
    }
    
    [CreateAssetMenu(fileName = "MazeConfig", menuName = "Game/Map/Maze Config", order = 0)]
    public class MazeConfig : ScriptableObject
    {
        public List<MapObject> mapObjects;
        [Space]
        public List<BlockTypePath> blockPrefabPaths;
    }
}