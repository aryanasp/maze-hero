namespace Ui.Menu.InputField.Type
{
    public class GameRoundDurationInputFieldUi : BaseGameConfigIntInputField
    {
        protected override void AssignValue(int finalValue)
        {
            _gameConfig.roundDuration = finalValue;
        }

        protected override void Initialize()
        {
            inputField.text = _gameConfig.roundDuration.ToString();
        }

        protected override string GetDescription()
        {
            return "Each Round Duration";
        }
    }
}