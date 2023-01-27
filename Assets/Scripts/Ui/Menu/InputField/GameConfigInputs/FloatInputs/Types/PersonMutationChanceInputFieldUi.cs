namespace Ui.Menu.InputField
{
    public class PersonMutationChanceInputFieldUi : BaseGameConfigFloatInputField
    {
        protected override string GetDescription()
        {
            return "Person Mutate Chance";
        }

        protected override void AssignValue(float finalValue)
        {
            _geneticAlgorithmConfig.chanceToMutateNewPerson = finalValue;
        }
    }
}