namespace Ui.Menu.InputField.Type
{
    public class GameRoundInputFieldUi : BaseGameConfigIntInputField
    {
        protected override void AssignValue(int finalValue)
        {
            _gameConfig.roundCount = finalValue;
        }

        protected override string GetDescription()
        {
            return "Round Counts";
        }
    }
}