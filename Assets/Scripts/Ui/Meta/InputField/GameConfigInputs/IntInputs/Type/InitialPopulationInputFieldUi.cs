namespace Ui.Menu.InputField.Type
{
    public class InitialPopulationInputFieldUi : BaseGameConfigIntInputField
    {
        protected override void Initialize()
        {
            inputField.text = _geneticAlgorithmConfig.initialPopulation.ToString();

        }

        protected override string OnInputValidate(string inpString)
        {
            var validatedString = base.OnInputValidate(inpString);
            var number = int.Parse(validatedString);
            if (number % 4 != 0)
            {
                number -= number % 4;
            }
            return number.ToString();
        }
        protected override string GetDescription()
        {
            return "Initial Population";
        }

        protected override void AssignValue(int finalValue)
        {
            _geneticAlgorithmConfig.initialPopulation = finalValue;
        }
        
    }
}