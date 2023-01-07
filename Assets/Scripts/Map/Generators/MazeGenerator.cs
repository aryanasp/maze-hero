using System.Collections;
using System.Collections.Generic;
using BaseClass;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Map
{
    public class MazeGenerator : JobBehaviour
    {
        [SerializeField] private MazeConfig mazeConfig;
        [Inject] public MapModel MapModel;

        private Dictionary<BlockType, GameObject> _prefabs;

        protected override IEnumerator StartJob()
        {
            MapModel.IsMazeGenerated = false;
            yield return new WaitUntil(() => MapModel.IsTilesGenerated);
            _prefabs = new Dictionary<BlockType, GameObject>();
            LoadBlockTypePrefabs();
            GenerateBlocks();
            yield return new WaitForEndOfFrame();
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
                ConfigureMapItemModel(tileModel, blockType);
            }
        }

        private static void ConfigureMapItemModel(TileModel tileModel, BlockType blockType)
        {
            tileModel.HasBlock = true;
            tileModel.BlockType = blockType;
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