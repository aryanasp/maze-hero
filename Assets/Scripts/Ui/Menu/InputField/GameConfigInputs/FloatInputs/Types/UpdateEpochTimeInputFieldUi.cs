using System;
using System.Globalization;

namespace Ui.Menu.InputField
{
    public class UpdateEpochTimeInputFieldUi : BaseGameConfigFloatInputField
    {
        protected override void AssignValue(float finalValue)
        {
            _geneticAlgorithmConfig.updateEpochTime = finalValue;
        }

        protected override void Initialize()
        {
            inputField.text = _geneticAlgorithmConfig.updateEpochTime.ToString(CultureInfo.InvariantCulture);
        }

        protected override string GetDescription()
        {
            return "Recalculate Path Epoch Time";
        }
    }
}