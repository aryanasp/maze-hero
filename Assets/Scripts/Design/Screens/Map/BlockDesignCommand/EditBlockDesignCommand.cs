using System.Collections.Generic;
using Map;
using UnityEngine;

namespace Design.Screens.Map
{
    public class EditBlockDesignCommand : ITileDesignCommand
    {
        private MapModel _mapModel;
        private MazeConfig _mazeConfig;
        private Dictionary<BlockType, GameObject> _prefabs;

        public EditBlockDesignCommand(MapModel mapModel, MazeConfig mazeConfig)
        {
            _prefabs = new Dictionary<BlockType, GameObject>();
            _mapModel = mapModel;
            _mazeConfig = mazeConfig;
            foreach (var blockPrefabPath in _mazeConfig.blockPrefabPaths)
            {
                _prefabs[blockPrefabPath.blockType] = Resources.Load<GameObject>(blockPrefabPath.prefabPathToLoad);
            }
        }

        public DesignTileCommands GetCommand()
        {
            return DesignTileCommands.Edit;
        }

        public void DoAction(TileModel currentTileModel, BlockType selectedBlockType)
        {
            if (CheckIfTileNotActable(currentTileModel, selectedBlockType)) return;
            var currentTileHasBlock = currentTileModel.BlockType != BlockType.None;
            if (currentTileHasBlock)
            {
                DeleteTile(currentTileModel);
            }
            CreateNewBlock(currentTileModel, selectedBlockType);
        }

        private static bool CheckIfTileNotActable(TileModel currentTileModel, BlockType selectedBlockType)
        {
            if (currentTileModel == null)
            {
                return true;
            }

            if (currentTileModel.BlockType == selectedBlockType) return true;
            return false;
        }

        private void CreateNewBlock(TileModel currentTileModel, BlockType selectedBlockType)
        {
            if (selectedBlockType == BlockType.None) return;
            var newTile = UnityEngine.Object.Instantiate(_prefabs[selectedBlockType], currentTileModel.Transform);
            currentTileModel.BlockType = selectedBlockType;
            currentTileModel.HasBlock = true;
            currentTileModel.BlockTypeGameObject = newTile;
        }

        private void DeleteTile(TileModel currentTileModel)
        {
            UnityEngine.Object.Destroy(currentTileModel.BlockTypeGameObject);
            currentTileModel.BlockType = BlockType.None;
            currentTileModel.HasBlock = false;
            currentTileModel.BlockTypeGameObject = null;
        }
    }
}