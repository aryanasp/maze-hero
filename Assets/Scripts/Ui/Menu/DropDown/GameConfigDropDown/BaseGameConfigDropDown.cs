using Ai;
using UnityEngine;

namespace Ui.Menu.DropDown
{
    public abstract class BaseGameConfigDropDown : BaseDropDown
    {
        protected GeneticAlgorithmConfig _geneticAlgorithmConfig;
        protected override void OnEnable()
        {
            _geneticAlgorithmConfig = Resources.Load<GeneticAlgorithmConfig>("Ai/GeneticAlgorithmConfig");
            base.OnEnable();
        }
    }
}