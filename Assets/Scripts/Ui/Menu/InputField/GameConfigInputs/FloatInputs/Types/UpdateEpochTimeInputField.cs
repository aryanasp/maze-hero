using System;
using System.Globalization;

namespace Ui.Menu.InputField
{
    public class UpdateEpochTimeInputField : BaseGameConfigFloatInputField
    {
        protected override void AssignValue(float finalValue)
        {
            _gameConfig.updateEpochTime = finalValue;
        }
    }
}