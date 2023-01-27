using System;
using Ai;
using Game;
using UnityEngine;

namespace Ui.Menu.InputField
{
    public abstract class BaseGameConfigInputField : BaseInputField
    {
        protected GameConfig _gameConfig;
        protected GeneticAlgorithmConfig _geneticAlgorithmConfig;
        
        protected override void OnEnable()
        {
            _gameConfig = Resources.Load<GameConfig>("GameConfig");
            _geneticAlgorithmConfig = Resources.Load<GeneticAlgorithmConfig>("Ai/GeneticAlgorithmConfig");
            base.OnEnable();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _gameConfig = null;
            _geneticAlgorithmConfig = null;
        }
    }
}