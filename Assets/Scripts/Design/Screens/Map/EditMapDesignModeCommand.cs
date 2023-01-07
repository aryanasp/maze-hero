using System.Collections.Generic;
using Map;
using UnityEngine;

namespace Design.Screens.Map
{
    public class EditMapDesignModeCommand : IMapDesignModeCommand
    {
        private MapModel _mapModel;
        private MazeConfig _mazeConfig;
        private Dictionary<BlockType, GameObject> _prefabs;

        public EditMapDesignModeCommand(MapModel mapModel, MazeConfig mazeConfig)
        {
            _prefabs = new Dictionary<BlockType, GameObject>();
            _mapModel = mapModel;
            _mazeConfig = mazeConfig;
            foreach (var blockPrefabPath in _mazeConfig.blockPrefabPaths)
            {
                _prefabs[blockPrefabPath.blockType] = Resources.Load<GameObject>(blockPrefabPath.prefabPathToLoad);
            }
        }

        public DesignModeCommands GetCommand()
        {
            return DesignModeCommands.Edit;
        }

        public void DoAction(TileModel currentTileModel, BlockType selectedBlockType)
        {
            if (currentTileModel == null)
            {
                return;
            }
            if (currentTileModel.BlockType == selectedBlockType) return;
            if (currentTileModel.BlockType != BlockType.None)
            {
                UnityEngine.Object.Destroy(currentTileModel.BlockTypeGameObject);
                currentTileModel.BlockType = BlockType.None;
                currentTileModel.HasBlock = false;
            }
            if (selectedBlockType == BlockType.None) return;
            UnityEngine.Object.Instantiate(_prefabs[selectedBlockType], currentTileModel.Transform);
            currentTileModel.BlockType = selectedBlockType;
            currentTileModel.HasBlock = true;
        }
    }
}