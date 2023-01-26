namespace Ui.Menu.InputField.Type
{
    public class GameRoundDurationUi : BaseGameConfigIntInputField
    {
        protected override void AssignValue(int finalValue)
        {
            _gameConfig.roundDuration = finalValue;
        }

        protected override string GetDescription()
        {
            return "Each Round Duration";
        }
    }
}