using Map;

namespace Design.Screens.Map
{
    public interface ITileDesignCommand
    {
        DesignTileCommands GetCommand();
        void DoAction(TileModel currentTileModel, BlockType selectedBlockType);
    }
}