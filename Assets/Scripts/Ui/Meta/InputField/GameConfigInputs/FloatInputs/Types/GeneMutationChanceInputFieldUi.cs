using System.Globalization;

namespace Ui.Menu.InputField
{
    public class GeneMutationChanceInputFieldUi : BaseGameConfigFloatInputField
    {
        protected override void Initialize()
        {
            inputField.text = _geneticAlgorithmConfig.geneMutationChance.ToString(CultureInfo.InvariantCulture);
        }

        protected override string GetDescription()
        {
            return "Gene Mutate Chance";
        }

        protected override void AssignValue(float finalValue)
        {
            _geneticAlgorithmConfig.geneMutationChance = finalValue;
        }
    }
}