using System;
using Ai;
using TMPro;

namespace Ui.Menu.DropDown
{
    public class GeneticAlgorithmTypeDropDownUi : BaseGameConfigDropDown
    {
        protected override string GetDescription()
        {
            return "Genetic Algorithm State";
        }

        protected override void Initialize()
        {
            dropdown.value = (int)_geneticAlgorithmConfig.geneticAlgorithmType;
        }

        protected override void FillOptions()
        {
            dropdown.options.Clear();
            foreach (GeneticAlgorithmType heuristic in Enum.GetValues(typeof(GeneticAlgorithmType)))
            {
                dropdown.options.Add(new TMP_Dropdown.OptionData
                {
                    image = null,
                    text = heuristic.ToString()
                });
            }
        }

        protected override void OnValueChange(int newValue)
        {
            _geneticAlgorithmConfig.geneticAlgorithmType = (GeneticAlgorithmType)newValue;
        }
    }
}