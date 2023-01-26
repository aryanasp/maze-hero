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

        protected override string GetDescription()
        {
            return "Recalculate Path Epoch Time";
        }
    }
}