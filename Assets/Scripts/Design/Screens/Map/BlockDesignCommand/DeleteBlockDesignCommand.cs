using System.Collections.Generic;
using Map;
using UnityEngine;

namespace Design.Screens.Map
{
    public class DeleteBlockDesignCommand : ITileDesignCommand
    {
        public DesignTileCommands GetCommand()
        {
            return DesignTileCommands.Delete;
        }

        public void DoAction(TileModel currentTileModel, BlockType selectedBlockType)
        {
            var currentTileHasNoBlock = currentTileModel.BlockType == BlockType.None;
            if (currentTileHasNoBlock)
            {
                return;
            }
            DeleteTile(currentTileModel);
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