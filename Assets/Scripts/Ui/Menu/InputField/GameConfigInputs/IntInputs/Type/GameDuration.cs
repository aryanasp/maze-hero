namespace Ui.Menu.InputField.Type
{
    public class GameDuration : BaseGameConfigIntInputField
    {
        protected override void AssignValue(int finalValue)
        {
            _gameConfig.roundDuration = finalValue;
        }
    }
}