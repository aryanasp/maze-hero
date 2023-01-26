namespace Ui.Menu.InputField.Type
{
    public class GameRoundInputField : BaseGameConfigIntInputField
    {
        protected override void AssignValue(int finalValue)
        {
            _gameConfig.roundCount = finalValue;
        }
    }
}