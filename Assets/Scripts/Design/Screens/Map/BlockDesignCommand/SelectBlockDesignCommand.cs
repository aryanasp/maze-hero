using Map;

namespace Design.Screens.Map
{
    public class SelectBlockDesignCommand : ITileDesignCommand
    {
        public DesignTileCommands GetCommand()
        {
            return DesignTileCommands.Select;
        }
        
        //TODO: Show Tile Info
        public void DoAction(TileModel currentTileModel, BlockType selectedBlockType)
        {
            return;
        }
    }
}