using System;
using Ai;
using TMPro;

namespace Ui.Menu.DropDown
{
    public class InitialHeuristicDropDownUi : BaseGameConfigDropDown
    {
        protected override void FillOptions()
        {
            dropdown.options.Clear();
            foreach (InitialPopulationHeuristic heuristic in Enum.GetValues(typeof(InitialPopulationHeuristic)))
            {
                dropdown.options.Add(new TMP_Dropdown.OptionData
                {
                    image = null,
                    text = heuristic.ToString()
                });
            }
        }

        protected override string GetDescription()
        {
            return "Initial Heuristic";
        }
        
        protected override void Initialize()
        {
            dropdown.value = (int)_geneticAlgorithmConfig.initialHeuristic;
        }

        protected override void OnValueChange(int newValue)
        {
            _geneticAlgorithmConfig.initialHeuristic = (InitialPopulationHeuristic)newValue;
        }
    }
}