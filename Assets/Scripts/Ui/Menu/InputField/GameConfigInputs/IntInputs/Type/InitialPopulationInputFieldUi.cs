namespace Ui.Menu.InputField.Type
{
    public class InitialPopulationInputFieldUi : BaseGameConfigIntInputField
    {
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