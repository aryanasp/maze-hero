using System;
using System.Collections;
using System.Collections.Generic;
using BaseClass;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Map
{
    public class MazeGenerator : MonoBehaviour
    {
        [SerializeField] private MazeConfig mazeConfig;
        [Inject] public MapModel MapModel;

        private Dictionary<BlockType, GameObject> _prefabs;

        public void Awake()
        {
            MapModel.OnTilesGenerated += GenerateMaze;
        }

        private void OnDestroy()
        {
            MapModel.OnTilesGenerated -= GenerateMaze;
        }

        private void GenerateMaze()
        {
            if (!MapModel.IsTilesGenerated) return;
            MapModel.IsMazeGenerated = false;
            _prefabs = new Dictionary<BlockType, GameObject>();
            LoadBlockTypePrefabs();
            GenerateBlocks();
            _prefabs.Clear();
            MapModel.IsMazeGenerated = true;
        }

        private void GenerateBlocks()
        {
            foreach (var mapObject in mazeConfig.mapObjects)
            {
                var tileModel = MapModel.Tiles[mapObject.position];
                var blockType = mapObject.blockType;
                var blockPrefab = _prefabs[blockType];
                var block = Instantiate(blockPrefab, tileModel.Transform);
                ConfigureMapItemModel(tileModel, blockType, block);
            }
        }

        private static void ConfigureMapItemModel(TileModel tileModel, BlockType blockType, GameObject block)
        {
            tileModel.HasBlock = true;
            tileModel.BlockType = blockType;
            tileModel.BlockTypeGameObject = block;
        }

        private void LoadBlockTypePrefabs()
        {
            for (int i = 0; i < mazeConfig.blockPrefabPaths.Count; i++)
            {
                _prefabs[mazeConfig.blockPrefabPaths[i].blockType] =
                    Resources.Load<GameObject>(mazeConfig.blockPrefabPaths[i].prefabPathToLoad);
            }
        }
    }
}