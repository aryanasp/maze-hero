namespace Ui.Menu.InputField.Type
{
    public class GameRoundInputFieldUi : BaseGameConfigIntInputField
    {
        protected override void AssignValue(int finalValue)
        {
            _gameConfig.roundCount = finalValue;
        }

        protected override void Initialize()
        {
            inputField.text = _gameConfig.roundCount.ToString();

        }

        protected override string GetDescription()
        {
            return "Round Counts";
        }
    }
}