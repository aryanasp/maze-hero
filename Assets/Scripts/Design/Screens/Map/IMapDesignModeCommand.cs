using Map;

namespace Design.Screens.Map
{
    public interface IMapDesignModeCommand
    {
        DesignModeCommands GetCommand();
        void DoAction(TileModel currentTileModel, BlockType selectedBlockType);
    }
}